using UnityEngine;
using System.Collections;

public class whatToDoMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameScript gs = GameObject.FindGameObjectWithTag ("emptyGO").GetComponent<gameScript> ();
		gs.canMakeSquares = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDisable() { 
		if (gameObject.active) { 
			gameScript gs = GameObject.FindGameObjectWithTag ("emptyGO").GetComponent<gameScript> ();
			gs.canMakeSquares = true;
			gs.howToMenuCreated = false;
		}
	}
}
