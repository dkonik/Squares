using UnityEngine;
using System.Collections;

public class howToQuitGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
			MakeHowToDisappear ();
	}
	
	void OnMouseDown()
	{
		MakeHowToDisappear ();
	}
	
	
	void MakeHowToDisappear()
	{
		GameObject scrollable = GameObject.FindGameObjectWithTag ("howToPlayScrollable");
		
		scrollable.transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (10f, 0f, 10f));

		gameScript gs = GameObject.FindGameObjectWithTag ("emptyGO").GetComponent<gameScript> ();
		gs.canMakeSquares = true;
		gs.howToIsUp = false;
		
		Destroy (gameObject);
	}
}
