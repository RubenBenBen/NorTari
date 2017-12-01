using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSnowmansManager : MonoBehaviour {

    public GameObject snowmanPrototype;
    public GameObject snowballPrototype;
    private Transform walkingObjectsContainer;
    private Transform snowballContainer;

    void Awake () {
        GetChildren();
    }

    void Start () {
        InvokeRepeating("CreateRandomSnowman", 0, 1);
    }

    private void CreateRandomSnowman () {
        float posY = Random.Range(50, 800 - 200);
        bool fromRight = Random.Range(0, 2) == 1;
        CreateSnowmanAtPositionY(snowmanPrototype, posY, fromRight);
    }

    private void GetChildren () {
        walkingObjectsContainer = transform.Find("WalkingObjectsContainer");
        snowballContainer = transform.Find("SnowballContainer");
    }

    private void CreateSnowmanAtPositionY (GameObject prototype, float posY, bool fromRight) {
        GameObject object_instance = GameObject.Instantiate(prototype);
        float posX = fromRight ? 200 : 300;
        posX = fromRight ? -100 : 580;
        object_instance.transform.SetParent(walkingObjectsContainer);
        object_instance.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, -posY);
        object_instance.transform.localScale = Vector3.one;
        object_instance.GetComponent<Snowman>().fromRight = fromRight;
        object_instance.SetActive(true);
    }

    private void CreateSnowballAtPositionX (GameObject prototype, float posX) {
        posX = Mathf.Min(Mathf.Max(posX, 12.5f), 480 - 12.5f);
        GameObject object_instance = GameObject.Instantiate(prototype);
        object_instance.transform.SetParent(snowballContainer);
        object_instance.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, -25);
        object_instance.transform.localScale = Vector3.one;
        object_instance.SetActive(true);
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
