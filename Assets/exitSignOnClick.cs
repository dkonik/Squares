using UnityEngine;
using System.Collections;

public class exitSignOnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		gameScript gs = GameObject.FindGameObjectWithTag ("emptyGO").GetComponent<gameScript> ();
		gs.howToMenuCreated = false;

		Destroy(GameObject.FindGameObjectWithTag ("howToMenuGO"));
	}
}
