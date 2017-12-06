using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class STManager : MonoBehaviour {

    private Transform flyingObjectsContainer;
    private STMedalProgressManager medalProgressManager;

    void Awake () {
        GetChildren();

        medalProgressManager.SetDifficulty(new int[] { 5, 5, 5, 5 }, new int[] { 3, 3, 3, 3 });
        medalProgressManager.gameObject.SetActive(true);
        transform.Find("LifeCount").gameObject.SetActive(true);
    }

    void Start () {

    }

    private void GetChildren () {
        flyingObjectsContainer = transform.Find("FlyingObjectsContainer");
        medalProgressManager = transform.Find("MedalProgressManager").GetComponent<STMedalProgressManager>();
    }

}
