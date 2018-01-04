using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (CurrentGame.cg.end.robberyEnded) {
			switch (CurrentGame.cg.end.endCase) {
			case EndCases.Null:
				break;

			case EndCases.Run:
				break;

			case EndCases.Arrested:
				break;

			case EndCases.Died:
				break;
			}
			CurrentGame.cg.end.Reset ();
		}
	}
}
