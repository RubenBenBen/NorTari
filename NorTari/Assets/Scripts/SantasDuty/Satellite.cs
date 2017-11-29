using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour {

	public GameObject core;
	public float speed = 10.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 position_vector = gameObject.transform.position - core.transform.position;
		float distance = position_vector.magnitude;
		float angle = Mathf.Atan2 (position_vector.y, position_vector.x);
		
		float new_angle = angle + speed * Time.deltaTime;
		Vector3 new_direction = new Vector3 (Mathf.Cos (new_angle), Mathf.Sin (new_angle), 0.0f);
		Vector3 position_new = core.transform.position + new_direction * distance;

		gameObject.transform.position = position_new;
		//gameObject.transform.localScale = new Vector3 (SantasDutyMountainsInitializer.scale, SantasDutyMountainsInitializer.scale, SantasDutyMountainsInitializer.scale);
		gameObject.transform.RotateAround (gameObject.transform.position, new Vector3 (0.0f, 0.0f, 1.0f),
			speed * Time.deltaTime * 180f / Mathf.PI);
	}

	void ThrowPresent(){
	}

}
