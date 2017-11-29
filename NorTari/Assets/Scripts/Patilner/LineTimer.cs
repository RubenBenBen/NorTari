using UnityEngine;
using System.Collections;

public class LineTimer : MonoBehaviour {

    private float width;
    private float rate;
    private RectTransform rect;
    private bool timerIsOn;
    private PatilnerManager patilnerManager;

    void Awake () {
        rect = transform.GetComponent<RectTransform>();
        width = rect.sizeDelta.x;
        patilnerManager = transform.parent.GetComponent<PatilnerManager>();

    }

    void Update () {
        if (timerIsOn) {
            if (rect.sizeDelta.x > 0) {
                rect.sizeDelta = new Vector2(rect.sizeDelta.x - rate * Time.deltaTime, rect.sizeDelta.y);
            } else {
                TurnOffTimer();
            }
        }
    }

    public void TurnOffTimer () {
        gameObject.SetActive(false);
        timerIsOn = false;
        rect.sizeDelta = new Vector2(width, rect.sizeDelta.y);
        patilnerManager.LostRound();
    }

    public void ShowTimer (float seconds) {
        gameObject.SetActive(true);
        rect.sizeDelta = new Vector2(width, rect.sizeDelta.y);
        rate = width / seconds;
        timerIsOn = true;
    }
}