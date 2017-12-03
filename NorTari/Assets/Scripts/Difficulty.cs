using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty {

    public int[] scoresToReachMedals;
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
            case Medal.Bronze:
                medalColor = new Color(0.8f, 0.4f, 0.1f, 1.0f);
                break;

            case Medal.Silver:
                medalColor = new Color(0.6f, 0.6f, 0.7f, 1.0f);
                break;

            case Medal.Gold:
                medalColor = new Color(1.0f, 1.0f, 0.0f, 1.0f);
                break;

            case Medal.Platinium:
            default:
                medalColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                break;
        }
        return medalColor;
    }
}
