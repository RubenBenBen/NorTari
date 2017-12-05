using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingPresents : MonoBehaviour {


    public GameObject presentPrototype;
    private Transform flyingObjectsContainer;

    void Awake () {
        GetChildren();
        InitPrototypes();
    }

    void Start () {
        CreatePrototypeAtPositionY(presentPrototype, 0, true, 20000);
        CreatePrototypeAtPositionY(presentPrototype, -150, false, 25000);
    }

    private void GetChildren () {
        flyingObjectsContainer = transform.Find("FlyingObjectsContainer");
    }

    private void InitPrototypes () {
        Transform prototypesContainer = transform.Find("Prototypes");
        foreach (Transform prototypeTransform in prototypesContainer) {
            prototypeTransform.GetComponent<PrototypeManager>().InitPrototype();
        }
    }

    private void CreatePrototypeAtPositionY (GameObject prototype, float posY, bool fromRight, float velocityX = 0) {
        GameObject object_instance = GameObject.Instantiate(prototype);
        float posX = fromRight ? 200 : 300;
        posX = fromRight ? -125 : 615;
        int sign = fromRight ? 1 : -1;
        object_instance.transform.SetParent(flyingObjectsContainer);
        object_instance.GetComponent<RectTransform>().position = new Vector2(posX, posY);
        object_instance.transform.localScale = Vector3.one;
        object_instance.SetActive(true);
        object_instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(velocityX * sign, velocityX));
    }

    public void ObjectTapped (string objectTag) {
        Debug.Log(objectTag);
        switch (objectTag) {
            case "Turkey":
                break;
            default:
                break;
        }
    }

}
