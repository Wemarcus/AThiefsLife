using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	public List<GameObject> Neighbours;

	[Header("Node Settings")]
	public bool AllySpawn;
	public GameObject obstacle;
	public Material thisMaterial;
	public BlockType blockType;
	public GameObject player;
	public bool visited = false;
	public int depth=0;
	public bool isCover;
	//MapHandler mh;

	// Use this for initialization
	void Start () {
		//mh = FindObjectOfType<MapHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*void OnMouseEnter(){
		Debug.Log ("entro in" + this.gameObject.name);
	}

	void OnMouseExit(){
		Debug.Log ("esco da" + this.gameObject.name);
	}*/

	public void FindNeighbours(GameObject[,] grid){
		FindRightNeighbour (grid);
		FindLeftNeighbour (grid);
		FindTopNeighbour (grid);
		FindBottomNeighbour (grid);
	}

	public void FindRightNeighbour(GameObject[,] grid){
		int x = (int)this.gameObject.transform.position.x;
		int z = (int)this.gameObject.transform.position.z;
		if (x >= 0 && z-1 >= 0) {
			GameObject neigh = grid [x, z - 1];
			if (neigh) {
				Neighbours.Add (neigh);
			}
		}
	}

	public void FindLeftNeighbour(GameObject[,] grid){
		int x = (int)this.gameObject.transform.position.x;
		int z = (int)this.gameObject.transform.position.z;
		if (x >= 0 && z >= 0) {
			GameObject neigh = grid [x, z + 1];
			if (neigh) {
				Neighbours.Add (neigh);
			}
		}
	}
	

	public void FindTopNeighbour(GameObject[,] grid){
		int x = (int)this.gameObject.transform.position.x;
		int z = (int)this.gameObject.transform.position.z;
		if (x >= 0 && z >= 0) {
			GameObject neigh = grid [x + 1, z];
			if (neigh) {
				Neighbours.Add (neigh);
			}
		}
	}

	public void FindBottomNeighbour(GameObject[,] grid){
		int x = (int)this.gameObject.transform.position.x;
		int z = (int)this.gameObject.transform.position.z;
		if (x-1 >= 0 && z >= 0) {
			GameObject neigh = grid [x - 1, z];
			if (neigh) {
				Neighbours.Add (neigh);
			}
		}
	}
}
