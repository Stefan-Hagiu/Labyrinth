using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour {

	public int height;
	public int width;
	public int numberOfLevels;

	public void buttonClick () {
		PlayerPrefs.SetInt ("height", height);
		PlayerPrefs.SetInt ("width", width);
		PlayerPrefs.SetInt ("levelsRemaining", numberOfLevels);
		PlayerPrefs.SetInt ("cameraZoom", 256);
		PlayerPrefs.Save ();
		SceneManager.LoadScene (1);
	}
}
