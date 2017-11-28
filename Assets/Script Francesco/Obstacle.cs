using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	void OnCollisionEnter(Collision collision){
		Debug.Log ("ostacolo collide con :" + collision.gameObject.name);
		Node n;
		if (collision.gameObject.tag == "Walkable") {
			n = collision.gameObject.GetComponent<Node> ();
			n.blockType = BlockType.Obstacle;
			Grid.GridMath.ChangeBlockColour (Color.red, collision.gameObject);
			n.obstacle = this.gameObject;
		}
	}

	void OnCollisionExit(Collision collision){
		Node n;
		if (collision.gameObject.tag == "Walkable") {
			n = collision.gameObject.GetComponent<Node> ();
			n.blockType = BlockType.Walkable;
			Grid.GridMath.RevertBlockColour (collision.gameObject);
			n.obstacle = null;
		}
	}
}
