using UnityEngine;
using System.Collections;

public class playAgainOnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		gameScript gs = GameObject.FindGameObjectWithTag ("emptyGO").GetComponent<gameScript> ();
		gs.RestartGame ();

		GameObject parent = GameObject.FindGameObjectWithTag ("gameOverBanner");
		Destroy (parent);
	}
}
