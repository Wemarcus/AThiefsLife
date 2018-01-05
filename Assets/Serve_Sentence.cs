using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serve_Sentence : MonoBehaviour {

	public void ServeSentence(){
		CurrentGame.cg.ServeSentence (CurrentGame.cg.end.years);
	}
}
