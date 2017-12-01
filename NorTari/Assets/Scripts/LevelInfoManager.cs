using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfoManager : MonoBehaviour {

    public string[][] blitzLevels;
    public string[] dailyLevels;
    private int dailyLevelsCount;
    private int blitzCount;

    public int blitzLevelsCount;
    public int currentDayIndex;
    public int currentBlitzGameIndex;

    public MenuSceneManager menuSceneManager;

    void Awake () {
        SetValues();
        InitDailyLevels();
        InitBlitzLevels();
        GetCurrentDay();
        menuSceneManager.LoadScenes();
    }

    private void SetValues () {
        currentBlitzGameIndex = -1;
        blitzLevelsCount = 4;
        blitzCount = 30;
        dailyLevelsCount = 30;
    }

    private void GetCurrentDay () {
        currentDayIndex = 0;
    }

    private void InitDailyLevels () {
        dailyLevels = new string[dailyLevelsCount];
        dailyLevels[0] = "Patilner";
        dailyLevels[1] = "SantasDuty_Mountains";
        dailyLevels[2] = "FlyingTurkeys";
    }

    private void InitBlitzLevels () {
        blitzLevels = new string[blitzCount][];
		blitzLevels[0] = new string[]{ "SantasDuty_Mountains", "Scene3", "Scene4", "Scene5"};

    }
}
