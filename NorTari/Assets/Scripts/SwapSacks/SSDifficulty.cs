using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDifficulty {

    public int[] scoresToReachMedals;
    public int[] lifeCountToReachMedals;
    public Medal currentMedal;

    public enum Medal {
        None = 0,
        Bronze,
        Silver,
        Gold,
        Platinium
    }

    public Color MedalColor (Medal currMedal) {
        Color medalColor = new Color();
        switch (currMedal) {
            case Medal.None:
                medalColor = Color.white;
                break;
            case Medal.Bronze:
                medalColor = StaticMethodsAndProperties.hexToColor("#CD7F32");
                break;

            case Medal.Silver:
                medalColor = StaticMethodsAndProperties.hexToColor("#C0C0C0");
                break;

            case Medal.Gold:
                medalColor = StaticMethodsAndProperties.hexToColor("#ffd700");
                break;

            case Medal.Platinium:
            default:
                medalColor = StaticMethodsAndProperties.hexToColor("#e5e4e2");
                break;
        }
        return medalColor;
    }
}
