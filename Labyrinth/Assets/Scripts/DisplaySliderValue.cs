using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySliderValue : MonoBehaviour {

	public void displayValue () {
		this.GetComponentInChildren<Text>().text =  this.gameObject.GetComponent<Slider> ().value.ToString();
	}
}
