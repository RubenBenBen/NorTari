using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravitation : MonoBehaviour {

	// Use this for initialization
	public GameObject gravitation_source;

	[HideInInspector]
	public Rigidbody2D rb;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2 direction = (gravitation_source.transform.position - gameObject.transform.position) / SantasDutyMountainsInitializer.canvas_scale;
		direction = direction / Mathf.Pow(direction.magnitude, 3.0f);
			
		rb.AddForce(direction * gravitation_source.GetComponent<Planet>().gravitation * rb.mass * SantasDutyMountainsInitializer.canvas_scale);
	}
}
