using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour {

	public int height;
	public int width;
	public int numberOfLevels;
	public int isCustom;

	public GameObject sliderHeight;
	public GameObject sliderWidth;
	public GameObject sliderLevels;

	public GameObject myopiaToggle;
	public GameObject lightToggle;
	public GameObject teenagerToggle;
	public GameObject drunkToggle;
	int myopia = 0;
	int dark = 0;
	int teenager = 0;
	int drunk = 0;

	void readToggles () {
		if (myopiaToggle.gameObject.GetComponent<Toggle> ().isOn) {
			myopia = 1;
		}
		if (lightToggle.gameObject.GetComponent<Toggle> ().isOn) {
			dark = 1;
		}
		if (teenagerToggle.gameObject.GetComponent<Toggle> ().isOn) {
			teenager = 1;
		}
		if (drunkToggle.gameObject.GetComponent<Toggle> ().isOn) {
			drunk = 1;
		}
	}

	public void buttonClick () {
		readToggles ();
		if (isCustom == 1) {
			height = (int) sliderHeight.GetComponent<Slider> ().value;
			width = (int) sliderWidth.GetComponent<Slider> ().value;
			numberOfLevels = (int) sliderLevels.GetComponent<Slider> ().value;
		}
		PlayerPrefs.SetInt ("height", height);
		PlayerPrefs.SetInt ("width", width);
		PlayerPrefs.SetInt ("levelsRemaining", numberOfLevels);
		PlayerPrefs.SetInt ("cameraZoom", 256);
		PlayerPrefs.SetInt ("startingX", 0);
		PlayerPrefs.SetInt ("startingY", 0);
		PlayerPrefs.SetInt ("myopia", myopia);
		PlayerPrefs.SetInt ("dark", dark);
		PlayerPrefs.SetInt ("teenager", teenager);
		PlayerPrefs.SetInt ("drunk", drunk);
		PlayerPrefs.Save ();
		SceneManager.LoadScene (1);
	}
}
