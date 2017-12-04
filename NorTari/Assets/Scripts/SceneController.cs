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

    private bool reloadingInProgress;

    public IEnumerator LoadDailyScene () {
        yield return StartCoroutine(LoadScene(levelInfoManager.dailyLevels[levelInfoManager.currentDayIndex], false));
        menuSceneManager.dailyLoaded = true;
        menuSceneManager.OnScenesLoaded();
    }

    public void OpenDailyScene () {
        StartCoroutine(LoadScene(levelInfoManager.dailyLevels[levelInfoManager.currentDayIndex],
            true));
    }

    private void ShowCanvasOfScene (Scene scene) {
        GameObject[] objects = scene.GetRootGameObjects();
        foreach (GameObject obj in objects) {
            if (obj.name == "Canvas") {
                obj.SetActive(true);
                return;
            }
        }
    }

    private IEnumerator LoadScene (string sceneName, bool show) {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (!scene.isLoaded) {
            AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while (!asyncLoadScene.isDone) {
                yield return null;
            }
            if (show) {
                SceneManager.SetActiveScene(scene);
                ShowCanvasOfScene(scene);
            }
        } else {
            yield return StartCoroutine(ReloadScene(scene, true));
        }
    }

    private IEnumerator ReloadScene (Scene scene, bool showCanvas) {
        if (reloadingInProgress) {
            yield break;
        }
        reloadingInProgress = true;
        int currentSceneIndex = scene.buildIndex;
        string currentSceneName = scene.name;

        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive);
        while (!asyncLoadScene.isDone) {
            yield return null;
        }

        AsyncOperation asyncUnloadScene = SceneManager.UnloadSceneAsync(currentSceneIndex);
        while (!asyncUnloadScene.isDone) {
            yield return null;
        }

        Scene currentScene = SceneManager.GetSceneByName(currentSceneName);
        if (showCanvas) {
            SceneManager.SetActiveScene(currentScene);
            ShowCanvasOfScene(currentScene);
        }
        reloadingInProgress = false;
    }

    public void HideCurrentScene () {
        GameObject[] objects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in objects) {
            obj.SetActive(false);
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MenuScene"));
    }

    public void ReloadCurrentScene () {
        StartCoroutine(ReloadScene(SceneManager.GetActiveScene(), true));
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
