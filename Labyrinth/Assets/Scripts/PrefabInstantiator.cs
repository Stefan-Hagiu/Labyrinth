using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabInstantiator : MonoBehaviour {
	// Unity considers the X axis to the right and the Y axis up
	public GameObject cell;
	public GameObject canvas;
	GameObject newCell;

	int mapHeight, mapWidth;

	public GameObject mainCamera;

	List < List <SharedDataTypes.cellType> > map = new List < List <SharedDataTypes.cellType> > ();

	public void getMap (object receivedMap) {
		map = receivedMap as List < List <SharedDataTypes.cellType> >;
	}

	void Start () {
		if (map == null) {
			return;
		}
		//placing cells
		for (int i = 1; i < map.Count - 1; i++) {
			for (int j = 1; j < map [0].Count - 1; j++) {
				newCell = Instantiate (cell, new Vector3 (50 * j, -50 * i), this.transform.rotation);
				newCell.transform.SetParent (canvas.transform, false);
				if (map [i] [j] == SharedDataTypes.cellType.wall) {
					newCell.GetComponent <Image> ().color = Color.black;
				}
				if (map [i] [j] == SharedDataTypes.cellType.clear) {
					newCell.GetComponent <Image> ().color = Color.white;
				}
			}
		}
		mainCamera.SetActive (true);
		this.enabled = false;
	}

	void Update () {
		
	}
}
