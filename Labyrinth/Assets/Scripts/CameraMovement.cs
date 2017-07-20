using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public int startingCellX;
	public int startingCellY;

	// Use this for initialization
	void Start () {
		this.gameObject.transform.Translate (50 * startingCellY, -50 * startingCellX, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Horizontal")) {
			if (Input.GetAxis ("Horizontal") < 0) {
				this.gameObject.transform.Translate (-50, 0, 0);
			}
			if (Input.GetAxis ("Horizontal") > 0) {
				this.gameObject.transform.Translate (50, 0, 0);
			}
		}
		if (Input.GetButtonDown ("Vertical")) {
			if (Input.GetAxis ("Vertical") < 0) {
				this.gameObject.transform.Translate (0, -50, 0);
			}
			if (Input.GetAxis ("Vertical") > 0) {
				this.gameObject.transform.Translate (0, 50, 0);
			}
		}
	}
}
