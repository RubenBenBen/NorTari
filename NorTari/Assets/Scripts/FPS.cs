using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPS : MonoBehaviour {

    private float deltaTime = 0.0f;
    private Text textLabel;

    void Awake () {
        textLabel = transform.GetComponent<Text>();
    }

    void Update () {
        deltaTime += ( Time.deltaTime - deltaTime ) * 0.1f;
    }

    void OnGUI () {
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string fpsText = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        textLabel.text = fpsText;
    }
}