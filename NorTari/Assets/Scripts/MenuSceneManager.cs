using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneManager : MonoBehaviour {

    public SceneController sceneController;
    public LevelInfoManager levelInfoManager;
    public bool dailyLoaded;
    public bool blitzLoaded;

    public void LoadScenes () {
        StartCoroutine(sceneController.LoadDailyScene());
        StartCoroutine(sceneController.LoadBlitzScenes(levelInfoManager.blitzLevels[levelInfoManager.currentDayIndex]));
    }

    public void DailyPressed () {
        sceneController.OpenDailyScene();
    }

    public void BlitzPressed () {
        sceneController.OpenNextBlitzScene();
    }

    private void turnLoadingScreenOff () {
        //Debug.Log("Turn loading screen off");
        transform.Find("LoadingPanel").gameObject.SetActive(false);
    }

    public void OnScenesLoaded () {
        if (!blitzLoaded || !dailyLoaded)
            return;

        turnLoadingScreenOff();
    }

}
