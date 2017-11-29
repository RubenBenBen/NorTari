using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InchBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.transform.position = gameObject.transform.position * Screen.dpi;
		gameObject.transform.localScale = gameObject.transform.localScale * Screen.dpi;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
