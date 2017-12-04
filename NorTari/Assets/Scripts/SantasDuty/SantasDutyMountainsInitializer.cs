using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantasDutyMountainsInitializer : MonoBehaviour {
	// Use this for initialization
	public GameObject planet;

	public List <GameObject> objects_to_scale;
	public static int difficulty = 0;
	public static bool blizz_mode = false;

	[HideInInspector]
	public static int number_of_circles = 0;
	public static int number_of_buildings = 0;
	public static int score = 0;
	public static int max_score = 0;
	public static float scale = 0.0f;
	public static int number_of_lifes = 0;
	public static int latent_num_of_lifes = 0;
	public static int high_score = 0;

	public static float canvas_scale;

    public GameObject scoreCanvas;
    public GameObject lifeIndicatorCanvas;

	void Awake() {
		
		number_of_circles = 0;
		number_of_buildings = 0;
		score = 0;

		if (blizz_mode) {
			number_of_circles = 1;
		} else {
			number_of_circles = new int[] {2, 3, 4, 5} [difficulty];
		}
		number_of_buildings = new int[] {3, 5, 7, 9} [difficulty];
		scale = new float[] {0.75f, 0.65f, 0.55f, 0.45f} [difficulty];
		number_of_lifes = new int[] { 5, 7, 9, 11 } [difficulty];
		max_score = number_of_buildings;
		latent_num_of_lifes = number_of_lifes;

        scoreCanvas.SetActive (true);
		lifeIndicatorCanvas.SetActive (true);
	}

	void Start () {
		foreach (GameObject obj in objects_to_scale){
			obj.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
		}
        canvas_scale = gameObject.GetComponent<RectTransform>().localScale.x;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
