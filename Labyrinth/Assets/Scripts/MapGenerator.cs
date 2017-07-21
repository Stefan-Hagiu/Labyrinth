using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	const int uninitialized = 0;
	const float almostOne = (float) 0.9999999;
	const int bigNumber = 2000000000;

	int height;
	int width;
	const int defaultHeight = 250;
	const int defaultWidth = 250;
	public int startingX; // I consider that the X coordinate represents height and they Y coordinate represents width
	public int startingY;

	public List < List <SharedDataTypes.cellType> > map = new List < List <SharedDataTypes.cellType> > (); 

	public GameObject mainCamera;
	public GameObject cell;

	PrefabInstantiator localPrefabInstantiator;

	void initialize () {

		height = PlayerPrefs.GetInt ("height"); 
		width = PlayerPrefs.GetInt ("width");

		if (height == uninitialized && width == uninitialized) {
			height = defaultHeight;
			width = defaultWidth;
		}

		for (int i = 0; i <= height + 1; i++) {
			map.Add (new List <SharedDataTypes.cellType> ());
			for (int j = 0; j <= width + 1; j++) {
				map [i].Add (SharedDataTypes.cellType.wall);
			}
		}
		map [startingX] [startingY] = SharedDataTypes.cellType.clear;
	}

	void generateTree () {
		/*	
		       before adding a point to the tree, I make sure that:
		    1) it is still in the matrix' range 
			2) no more than 1 of its neighbors have been added; 
		*/
		List <SharedDataTypes.pair> pointsToBeChecked = new List <SharedDataTypes.pair> ();
		int currentPointIndex;
		int currentNeighbors;

		pointsToBeChecked.Add (new SharedDataTypes.pair (startingX, startingY));
		while (pointsToBeChecked.Count != 0) {
			currentPointIndex = (int) Random.Range (0, (pointsToBeChecked.Count - 1) + almostOne);
			currentNeighbors = 0;

			if (pointsToBeChecked [currentPointIndex].first == 0 ||
			    pointsToBeChecked [currentPointIndex].second == 0 ||
			    pointsToBeChecked [currentPointIndex].first > height ||
			    pointsToBeChecked [currentPointIndex].second > width) {
				pointsToBeChecked.RemoveAt (currentPointIndex);
				continue;
			}

			//I check how many clear neighbors the cell has
			if (map [pointsToBeChecked [currentPointIndex].first - 1] [pointsToBeChecked [currentPointIndex].second] == SharedDataTypes.cellType.clear) {
				currentNeighbors++;
			}
			if (map [pointsToBeChecked [currentPointIndex].first + 1] [pointsToBeChecked [currentPointIndex].second] == SharedDataTypes.cellType.clear) {
				currentNeighbors++;
			}
			if (map [pointsToBeChecked [currentPointIndex].first] [pointsToBeChecked [currentPointIndex].second - 1] == SharedDataTypes.cellType.clear) {
				currentNeighbors++;
			}
			if (map [pointsToBeChecked [currentPointIndex].first] [pointsToBeChecked [currentPointIndex].second + 1] == SharedDataTypes.cellType.clear) {
				currentNeighbors++;
			}
			//I add all wall neighbors to pointsToBeChecked
			if (currentNeighbors <= 1) {
				map [pointsToBeChecked [currentPointIndex].first] [pointsToBeChecked [currentPointIndex].second] = SharedDataTypes.cellType.clear;
				if (map [pointsToBeChecked [currentPointIndex].first - 1] [pointsToBeChecked [currentPointIndex].second] == SharedDataTypes.cellType.wall) {
					pointsToBeChecked.Add (new SharedDataTypes.pair (pointsToBeChecked [currentPointIndex].first - 1, pointsToBeChecked [currentPointIndex].second));
				}
				if (map [pointsToBeChecked [currentPointIndex].first + 1] [pointsToBeChecked [currentPointIndex].second] == SharedDataTypes.cellType.wall) {
					pointsToBeChecked.Add (new SharedDataTypes.pair (pointsToBeChecked [currentPointIndex].first + 1, pointsToBeChecked [currentPointIndex].second));				
				}
				if (map [pointsToBeChecked [currentPointIndex].first] [pointsToBeChecked [currentPointIndex].second - 1] == SharedDataTypes.cellType.wall) {
					pointsToBeChecked.Add (new SharedDataTypes.pair (pointsToBeChecked [currentPointIndex].first, pointsToBeChecked [currentPointIndex].second - 1));
				}
				if (map [pointsToBeChecked [currentPointIndex].first] [pointsToBeChecked [currentPointIndex].second + 1] == SharedDataTypes.cellType.wall) {
					pointsToBeChecked.Add (new SharedDataTypes.pair (pointsToBeChecked [currentPointIndex].first, pointsToBeChecked [currentPointIndex].second + 1));
				}
			}
			pointsToBeChecked.RemoveAt (currentPointIndex);
		}
	}

	void findStartingSpot () {
		if (startingX == uninitialized && startingY == uninitialized) {
			startingX = (int)Random.Range (1, height + almostOne);
			startingY = (int)Random.Range (1, width + almostOne);
		}
	}

	void startGenerating () {
		initialize ();
		findStartingSpot ();
		generateTree (); //The labyrinth's structure is going to be a tree

	}

	void Awake () {
		startGenerating ();
		localPrefabInstantiator = this.gameObject.GetComponent <PrefabInstantiator> ();
	}

	void sendValuesToCamera () {
		mainCamera.GetComponent<CameraMovement> ().startingCellPositionX = startingX;
		mainCamera.GetComponent<CameraMovement> ().startingCellPositionY = startingY;
		mainCamera.GetComponent<CameraMovement> ().map = map;
	}

	void Start () {
		localPrefabInstantiator.enabled = true;
		localPrefabInstantiator.getMap (map);
		sendValuesToCamera ();
		this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
