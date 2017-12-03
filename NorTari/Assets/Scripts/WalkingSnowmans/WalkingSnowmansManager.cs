using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSnowmansManager : MonoBehaviour {

    public GameObject badSnowmanPrototype;
    public GameObject goodSnowmanPrototype;
    public GameObject snowballPrototype;
    private Transform walkingObjectsContainer;
    private Transform snowballContainer;
    private MedalProgressManager medalProgressManager;

    void Awake () {
        GetChildren();
        InitPrototypes();
    }

    void Start () {
        InvokeRepeating("CreateRandomSnowman", 0, 1);
    }

    private void CreateRandomSnowman () {
        float posY = Random.Range(badSnowmanPrototype.GetComponent<RectTransform>().rect.height / 2,
            Screen.height - badSnowmanPrototype.GetComponent<RectTransform>().rect.height / 2);
        bool fromRight = Random.Range(0, 2) == 1;
        CreateSnowmanAtPositionY(badSnowmanPrototype, posY, fromRight);
    }

    private void InitPrototypes () {
        Transform prototypesContainer = transform.Find("Prototypes");
        foreach (Transform prototypeTransform in prototypesContainer) {
            prototypeTransform.GetComponent<WSPrototypeManager>().InitPrototype();
        }
    }

    private void GetChildren () {
        walkingObjectsContainer = transform.Find("WalkingObjectsContainer");
        snowballContainer = transform.Find("SnowballContainer");
        medalProgressManager = transform.Find("MedalProgressManager").GetComponent<MedalProgressManager>();
    }

    private void CreateSnowmanAtPositionY (GameObject prototype, float posY, bool fromRight) {
        GameObject object_instance = GameObject.Instantiate(prototype);
        float posX = fromRight ? Screen.width + prototype.GetComponent<RectTransform>().rect.width :
            - prototype.GetComponent<RectTransform>().rect.width;
        object_instance.transform.SetParent(walkingObjectsContainer);
        object_instance.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, -posY);
        object_instance.transform.localScale = Vector3.one;
        object_instance.GetComponent<Snowman>().fromRight = fromRight;
        if (fromRight) {
            object_instance.GetComponent<RectTransform>().Rotate(new Vector3(0, 180, 0));
        }
        object_instance.SetActive(true);
    }

    private void CreateSnowballAtPositionX (GameObject prototype, float posX) {
        float prototypeWidth = prototype.GetComponent<RectTransform>().rect.width;
        float prototypeHeight = prototype.GetComponent<RectTransform>().rect.height;
        posX = Mathf.Min(Mathf.Max(posX, prototypeWidth/2), Screen.width - prototypeWidth/2);
        GameObject object_instance = GameObject.Instantiate(prototype);
        object_instance.transform.SetParent(snowballContainer);
        object_instance.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, -prototypeHeight);
        object_instance.transform.localScale = Vector3.one;
        object_instance.GetComponent<SnowBall>().walkingSnowmansManager = this;
        object_instance.SetActive(true);
    }

    public void SnowmanDestroyed () {
        medalProgressManager.Scored();
    }

    public void OnTap () {
        float posX;
        if (Input.touchCount > 0) {
            posX = Input.GetTouch(0).position.x;
        } else {
            posX = Input.mousePosition.x;
        }
        
        CreateSnowballAtPositionX(snowballPrototype, posX);
    }
}
