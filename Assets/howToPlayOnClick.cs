using UnityEngine;
using System.Collections;

public class howToPlayOnClick : MonoBehaviour {


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

			gameScript gS = GameObject.FindGameObjectWithTag ("emptyGO").GetComponent<gameScript> ();
			gS.howToIsUp = true;

			GameObject scrollable = GameObject.FindGameObjectWithTag ("howToPlayScrollable");
			scrollable.transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (.5f, .5f, 1f));

			GameObject outSideBox = GameObject.FindGameObjectWithTag ("howToPlayScrollable");
		
			Vector3 exitSignPos = Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, 1f));
			exitSignPos.x = exitSignPos.x + (outSideBox.GetComponent<Renderer> ().bounds.size.x * 8 / 19);
			exitSignPos.y = exitSignPos.y + (outSideBox.GetComponent<Renderer> ().bounds.size.y * 9 / 20);
		
		
			Instantiate (Resources.Load ("Prefabs/howToQuitButtonGame"), exitSignPos, Quaternion.identity);

		}
	}
}
