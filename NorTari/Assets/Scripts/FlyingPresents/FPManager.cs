using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPManager : MonoBehaviour {


    public GameObject presentPrototype;
    private Transform flyingObjectsContainer;
    private FPMedalProgressManager medalProgressManager;

    void Awake () {
        GetChildren();
        InitPrototypes();

        medalProgressManager.SetDifficulty(new int[] { 5, 5, 5, 5 }, new int[] { 3, 3, 3, 3 });
        medalProgressManager.gameObject.SetActive(true);
        transform.Find("LifeCount").gameObject.SetActive(true);
    }

    void Start () {
        Invoke("CreatePresent", 1.5f);
    }

    private void GetChildren () {
        flyingObjectsContainer = transform.Find("FlyingObjectsContainer");
        medalProgressManager = transform.Find("MedalProgressManager").GetComponent<FPMedalProgressManager>();
    }

    private void InitPrototypes () {
        Transform prototypesContainer = transform.Find("Prototypes");
        foreach (Transform prototypeTransform in prototypesContainer) {
            prototypeTransform.GetComponent<FPPrototypeManager>().InitPrototype();
        }
    }

    private void CreatePresent () {
        bool fromRight = Random.Range(0, 2) == 1;
        GameObject prototype = presentPrototype;
        switch (medalProgressManager.Difficulty().currentMedal) {
            case FPDifficulty.Medal.None:
                CreatePrototypeAtPositionY(presentPrototype, -150, fromRight, 25000);
                break;
            case FPDifficulty.Medal.Bronze:
                CreatePrototypeAtPositionY(presentPrototype, -150, fromRight, 25000);
                break;
            case FPDifficulty.Medal.Silver:
                CreatePrototypeAtPositionY(presentPrototype, -150, fromRight, 25000);
                break;
            case FPDifficulty.Medal.Gold:
            case FPDifficulty.Medal.Platinium:
            default:
                CreatePrototypeAtPositionY(presentPrototype, -150, fromRight, 25000);
                break;
        }
    }

    private void CreatePrototypeAtPositionY (GameObject prototype, float posY, bool fromRight, float velocityX = 0) {
        GameObject object_instance = GameObject.Instantiate(prototype);
        float posX = fromRight ? 300 : 200;
        posX = fromRight ? 615 : -125;
        int sign = fromRight ? -1 : 1;
        object_instance.transform.SetParent(flyingObjectsContainer);
        object_instance.GetComponent<RectTransform>().position = new Vector2(posX, posY);
        object_instance.transform.localScale = Vector3.one;
        object_instance.GetComponent<FPFlyingObject>().fromRight = fromRight;
        object_instance.SetActive(true);
        object_instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(velocityX * sign, velocityX));
    }

    public void MissedPresent () {
        medalProgressManager.ReduceLife();
        CreatePresent();
    }

    public void ObjectTapped (string objectTag) {
        Debug.Log(objectTag);
        switch (objectTag) {
            case "Present":
                medalProgressManager.Scored();
                CreatePresent();
                break;
            default:
                break;
        }
    }

}
