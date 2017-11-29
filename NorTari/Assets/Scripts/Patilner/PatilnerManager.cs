using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatilnerManager : MonoBehaviour {

    private Transform[] patilRows;
    public static int currentLevel;

    private int levelRoundsWinCount;
    private int roundsToWin = 5;
    private LineTimer lineTimer;

    private bool chooseEnabled;
    private Color trueColor;
    private Color[] patilColorsArray;

    private Color[] avalaibleColors;

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
        currentLevel = 0;
        levelRoundsWinCount = 0;
        InitAvailableColors();
    }

    void OnEnable () {
        StartNewRound();
    }

    void OnDisable () {
        chooseEnabled = false;
    }

    private void StartNewRound () {
        CreateNewRound();
        chooseEnabled = true;
        lineTimer.ShowTimer(4);
    }

    private void CreateNewRound () {
        if (currentLevel == 0) {
            CreatePatilColorsArray(new int[] { 70, 20, 10 });
        } else if (currentLevel == 1) {
            CreatePatilColorsArray(new int[] { 60, 20, 10, 10 });
        } else if (currentLevel == 2) {
            CreatePatilColorsArray(new int[] { 40, 30, 20, 10 });
        } else {
            CreatePatilColorsArray(new int[] { 30, 20, 20, 10, 10, 10 });
        }
        SetPatilColors();
        Debug.Log("Creating New Round with Difficulty: " + currentLevel + " rounds win: " + levelRoundsWinCount);
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

    public void LostRound () {
        Debug.Log("YOU LOST");
        currentLevel = 0;
        levelRoundsWinCount = 0;
        StartNewRound();
    }

    private void CheckChosenColor () {
        if (chosenColor == trueColor) {
            levelRoundsWinCount++;
            if (levelRoundsWinCount >= roundsToWin) {
                currentLevel++;
                levelRoundsWinCount = 0;
            }
            StartNewRound();
        } else {
            LostRound();
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
