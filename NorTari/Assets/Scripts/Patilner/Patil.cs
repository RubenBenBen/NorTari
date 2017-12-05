using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Patil : MonoBehaviour {

    private float rotationRate;
    private Color _color;
    private CPManager patilnerManager;
    private Transform patilImageTransform;
    public Color color {
        get {
            return _color;
        }
        set {
            _color = value;
            transform.Find("Image").GetComponent<Image>().color = value;
            ResetRotation();
        }
    }

    void Awake () {
        patilImageTransform = transform.Find("Image");
        patilnerManager = transform.parent.parent.parent.GetComponent<CPManager>();
        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => {
            OnPointerDownDelegate((PointerEventData) data);
        });
        trigger.triggers.Add(entry);
    }

    public void OnPointerDownDelegate (PointerEventData data) {
        patilnerManager.chosenColor = color;
    }

    private void ResetRotation () {
        int rotationSign = Random.Range(0, 2) * 2 - 1;
        rotationRate = Random.Range(5, 10) * rotationSign;
    }
	
	// Update is called once per frame
	void Update () {
        patilImageTransform.Rotate(0, 0, rotationRate);
	}
}
