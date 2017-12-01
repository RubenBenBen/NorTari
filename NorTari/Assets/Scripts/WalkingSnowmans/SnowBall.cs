using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour {

    float velocity;
    float width;

    void Awake () {
        velocity = 800;
        width = GetComponent<RectTransform>().rect.width;
    }

    // Update is called once per frame
    void Update () {
        transform.position = new Vector2(transform.position.x, transform.position.y + velocity * Time.deltaTime);
        if (transform.position.y > 800 + width) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D (Collider2D collider) {
        if (collider.gameObject.tag == "Snowman") {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }
}
