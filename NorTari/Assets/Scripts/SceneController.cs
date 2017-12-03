using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


////USE THIS CODE TO RELOAD CURRENT SCENE
/*
GameObject[] objects = SceneManager.GetSceneByName("MenuScene").GetRootGameObjects();
foreach (GameObject obj in objects) {
    if (obj.name == "SceneController") {
        obj.GetComponent<SceneController>().ReloadCurrentScene ();
        break;
    }
}
*/
////

////USE THIS CODE TO HIDE CURRENT SCENE
/*
GameObject[] objects = SceneManager.GetSceneByName("MenuScene").GetRootGameObjects();
foreach (GameObject obj in objects) {
    if (obj.name == "SceneController") {
        obj.GetComponent<SceneController>().HideCurrentScene ();
        break;
    }
}
*/
////

public class SceneController : MonoBehaviour {

    public MenuSceneManager menuSceneManager;
    public LevelInfoManager levelInfoManager;
    public GameObject[] blitzSceneCanvasArray;
    public GameObject dailySceneCanvas;

    private bool reloadingInProgress;

    public IEnumerator LoadDailyScene (string sceneName) {
        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!asyncLoadScene.isDone) {
            yield return null;
        }
        GameObject[] objects = SceneManager.GetSceneByName(sceneName).GetRootGameObjects();
        foreach (GameObject obj in objects) {
            if (obj.name == "Canvas") {
                obj.SetActive(false);
                dailySceneCanvas = obj;
                break;
            }
        }
        //Debug.Log("Daily Loaded");
        menuSceneManager.dailyLoaded = true;
        menuSceneManager.OnScenesLoaded();
    }

    public IEnumerator LoadBlitzScenes (string[] sceneNames) {
        blitzSceneCanvasArray = new GameObject[sceneNames.Length];
        for (int i = 0 ; i < sceneNames.Length ; i++) {
            AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(sceneNames[i], LoadSceneMode.Additive);
            while (!asyncLoadScene.isDone) {
                yield return null;
            }
            GameObject[] objects = SceneManager.GetSceneByName(sceneNames[i]).GetRootGameObjects();
            foreach (GameObject obj in objects) {
                if (obj.name == "Canvas") {
                    obj.SetActive(false);
                    blitzSceneCanvasArray[i] = obj;
                    break;
                }
            }
            //Debug.Log("Blitz Scene " + i + " loaded");
        }
        menuSceneManager.blitzLoaded = true;
        menuSceneManager.OnScenesLoaded();
    }

    public void OpenDailyScene () {
        Scene dailyScene = SceneManager.GetSceneByName(levelInfoManager.dailyLevels[levelInfoManager.currentDayIndex]);
        SceneManager.SetActiveScene(dailyScene);
        dailySceneCanvas.SetActive(true);
    }

    public void HideCurrentScene () {
        GameObject[] objects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in objects) {
            obj.SetActive(false);
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MenuScene"));
    }

    public void ReloadCurrentScene () {
        StartCoroutine(ReloadActiveScene());
    }

    private IEnumerator ReloadActiveScene () {
        if (reloadingInProgress) {
            yield return null;
        }
        reloadingInProgress = true;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        string currentSceneName = SceneManager.GetActiveScene().name;

        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive);
        while (!asyncLoadScene.isDone) {
            yield return null;
        }

        AsyncOperation asyncUnloadScene = SceneManager.UnloadSceneAsync(currentSceneIndex);
        while (!asyncUnloadScene.isDone) {
            yield return null;
        }

        Scene currentScene = SceneManager.GetSceneByName(currentSceneName);
        GameObject[] objects = currentScene.GetRootGameObjects();
        foreach (GameObject obj in objects) {
            if (obj.name == "Canvas") {
                obj.SetActive(true);
                break;
            }
        }
        SceneManager.SetActiveScene(currentScene);
        reloadingInProgress = false;
    }

    public void OpenNextBlitzScene () {
        levelInfoManager.currentBlitzGameIndex++;
        if (levelInfoManager.currentBlitzGameIndex >= levelInfoManager.blitzLevelsCount) {
            levelInfoManager.currentBlitzGameIndex = 0;
        }
        Scene nextBlitzScene = SceneManager.GetSceneByName(levelInfoManager.blitzLevels[levelInfoManager.currentDayIndex]
            [levelInfoManager.currentBlitzGameIndex]);
        SceneManager.SetActiveScene(nextBlitzScene);
        blitzSceneCanvasArray[levelInfoManager.currentBlitzGameIndex].SetActive(true);
    }
}
