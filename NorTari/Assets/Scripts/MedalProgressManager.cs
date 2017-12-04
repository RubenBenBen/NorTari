using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MedalProgressManager : MonoBehaviour {

    private Image difficulty_ind_image;
    private RectTransform high_score_bar;
    private RectTransform score_bar;
    private static Difficulty currentDifficulty;

    private int currentScore = 0;
    private static int currentMedalHighScore;

    void Awake () {
        GetChildren();
        if (currentDifficulty == null) {
            SetDifficulty();
        }
        UpdateUI();
    }

    private void GetChildren () {
        high_score_bar = transform.Find("HighScore").GetComponent<RectTransform>();
        score_bar = transform.Find("CurrentScore").GetComponent<RectTransform>();
        difficulty_ind_image = transform.Find("CurrentMedal").GetComponent<Image>();
    }

    private void SetDifficulty () {
        currentDifficulty = new Difficulty();
        currentDifficulty.scoresToReachMedals = new int[] { 2, 3, 4, 6 };
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

    public void Scored () {
        currentScore++;
        currentMedalHighScore = Mathf.Max(currentScore, currentMedalHighScore);
        if (currentScore == currentDifficulty.scoresToReachMedals[Mathf.Min((int) currentDifficulty.currentMedal,
    currentDifficulty.scoresToReachMedals.Length - 1)]) {
            if (currentDifficulty.currentMedal != Difficulty.Medal.Platinium) {
                //New medal win
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
        UpdateUI();
    }

}
