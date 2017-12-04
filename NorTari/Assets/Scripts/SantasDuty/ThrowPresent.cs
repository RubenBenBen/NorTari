using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPresent : MonoBehaviour {

	// Use this for initialization
	public bool call_on_click = true;

	public float tangent_speed = 0.0f;
	public float vertical_speed = 0.0f;

	public float angular_speed = 0.0f;
	public Vector2 offset = new Vector2 (0.0f, 0.0f);

	public GameObject throwable;

	void Start () {
		
	}
	
	// Update is called once per frame

	public void TriggerThrow(){
		if (call_on_click && SantasDutyMountainsInitializer.number_of_lifes > 0) {
			OnThrow (tangent_speed, vertical_speed, throwable);
		}
	}

	void OnThrow(float tangent_sp, float vert_sp, GameObject throwable){
		SantasDutyMountainsInitializer.number_of_lifes -= 1;

		GameObject object_instance = GameObject.Instantiate (throwable);

		object_instance.transform.SetParent (GameObject.Find ("ThrownObjects").transform);
		object_instance.transform.position = gameObject.transform.position + gameObject.transform.rotation * offset;
		object_instance.transform.rotation = gameObject.transform.rotation;
		object_instance.transform.localScale = new Vector3 (SantasDutyMountainsInitializer.scale, SantasDutyMountainsInitializer.scale, SantasDutyMountainsInitializer.scale);
		Rigidbody2D rb = object_instance.GetComponent<Rigidbody2D> ();
        object_instance.SetActive(true);
        Vector2 rel_velocity = new Vector2 (tangent_sp, vert_sp) * SantasDutyMountainsInitializer.canvas_scale;

		rb.simulated = true;
		rb.velocity = object_instance.transform.rotation * rel_velocity;
		rb.angularVelocity = angular_speed;


    }
}
