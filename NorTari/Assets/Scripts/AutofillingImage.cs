using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutofillingImage : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        float screenScale = (float) Screen.width / Screen.height;
        float imageScale = (float) GetComponent<Image>().sprite.texture.width / GetComponent<Image>().sprite.texture.height;
        if (screenScale > imageScale) {
            GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.width / imageScale);
        } else {
            GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height * imageScale, Screen.height);
        }
    }
}
