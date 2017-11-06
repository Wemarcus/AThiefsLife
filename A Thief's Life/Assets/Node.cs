using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	public List<GameObject> Neighbours;

	[Header("Node Settings")]
	public bool AllySpawn;
	public Material thisMaterial;
	public BlockType blockType;
	public bool visited = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
