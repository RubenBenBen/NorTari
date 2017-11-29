using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyIndicator : MonoBehaviour {

	// Use this for initialization
	[HideInInspector]
	Image difficulty_ind_image;
	RectTransform score_bar;
	RectTransform high_score_bar;

	void Start () {
		int difficulty = SantasDutyMountainsInitializer.difficulty;

		difficulty_ind_image = GameObject.Find ("DifficultyIndicator").GetComponent<Image> ();
		score_bar = GameObject.Find ("ScoreBar").GetComponent<RectTransform> ();
		high_score_bar = GameObject.Find ("HighScore").GetComponent<RectTransform> ();

		switch (difficulty) {
		case 0:
			difficulty_ind_image.color = new Vector4 (0.8f, 0.4f, 0.1f, 1.0f);
			break;

		case 1:
			difficulty_ind_image.color = new Vector4 (0.6f, 0.6f, 0.7f, 1.0f);
			break;

		case 2:
			difficulty_ind_image.color = new Vector4 (1.0f, 1.0f, 0.0f, 1.0f);
			break;

		case 3:
			difficulty_ind_image.color = new Vector4 (1.0f, 1.0f, 1.0f, 1.0f);
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		score_bar.sizeDelta = new Vector2(40,40 * SantasDutyMountainsInitializer.score / SantasDutyMountainsInitializer.max_score);
		high_score_bar.sizeDelta = new Vector2(40,40 * SantasDutyMountainsInitializer.high_score / SantasDutyMountainsInitializer.max_score);
	}
}
