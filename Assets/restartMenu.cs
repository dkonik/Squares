using UnityEngine;
using System.Collections;

public class restartMenu : MonoBehaviour {

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
			gs.howToMenuCreated = false;

			GameObject parent = GameObject.FindGameObjectWithTag ("howToMenuGO");

			if (parent == null)
				Debug.LogError ("Couldnt find parent GO");

			Destroy (parent);
		}
	}
}
