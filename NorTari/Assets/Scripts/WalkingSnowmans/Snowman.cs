using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : MonoBehaviour {

    public bool fromRight;
    float velocity;

    void Awake () {
        int sign = fromRight ? 1 : -1;
        velocity = Random.Range(200, 350) * sign;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector2(transform.position.x + velocity * Time.deltaTime, transform.position.y);
        if ((fromRight && transform.position.x > 480 + 50) || ( !fromRight && transform.position.x < -50 )) {
            Destroy(gameObject);
        }
	}
}
