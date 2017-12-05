using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WSSnowball : MonoBehaviour {

    float velocity;
    float width;
    Rigidbody2D rigidBody;
    float screenHeight;
    public WSManager walkingSnowmansManager;

    void Awake () {
        velocity = Screen.height * 1.25f;
        screenHeight = Screen.height;
        width = GetComponent<RectTransform>().rect.width;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        //rigidBody.MovePosition(Vector2.up);
        transform.position = new Vector2(transform.position.x, transform.position.y + velocity * Time.deltaTime);
        if (transform.position.y > screenHeight + width) {
            Destroy(gameObject);
            walkingSnowmansManager.MissedSnowball();
        }
    }

    void OnTriggerEnter2D (Collider2D collider) {
        if (collider.gameObject.tag == "BadSnowman" || collider.gameObject.tag == "GoodSnowman") {
            Destroy(collider.gameObject);
            Destroy(gameObject);
            walkingSnowmansManager.SnowmanDestroyed(collider.gameObject.tag);
        }
    }
}
