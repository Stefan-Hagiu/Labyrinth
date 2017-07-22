using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour {

	public int height;
	public int width;
	public int numberOfLevels;

	// Use this for initialization
	void Start () {
	}

	public void buttonClick () {
		PlayerPrefs.SetInt ("height", height);
		PlayerPrefs.SetInt ("width", width);
		PlayerPrefs.SetInt ("levelsRemaining", numberOfLevels);
		PlayerPrefs.Save ();
		SceneManager.LoadScene (1);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
