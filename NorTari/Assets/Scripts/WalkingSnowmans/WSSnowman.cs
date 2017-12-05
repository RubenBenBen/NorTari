using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WSSnowman : MonoBehaviour {

    public bool fromRight;
    public float velocity;
    float screenWidth;
    float width;
    public WSManager walkingSnowmansManager;

    void Awake () {
        int sign = fromRight ? -1 : 1;
        velocity *= sign;
        screenWidth = Screen.width;
        width = GetComponent<RectTransform>().rect.width;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector2(transform.position.x + velocity * Time.deltaTime, transform.position.y);
        if ((!fromRight && transform.position.x > screenWidth + width / 2) || ( fromRight && transform.position.x < - width / 2 )) {
            Destroy(gameObject);
            walkingSnowmansManager.MissedSnowman(gameObject.tag);
        }
	}
}
