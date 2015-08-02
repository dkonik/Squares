using UnityEngine;
using System.Collections;

public class quitMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) {
			gameScript gs = GameObject.FindGameObjectWithTag ("emptyGO").GetComponent<gameScript> ();
			if (!gs.howToIsUp) {
				Application.Quit ();
			}
		}
	}

	void OnMouseDown()
	{
		gameScript gs = GameObject.FindGameObjectWithTag ("emptyGO").GetComponent<gameScript> ();
		if (!gs.howToIsUp) {
			Application.Quit ();
		}
	}
}
