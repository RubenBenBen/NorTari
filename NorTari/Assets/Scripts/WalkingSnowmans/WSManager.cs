using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WSManager : MonoBehaviour {

    public GameObject badSnowmanPrototype;
    public GameObject goodSnowmanPrototype;
    public GameObject snowballPrototype;
    private Transform walkingObjectsContainer;
    private Transform snowballContainer;

    private WSMedalProgressManager medalProgressManager;

    void Awake () {
        GetChildren();
        InitPrototypes();
        medalProgressManager.SetDifficulty(new int[] { 5, 5, 5, 5 }, new int[] { 3, 3, 3, 3}, new int[] {7, 7, 6, 5});
        medalProgressManager.gameObject.SetActive(true);
        transform.Find("LifeCount").gameObject.SetActive(true);
        transform.Find("SnowballCount").gameObject.SetActive(true);
    }

    void Start () {
        Invoke("CreateSnowman", 2);
    }

    private void CreateRandomSnowman () {
        float posY = Random.Range(badSnowmanPrototype.GetComponent<RectTransform>().rect.height / 2 + Screen.height * 75f / 800,
            Screen.height * 5f / 8 - badSnowmanPrototype.GetComponent<RectTransform>().rect.height / 2);
        bool fromRight = Random.Range(0, 2) == 1;
        CreateSnowmanAtPositionY(badSnowmanPrototype, posY, fromRight, Random.Range(200, 350));
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
        medalProgressManager = transform.Find("MedalProgressManager").GetComponent<WSMedalProgressManager>();
    }

    private void CreateSnowman () {
        float velocity;
        float posY = Random.Range(badSnowmanPrototype.GetComponent<RectTransform>().rect.height / 2 + Screen.height * 75f / 800,
        Screen.height * 5f / 8 - badSnowmanPrototype.GetComponent<RectTransform>().rect.height / 2);
        bool fromRight = Random.Range(0, 2) == 1;
        GameObject prototype = badSnowmanPrototype;
        switch (medalProgressManager.Difficulty().currentMedal) {
            case WSDifficulty.Medal.None:
                velocity = Random.Range(325, 375);
                break;
            case WSDifficulty.Medal.Bronze:
                velocity = Random.Range(375, 425);
                break;
            case WSDifficulty.Medal.Silver:
                velocity = Random.Range(425, 450);
                prototype = Random.Range(0, 2) == 0 ? badSnowmanPrototype : goodSnowmanPrototype;
                break;
            case WSDifficulty.Medal.Gold:
            case WSDifficulty.Medal.Platinium:
            default:
                velocity = Random.Range(450, 475);
                prototype = Random.Range(0, 2) == 0 ? badSnowmanPrototype : goodSnowmanPrototype;
                break;
        }
        CreateSnowmanAtPositionY(prototype, posY, fromRight, velocity);
    }

    private void CreateSnowmanAtPositionY (GameObject prototype, float posY, bool fromRight, float velocity) {
        GameObject object_instance = GameObject.Instantiate(prototype);
        float posX = fromRight ? Screen.width + prototype.GetComponent<RectTransform>().rect.width :
            - prototype.GetComponent<RectTransform>().rect.width;
        object_instance.transform.SetParent(walkingObjectsContainer);
        object_instance.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, -posY);
        object_instance.transform.localScale = Vector3.one;
        object_instance.GetComponent<WSSnowman>().fromRight = fromRight;
        if (fromRight) {
            object_instance.GetComponent<RectTransform>().Rotate(new Vector3(0, 180, 0));
        }
        object_instance.GetComponent<WSSnowman>().velocity = velocity;
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
        object_instance.SetActive(true);
    }

    public void MissedSnowman (string tag) {
        if (tag == "BadSnowman") {
            medalProgressManager.ReduceLife();
        }
        Invoke("CreateSnowman", 1);
    }

    public void MissedSnowball () {
        if (medalProgressManager.snowballCount == 0) {
            medalProgressManager.LoseGame();
        }
        //medalProgressManager.ReduceLife();
    }

    public void SnowmanDestroyed (string snowmanTag) {
        if (snowmanTag == "BadSnowman") {
            medalProgressManager.Scored();
        } else if (snowmanTag == "GoodSnowman") {
            medalProgressManager.ReduceLife();
        }
        Invoke("CreateSnowman", 1);
        if (medalProgressManager.snowballCount == 0) {
            medalProgressManager.LoseGame();
        }
    }

    public void OnTap () {
        float posX;
        if (Input.touchCount > 0) {
            posX = Input.GetTouch(0).position.x;
        } else {
            posX = Input.mousePosition.x;
        }

        if (medalProgressManager.snowballCount > 0) {
            CreateSnowballAtPositionX(snowballPrototype, posX);
            medalProgressManager.ReduceSnowballCount();
        }
    }
}
