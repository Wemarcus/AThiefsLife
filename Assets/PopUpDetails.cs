using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpDetails : MonoBehaviour {

	public Text TitleTxt;
	public Text DescriptionTxt;
	public Image TurnImage;
	public Text TurnText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetTitle(string s){
		TitleTxt.text = s;
	}

	public void SetDescription(string s){
		DescriptionTxt.text = s;
	}

	public void SetCD(int cd){
		if (cd == 0) {
			TurnImage.enabled = false;
			TurnText.enabled = false;
		} else {
			TurnImage.enabled = true;
			TurnText.enabled = true;
			TurnText.text = cd.ToString ();
		}
	}
}
