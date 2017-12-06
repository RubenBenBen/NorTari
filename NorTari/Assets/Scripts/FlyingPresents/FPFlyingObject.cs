using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FPFlyingObject : MonoBehaviour {

    private FPManager flyingPresentsManager;

    private float objectSide;
    private float screenWidth;
    public bool fromRight;

    void Awake () {
        flyingPresentsManager = transform.parent.parent.GetComponent<FPManager>();
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => {
            ObjectTapped();
        });
        trigger.triggers.Add(entry);
        objectSide = GetComponent<RectTransform>().sizeDelta.x;
        screenWidth = Screen.width;
    }

    private void ObjectTapped () {
        flyingPresentsManager.ObjectTapped(tag);
        Destroy(gameObject);
    }

    void Update () {
        if (( fromRight && transform.position.x <= 0 - objectSide / 2f - 25f ) ||
            ( !fromRight && transform.position.x >= screenWidth + objectSide / 2f + 25f )) {
            Destroy(gameObject);
            flyingPresentsManager.MissedPresent();
        }
    }
}
