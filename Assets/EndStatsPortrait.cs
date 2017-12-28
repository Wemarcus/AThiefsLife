using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndStatsPortrait : MonoBehaviour {

	public Image deathImage;
	public Player player;
	bool death;

	void Update () {
		if (!death) {
			if (player.currentHP <= 0) {
				death = true;
				deathImage.gameObject.SetActive (true);
			}
		}
	}
}
