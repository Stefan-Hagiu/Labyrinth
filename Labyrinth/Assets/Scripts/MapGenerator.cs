using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	const int unitialized = 0;
	const float almostOne = (float) 0.9999999;
	const int bigNumber = 2000000000;

	int height;
	int width;
	const int defaultHeight = 4;
	const int defaultWidth = 4;
	public int startingX; // I consider that the X coordinate represents height and they Y coordinate represents width
	public int startingY;

	public List < List <SharedDataTypes.cellType> > map = new List < List <SharedDataTypes.cellType> > (); 

	PrefabInstantiator localPrefabInstantiator;

	class pair {
		public pair (int a, int b) {
			first = a;
			second = b;
		}
		public int first { get; set; }
		public int second { get; set; }
	}

	void initialize () {

		height = PlayerPrefs.GetInt ("height"); 
		width = PlayerPrefs.GetInt ("width");

		if (height == unitialized && width == unitialized) {
			height = defaultHeight;
			width = defaultWidth;
		}

		if (startingX == unitialized && startingY == unitialized) {
			startingX = (int) Random.Range (1, height + almostOne);
			startingY = (int) Random.Range (1, width + almostOne);
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
		List <pair> pointsToBeChecked = new List <pair> ();
		int currentPointIndex;
		int currentNeighbors;

		pointsToBeChecked.Add (new pair (startingX, startingY));
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
			if (currentNeighbors <= 1) {
				map [pointsToBeChecked [currentPointIndex].first] [pointsToBeChecked [currentPointIndex].second] = SharedDataTypes.cellType.clear;
				if (map [pointsToBeChecked [currentPointIndex].first - 1] [pointsToBeChecked [currentPointIndex].second] == SharedDataTypes.cellType.wall) {
					pointsToBeChecked.Add (new pair (pointsToBeChecked [currentPointIndex].first - 1, pointsToBeChecked [currentPointIndex].second));
				}
				if (map [pointsToBeChecked [currentPointIndex].first + 1] [pointsToBeChecked [currentPointIndex].second] == SharedDataTypes.cellType.wall) {
					pointsToBeChecked.Add (new pair (pointsToBeChecked [currentPointIndex].first + 1, pointsToBeChecked [currentPointIndex].second));				
				}
				if (map [pointsToBeChecked [currentPointIndex].first] [pointsToBeChecked [currentPointIndex].second - 1] == SharedDataTypes.cellType.wall) {
					pointsToBeChecked.Add (new pair (pointsToBeChecked [currentPointIndex].first, pointsToBeChecked [currentPointIndex].second - 1));
				}
				if (map [pointsToBeChecked [currentPointIndex].first] [pointsToBeChecked [currentPointIndex].second + 1] == SharedDataTypes.cellType.wall) {
					pointsToBeChecked.Add (new pair (pointsToBeChecked [currentPointIndex].first, pointsToBeChecked [currentPointIndex].second + 1));
				}
			}
			pointsToBeChecked.RemoveAt (currentPointIndex);
		}
	}

	void startGenerating () {
		initialize ();
		generateTree (); //The labyrinth's structure is going to be a tree
	}

	void Awake () {
		startGenerating ();
		localPrefabInstantiator = this.gameObject.GetComponent <PrefabInstantiator> ();
	}

	void Start () {
		localPrefabInstantiator.enabled = true;
		localPrefabInstantiator.getMap (map);
		this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
