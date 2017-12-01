using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlyingObject : MonoBehaviour {

    private FlyingTurkeysManager flyingTurkeysManager;

    void Awake () {
        flyingTurkeysManager = transform.parent.parent.GetComponent<FlyingTurkeysManager>();
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => {
            ObjectTapped();
        });
        trigger.triggers.Add(entry);
    }

    private void ObjectTapped () {
        flyingTurkeysManager.ObjectTapped(tag);
        Destroy(gameObject);
    }
}
