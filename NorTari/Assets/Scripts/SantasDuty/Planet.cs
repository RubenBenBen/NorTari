using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

	public List<float> rotation_speeds;
	public float gravitation = 0.1f;

	public GameObject building_type;

	[HideInInspector]
	public float rotation_speed;
	// Use this for initialization
	void Start () {
		List <Vector2> Listofpos = new List <Vector2>();
		List <float> Listofangles = new List <float> ();


		rotation_speed = rotation_speeds [SantasDutyMountainsInitializer.difficulty];
		PolygonCollider2D pcl = gameObject.GetComponent<PolygonCollider2D>();

		Vector2[] planet_vertices = pcl.points;
		// Randomly place buildings on the surface, so that no 2 building are in the same position
		while (Listofangles.Count < SantasDutyMountainsInitializer.number_of_buildings){
			while (true) {
				Vector2 position = planet_vertices[Random.Range(0, planet_vertices.Length - 1)];
				float new_position = (float)Mathf.Atan2(position.y, position.x);
				bool is_valid = true;

				foreach (float angle in Listofangles){
					if (Mathf.Abs (new_position - angle) < Mathf.PI / SantasDutyMountainsInitializer.number_of_buildings) {
						is_valid = false;
					}
				}

				if (is_valid) {
					Listofpos.Add (position);
					Listofangles.Add (new_position);
					break;
				}
			}
		}
			

		for (int i = 0; i < SantasDutyMountainsInitializer.number_of_buildings; i++) {
			GameObject new_house = GameObject.Instantiate (building_type);
			Transform nht = new_house.transform;
			new_house.transform.SetParent (gameObject.transform.Find("PlanetSurface"));

			nht.localPosition = (Vector3)(Listofpos[i]);

			nht.eulerAngles = new Vector3 (0.0f, 0.0f, Listofangles[i]/ Mathf.PI * 180.0f - 90.0f);
			nht.localScale = new Vector3 (SantasDutyMountainsInitializer.scale, SantasDutyMountainsInitializer.scale, SantasDutyMountainsInitializer.scale);
		}


		// gameObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
	}
		
	// Update is called once per frame
	void Update () {
		gameObject.transform.RotateAround (gameObject.transform.position,
			new Vector3 (0.0f, 0.0f, 1.0f),
			rotation_speed * Time.deltaTime);
	}
}
