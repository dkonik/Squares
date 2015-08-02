using UnityEngine;
using System.Collections;

public class restartImage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		gameScript gs = GameObject.FindGameObjectWithTag ("emptyGO").GetComponent<gameScript> ();
		if (!gs.howToIsUp) {
			gs.RestartGame ();
		}
	}
}
