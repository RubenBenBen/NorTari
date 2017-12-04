using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreOnCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "ScoreOnCollision") {
			int child_count = coll.gameObject.transform.childCount;
			bool has_score = false;
			for (int i = 0; i < child_count; i++) {
				Image image = coll.gameObject.transform.GetChild(i).GetComponent<Image> ();
				if (image.enabled) {
					has_score = true;
				}
				image.enabled = true;
			}
			if (!has_score) {
				SantasDutyMountainsInitializer.score += 1;
				if (SantasDutyMountainsInitializer.score > SantasDutyMountainsInitializer.high_score) {
					SantasDutyMountainsInitializer.high_score = SantasDutyMountainsInitializer.score;
				}
			}
		}

		if (SantasDutyMountainsInitializer.score >= SantasDutyMountainsInitializer.max_score) {
			SantasDutyMountainsInitializer.high_score = 0;
			SantasDutyMountainsInitializer.difficulty = SantasDutyMountainsInitializer.difficulty + 1;
            GameObject[] objects = SceneManager.GetSceneByName("MenuScene").GetRootGameObjects();
            foreach (GameObject obj in objects) {
                if (obj.name == "SceneController") {
                    obj.GetComponent<SceneController>().ReloadCurrentScene();
                    break;
                }
            }
        }

		Destroy (gameObject);
		SantasDutyMountainsInitializer.latent_num_of_lifes -= 1;

		if (SantasDutyMountainsInitializer.latent_num_of_lifes <= 0) {
            GameObject[] objects = SceneManager.GetSceneByName("MenuScene").GetRootGameObjects();
            foreach (GameObject obj in objects) {
                if (obj.name == "SceneController") {
                    obj.GetComponent<SceneController>().ReloadCurrentScene();
                    break;
                }
            }
        }

	}
}
