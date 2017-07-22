using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour {

	GameObject playerCell;
	GameObject endingCell;
	public GameObject canvas;
	public GameObject cell;
	public GameObject mazeGenerator;

	public int inversedHorizontal = 0;
	public int inversedVertical = 0;
	public float drunkInterval = 5;
	public float currentInterval = 5;

	public List < List <SharedDataTypes.cellType> > map = new List < List <SharedDataTypes.cellType> > (); 

	public int startingCellPositionX;
	public int startingCellPositionY;
	public int endingCellPositionX;
	public int endingCellPositionY;

	public int mapHeight;
	public int mapWidth;

	public int currentCellPositionX;
	public int currentCellPositionY;

	float currentCameraPositionX;
	float currentCameraPositionY;
	float finalCameraPositionX;
	float finalCameraPositionY;

	float min (float a, float b) {
		if (a < b) {
			return a;
		} else {
			return b;
		}
	}
	float max (float a, float b) {
		if (a >b) {
			return a;
		} else {
			return b;
		}
	}

	void placeplayerCell () {
		playerCell = Instantiate (cell);
		playerCell.transform.Translate (50 * startingCellPositionY, -50 * startingCellPositionX, 0);
		playerCell.GetComponent<Image> ().color = new Color32 (63, 47, 255, 255);
		playerCell.transform.SetParent (canvas.transform, false);
	}
	void placeendingCell () {
		endingCell = Instantiate (cell);
		endingCell.transform.Translate (50 * endingCellPositionY, -50 * endingCellPositionX, 0);
		if (PlayerPrefs.GetInt ("teenager") == 1) {
			if (UnityEngine.Random.Range (1, 3) < 2) {
				endingCell.GetComponent<Image> ().color = Color.black;
			} else {
				endingCell.GetComponent<Image> ().color = Color.white;
			}
		} else {
			endingCell.GetComponent<Image> ().color = Color.red;
		}
		endingCell.transform.SetParent (canvas.transform, false);
	}
	void placeCamera() {
		if (PlayerPrefs.GetInt ("myopia") == 1) {
			this.gameObject.GetComponent <Camera> ().orthographicSize = 32;
		} else {
			this.gameObject.GetComponent <Camera> ().orthographicSize = PlayerPrefs.GetInt ("cameraZoom");
		}
		this.gameObject.transform.Translate (50 * startingCellPositionY, -50 * startingCellPositionX, 0);
		currentCameraPositionX = finalCameraPositionX = 50 * startingCellPositionY;
		currentCameraPositionY = finalCameraPositionY = -50 * startingCellPositionX;
	}

	void Start () {
		placeplayerCell ();
		placeendingCell ();
		placeCamera ();
		currentCellPositionX = startingCellPositionX;
		currentCellPositionY = startingCellPositionY;
		mapHeight = map.Count - 2;
		mapWidth = map [0].Count - 2;
	}

	void checkZoom () {
		if (Input.GetButtonDown ("Zoom") && (PlayerPrefs.GetInt ("myopia") == 0)) {
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
			finalCameraPositionX -= 50;
			playerCell.transform.Translate (-50, 0, 0);
			currentCellPositionY--;
		}
	}
	void tryMoveRight () {
		if (map [currentCellPositionX] [currentCellPositionY + 1] == SharedDataTypes.cellType.clear) {
			finalCameraPositionX += 50;
			playerCell.transform.Translate (50, 0, 0);
			currentCellPositionY++;
		}
	}
	void tryMoveDown () {
		if (map [currentCellPositionX + 1] [currentCellPositionY] == SharedDataTypes.cellType.clear) {
			finalCameraPositionY -= 50;
			playerCell.transform.Translate (0, -50, 0);
			currentCellPositionX++;
		}
	}
	void tryMoveUp () {
		if (map [currentCellPositionX - 1] [currentCellPositionY] == SharedDataTypes.cellType.clear) {
			finalCameraPositionY += 50;
			playerCell.transform.Translate (0, 50, 0);
			currentCellPositionX--;
		}
	}

	void checkMovement () {
		if (Input.GetButtonDown ("Horizontal")) {
			if ((Input.GetAxis ("Horizontal") < 0 && inversedHorizontal == 0) || (Input.GetAxis ("Horizontal") > 0 && inversedHorizontal == 1)) {
				tryMoveLeft ();
			}
			if ((Input.GetAxis ("Horizontal") > 0 && inversedHorizontal == 0) || (Input.GetAxis ("Horizontal") < 0 && inversedHorizontal == 1)) {
				tryMoveRight ();
			}
		}
		if (Input.GetButtonDown ("Vertical")) {
			if ((Input.GetAxis ("Vertical") < 0 && inversedVertical == 0) || (Input.GetAxis ("Vertical") > 0 && inversedVertical == 1)) {
				tryMoveDown ();
			}
			if ((Input.GetAxis ("Vertical") > 0 && inversedVertical == 0) || (Input.GetAxis ("Vertical") < 0 && inversedVertical == 1)) {
				tryMoveUp ();
			}
		}
	}

	void checkFinish () {
		if (currentCellPositionX == endingCellPositionX && currentCellPositionY == endingCellPositionY) {
			PlayerPrefs.SetInt ("cameraZoom", (int) this.gameObject.GetComponent <Camera> ().orthographicSize);
			PlayerPrefs.SetInt ("startingX", currentCellPositionX);
			PlayerPrefs.SetInt ("startingY", currentCellPositionY);
			if (PlayerPrefs.GetInt ("levelsRemaining") == 0) {
				SceneManager.LoadScene (0);
			} else {
				SceneManager.LoadScene (1);
			}
		}
	}

	void animateCamera () {
		if (currentCameraPositionX < finalCameraPositionX) {
			this.gameObject.transform.Translate (max ((finalCameraPositionX - currentCameraPositionX) / 10, 0.1f), 0, 0);
			currentCameraPositionX = this.gameObject.transform.position.x;
		}
		if (currentCameraPositionX > finalCameraPositionX) {
			this.gameObject.transform.Translate (min ((finalCameraPositionX - currentCameraPositionX) / 10, -0.1f), 0, 0);
			currentCameraPositionX = this.gameObject.transform.position.x;
		}
		if (currentCameraPositionY < finalCameraPositionY) {
			this.gameObject.transform.Translate (0, max ((finalCameraPositionY - currentCameraPositionY) / 10, 0.1f), 0);
			currentCameraPositionY = this.gameObject.transform.position.y;
		}
		if (currentCameraPositionY > finalCameraPositionY) {
			this.gameObject.transform.Translate (0, min ((finalCameraPositionY - currentCameraPositionY) / 10, -0.1f), 0);
			currentCameraPositionY = this.gameObject.transform.position.y;
		}
	}

	void checkSurrender () {
		if (Input.GetButtonDown ("Surrender")) {
			SceneManager.LoadScene (0);
		}
	}

	void invertAxis () {
		if (Random.Range ((float)1, (float)3) > (float)2) {
			inversedHorizontal = 0;
		} else {
			inversedHorizontal = 1;
		}
		if (Random.Range ((float)1, (float)3) > (float)2) {
			inversedVertical = 0;
		} else {
			inversedVertical = 1;
		}
	}

	void checkDrunk () {
		if (PlayerPrefs.GetInt ("drunk") == 1) {
			currentInterval -= Time.deltaTime;
			if (currentInterval < 0) {
				currentInterval = drunkInterval;
				invertAxis ();
			}
		}
	}

	void Update () {
		checkDrunk ();
		checkZoom ();
		checkMovement ();
		checkFinish ();
		checkSurrender ();
		animateCamera ();
	}
}
