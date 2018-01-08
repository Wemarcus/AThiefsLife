using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLibrary : MonoBehaviour {

	public static string ReturnDotOfInt(int number){
		string s = string.Format("{0:C}", number);
			return s;
	}
}
