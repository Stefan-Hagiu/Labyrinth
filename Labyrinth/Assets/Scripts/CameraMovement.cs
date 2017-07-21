using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour {

	GameObject playerCell;
	public GameObject canvas;
	public GameObject cell;

	public int startingCellPositionX;
	public int startingCellPositionY;

	// Use this for initialization
	void Start () {
		playerCell = Instantiate (cell);
		this.gameObject.transform.Translate (50 * startingCellPositionY, -50 * startingCellPositionX, 0);
		playerCell.transform.Translate (50 * startingCellPositionY, -50 * startingCellPositionX, 0);
		playerCell.GetComponent<Image> ().color = Color.blue;
		playerCell.transform.SetParent (canvas.transform, false);
	}
	
	// Update is called once per frame
	void Update () {
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
}
