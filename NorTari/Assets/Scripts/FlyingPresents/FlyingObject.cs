using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlyingObject : MonoBehaviour {

    private FlyingPresents flyingPresentsManager;

    void Awake () {
        flyingPresentsManager = transform.parent.parent.GetComponent<FlyingPresents>();
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => {
            ObjectTapped();
        });
        trigger.triggers.Add(entry);
    }

    private void ObjectTapped () {
        flyingPresentsManager.ObjectTapped(tag);
        Destroy(gameObject);
    }
}
