using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A Thief's Life/End")]
public class EndRobbery : ScriptableObject {

	public bool robberyEnded;
	public EndCases endCase;

	public void EndSetup(EndCases end){
		robberyEnded = true;
		endCase = end;
	}

	public void Reset(){
		robberyEnded = false;
		endCase = EndCases.Null;
	}
}
