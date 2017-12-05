using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPManager : MonoBehaviour {

    private Transform[] patilRows;

    private LineTimer lineTimer;

    private bool chooseEnabled;
    private Color trueColor;
    private Color[] patilColorsArray;

    private Color[] avalaibleColors;

    private CPMedalProgressManager medalProgressManager;

    private Color _chosenColor;
    public Color chosenColor {
        get {
            return _chosenColor;
        }
        set {
            if (!chooseEnabled) {
                return;
            }
            chooseEnabled = false;
            _chosenColor = value;
            CheckChosenColor();
        }
    }

    private void GetChildren () {
        Transform patilContainer = transform.Find("PatilContainer");
        patilRows = new Transform[patilContainer.childCount];
        for (int i = 0 ; i < patilRows.Length ; i++) {
            patilRows[i] = patilContainer.GetChild(i);
        }
        lineTimer = transform.Find("LineTimer").GetComponent<LineTimer>();
        medalProgressManager = transform.Find("MedalProgressManager").GetComponent<CPMedalProgressManager>();
    }

    private void InitAvailableColors () {
        avalaibleColors = new Color[10];
        avalaibleColors[0] = Color.green;
        avalaibleColors[1] = Color.red;
        avalaibleColors[2] = Color.green;
        avalaibleColors[3] = Color.grey;
        avalaibleColors[4] = Color.yellow;
        avalaibleColors[5] = Color.blue;
        avalaibleColors[6] = Color.magenta;
        avalaibleColors[7] = StaticMethodsAndProperties.hexToColor("#800080");
        avalaibleColors[8] = StaticMethodsAndProperties.hexToColor("#FFA500");
        avalaibleColors[9] = StaticMethodsAndProperties.hexToColor("#FFD700");
    }

    void Awake () {
        GetChildren();
        InitAvailableColors();

        medalProgressManager.SetDifficulty(new int[] { 5, 5, 5, 5 }, new int[] { 2, 2, 2, 2 });
        medalProgressManager.gameObject.SetActive(true);
        transform.Find("LifeCount").gameObject.SetActive(true);
    }

    void OnEnable () {
        StartNewRound();
    }

    void OnDisable () {
        chooseEnabled = false;
    }

    public void StartNewRound () {
        CreateNewRound();
        chooseEnabled = true;
        lineTimer.ShowTimer(3);
    }

    private void CreateNewRound () {
        switch (medalProgressManager.Difficulty().currentMedal) {
            case CPDifficulty.Medal.None:
                CreatePatilColorsArray(new int[] { 70, 20, 10 });
                break;
            case CPDifficulty.Medal.Bronze:
                CreatePatilColorsArray(new int[] { 60, 20, 10, 10 });
                break;
            case CPDifficulty.Medal.Silver:
                CreatePatilColorsArray(new int[] { 40, 30, 20, 10 });
                break;
            case CPDifficulty.Medal.Gold:
            case CPDifficulty.Medal.Platinium:
            default:
                CreatePatilColorsArray(new int[] { 30, 20, 20, 10, 10, 10 });
                break;
        }

        SetPatilColors();
    }

    private void CreatePatilColorsArray (int[] percents, int extraColorCount = 0) {
        patilColorsArray = new Color[64];
        int avalaibleIndex = 0;
        RandomizeColorArray(avalaibleColors);
        trueColor = avalaibleColors[0];
        int minIndex, maxIndex;
        int definedColorsCount = 0;
        for (int i = 0 ; i < percents.Length ; i++) {
            maxIndex = definedColorsCount + patilColorsArray.Length * percents[i] / 100;
            minIndex = definedColorsCount;
            for (int k = minIndex ; k < maxIndex ; k++) {
                patilColorsArray[k] = avalaibleColors[i];
                definedColorsCount++;
            }
            avalaibleIndex++;
        }
        minIndex = definedColorsCount;
        for (int k = minIndex ; k < patilColorsArray.Length ; k++) {
            patilColorsArray[k] = avalaibleColors[avalaibleIndex];
        }
        RandomizeColorArray(patilColorsArray);
    }

    private void SetPatilColors () {
        int index = 0;
        foreach (Transform patilRow in patilRows) {
            foreach (Transform patil in patilRow) {
                patil.GetComponent<Patil>().color = patilColorsArray[index];
                index++;
            }
        }
        chooseEnabled = true;
    }

    public void TimerEnded () {
        medalProgressManager.ReduceLife();
    }

    private void CheckChosenColor () {
        if (chosenColor == trueColor) {
            medalProgressManager.Scored();
        } else {
            medalProgressManager.ReduceLife();
        }
    }

    private void RandomizeColorArray (Color[] arr) {
        for (var i = arr.Length - 1 ; i > 0 ; i--) {
            var r = Random.Range(0, i);
            var tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }
}
