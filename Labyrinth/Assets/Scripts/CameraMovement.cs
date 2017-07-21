using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour {

	GameObject playerCell;
	public GameObject canvas;
	public GameObject cell;

	public List < List <SharedDataTypes.cellType> > map = new List < List <SharedDataTypes.cellType> > (); 

	public int startingCellPositionX;
	public int startingCellPositionY;

	public int mapHeight;
	public int mapWidth;

	public int currentCellPositionX;
	public int currentCellPositionY;

	void placeplayerCell () {
		playerCell = Instantiate (cell);
		playerCell.transform.Translate (50 * startingCellPositionY, -50 * startingCellPositionX, 0);
		playerCell.GetComponent<Image> ().color = new Color32 (63, 47, 255, 255);
		playerCell.transform.SetParent (canvas.transform, false);
	}

	void Start () {
		placeplayerCell ();
		this.gameObject.transform.Translate (50 * startingCellPositionY, -50 * startingCellPositionX, 0);
		currentCellPositionX = startingCellPositionX;
		currentCellPositionY = startingCellPositionY;
		mapHeight = map.Count - 2;
		mapWidth = map [0].Count - 2;
	}

	void checkZoom () {
		if (Input.GetButtonDown ("Zoom")) {
			if (Input.GetAxis ("Zoom") > 0 && this.gameObject.GetComponent <Camera> ().orthographicSize > 32) {
				this.gameObject.GetComponent <Camera> ().orthographicSize /= 2;
			}
			if (Input.GetAxis ("Zoom") < 0 && this.gameObject.GetComponent <Camera> ().orthographicSize < 10000) {
				this.gameObject.GetComponent <Camera> ().orthographicSize *= 2;
			}
		}
	}

	void tryMoveLeft () {
		if (map [currentCellPositionX] [currentCellPositionY - 1] == SharedDataTypes.cellType.clear) {
			this.gameObject.transform.Translate (-50, 0, 0);
			playerCell.transform.Translate (-50, 0, 0);
			currentCellPositionY--;
		}
	}

	void tryMoveRight () {
		if (map [currentCellPositionX] [currentCellPositionY + 1] == SharedDataTypes.cellType.clear) {
			this.gameObject.transform.Translate (50, 0, 0);
			playerCell.transform.Translate (50, 0, 0);
			currentCellPositionY++;
		}
	}

	void tryMoveDown () {
		if (map [currentCellPositionX + 1] [currentCellPositionY] == SharedDataTypes.cellType.clear) {
			this.gameObject.transform.Translate (0, -50, 0);
			playerCell.transform.Translate (0, -50, 0);
			currentCellPositionX++;
		}
	}

	void tryMoveUp () {
		if (map [currentCellPositionX - 1] [currentCellPositionY] == SharedDataTypes.cellType.clear) {
			this.gameObject.transform.Translate (0, 50, 0);
			playerCell.transform.Translate (0, 50, 0);
			currentCellPositionX--;
		}
	}

	void checkMovement () {
		if (Input.GetButtonDown ("Horizontal")) {
			if (Input.GetAxis ("Horizontal") < 0) {
				tryMoveLeft ();
			}
			if (Input.GetAxis ("Horizontal") > 0) {
				tryMoveRight ();
			}
		}
		if (Input.GetButtonDown ("Vertical")) {
			if (Input.GetAxis ("Vertical") < 0) {
				tryMoveDown ();
			}
			if (Input.GetAxis ("Vertical") > 0) {
				tryMoveUp ();
			}
		}
	}

	void Update () {
		checkZoom ();
		checkMovement ();
	}
}
