using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class STMedalProgressManager : MonoBehaviour {

    private Image difficulty_ind_image;
    private RectTransform high_score_bar;
    private RectTransform score_bar;
    private static STDifficulty currentDifficulty;

    private STManager ssManager;
    private bool roundEnded;

    private int currentScore = 0;
    private static int currentMedalHighScore;

    public Text lifeCountText;
    private int _lifeCount;
    public int lifeCount {
        set {
            _lifeCount = value;
            lifeCountText.text = value + "";
        }
        get {
            return _lifeCount;
        }
    }

    void Awake () {
        GetChildren();
        UpdateUI();
        lifeCount = currentDifficulty.lifeCountToReachMedals[Mathf.Min((int) currentDifficulty.currentMedal,
            currentDifficulty.lifeCountToReachMedals.Length - 1)];
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
    }

    public STDifficulty Difficulty () {
        return currentDifficulty;
    }

    private void GetChildren () {
        high_score_bar = transform.Find("HighScore").GetComponent<RectTransform>();
        score_bar = transform.Find("CurrentScore").GetComponent<RectTransform>();
        difficulty_ind_image = transform.Find("CurrentMedal").GetComponent<Image>();
        ssManager = transform.parent.GetComponent<STManager>();
    }

    public void SetDifficulty (int[] scoresToReachMedals, int[] lifeCountToReachMedals) {
        if (currentDifficulty != null) {
            return;
        }

        currentDifficulty = new STDifficulty();
        currentDifficulty.currentMedal = STDifficulty.Medal.None;
        currentDifficulty.scoresToReachMedals = scoresToReachMedals;
        currentDifficulty.lifeCountToReachMedals = lifeCountToReachMedals;
    }

    private void UpdateUI () {
        difficulty_ind_image.color = currentDifficulty.MedalColor(currentDifficulty.currentMedal);
        score_bar.GetComponent<Image>().color = currentDifficulty.MedalColor(currentDifficulty.currentMedal + 1);
        score_bar.sizeDelta = new Vector2(40, 40 * (float) currentScore /
            currentDifficulty.scoresToReachMedals[Mathf.Min((int) currentDifficulty.currentMedal,
            currentDifficulty.scoresToReachMedals.Length - 1)]);
        high_score_bar.sizeDelta = new Vector2(40, 40 * (float) currentMedalHighScore /
            currentDifficulty.scoresToReachMedals[Mathf.Min((int) currentDifficulty.currentMedal,
            currentDifficulty.scoresToReachMedals.Length - 1)]);
    }

    private void UpdateDifficulty () {

    }

    public void LoseGame () {
        if (roundEnded) {
            return;
        }

        roundEnded = true;

        Debug.Log("GAME OVER");
        GameObject[] objects = SceneManager.GetSceneByName("MenuScene").GetRootGameObjects();
        foreach (GameObject obj in objects) {
            if (obj.name == "SceneController") {
                obj.GetComponent<SceneController>().HideCurrentScene();
                break;
            }
        }
    }

    public void ReduceLife() {
        if (lifeCount > 0) {
            lifeCount--;
        } else {
            LoseGame();
        }
    }

    private void RoundWin () {
        if (roundEnded) {
            return;
        }

        roundEnded = true;
        currentDifficulty.currentMedal++;
        currentMedalHighScore = 0;
        currentScore = 0;
        GameObject[] objects = SceneManager.GetSceneByName("MenuScene").GetRootGameObjects();
        foreach (GameObject obj in objects) {
            if (obj.name == "SceneController") {
                obj.GetComponent<SceneController>().ReloadCurrentScene();
                break;
            }
        }
    }

    public void Scored () {
        currentScore++;
        UpdateUI();
        currentMedalHighScore = Mathf.Max(currentScore, currentMedalHighScore);
        if (currentScore == currentDifficulty.scoresToReachMedals[Mathf.Min((int) currentDifficulty.currentMedal,
                            currentDifficulty.scoresToReachMedals.Length - 1)]) {
            if (currentDifficulty.currentMedal != STDifficulty.Medal.Platinium) {
                //New medal win
                RoundWin();
            } else {
                //Platinium ended
                GameObject[] objects = SceneManager.GetSceneByName("MenuScene").GetRootGameObjects();
                foreach (GameObject obj in objects) {
                    if (obj.name == "SceneController") {
                        obj.GetComponent<SceneController>().HideCurrentScene();
                        break;
                    }
                }
            }
        }
    }

}
