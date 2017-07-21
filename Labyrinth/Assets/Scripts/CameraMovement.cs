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

	void placeplayerCell () {
		playerCell = Instantiate (cell);
		playerCell.transform.Translate (50 * startingCellPositionY, -50 * startingCellPositionX, 0);
		playerCell.GetComponent<Image> ().color = Color.blue;
		playerCell.transform.SetParent (canvas.transform, false);
	}

	void Start () {
		placeplayerCell ();
		this.gameObject.transform.Translate (50 * startingCellPositionY, -50 * startingCellPositionX, 0);
	}

	void checkZoom () {
		if (Input.GetButtonDown ("Zoom")) {
			if (Input.GetAxis ("Zoom") > 0 && this.gameObject.GetComponent <Camera> ().orthographicSize > 32) {
				this.gameObject.GetComponent <Camera> ().orthographicSize /= 2;
			}
			if (Input.GetAxis ("Zoom") < 0) {
				this.gameObject.GetComponent <Camera> ().orthographicSize *= 2;
			}
		}
	}

	void checkMovement () {
		if (Input.GetButtonDown ("Horizontal")) {
			if (Input.GetAxis ("Horizontal") < 0) {
				this.gameObject.transform.Translate (-50, 0, 0);
				playerCell.transform.Translate (-50, 0, 0);
			}
			if (Input.GetAxis ("Horizontal") > 0) {
				this.gameObject.transform.Translate (50, 0, 0);
				playerCell.transform.Translate (50, 0, 0);
			}
		}
		if (Input.GetButtonDown ("Vertical")) {
			if (Input.GetAxis ("Vertical") < 0) {
				this.gameObject.transform.Translate (0, -50, 0);
				playerCell.transform.Translate (0, -50, 0);
			}
			if (Input.GetAxis ("Vertical") > 0) {
				this.gameObject.transform.Translate (0, 50, 0);
				playerCell.transform.Translate (0, 50, 0);
			}
		}
	}

	void Update () {
		checkZoom ();
		checkMovement ();
	}
}
