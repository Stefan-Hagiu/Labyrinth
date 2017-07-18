using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	const int unitialized = 0;
	const float almostOne = (float) 0.9999999;
	const int bigNumber = 2000000000;

	int height;
	int width;
	const int defaultHeight = 10;
	const int defaultWidth = 10;
	public int startingX; // I consider that the X coordinate represents height and they Y coordinate represents width
	public int startingY;

	public enum cellType {wall, clear};
	public List < List <cellType> > map = new List < List <cellType> > (); //the map is going to be represented as a matrix of bools

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

		for (int i = 0; i <= height; i++) {
			map.Add (new List <cellType> ());
			for (int j = 0; j <= width; j++) {
				map [i].Add (cellType.wall);
			}
		}
		map [startingX] [startingY] = cellType.clear;
	}

	void generateTree () {
		
	}

	void startGenerating () {
		initialize ();
		generateTree (); //The labyrinth's structure is going to be a tree
	}

	void Awake () {
		
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
