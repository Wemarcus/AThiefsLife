using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MessagePopUp{
public class MessagePopUp : MonoBehaviour {

	public static IEnumerator ShowMessage(string s,Text messageTxt){
			messageTxt.text = s;
			yield return new WaitForSeconds (3.0f);
			messageTxt.text = "";
		}
	}
}
