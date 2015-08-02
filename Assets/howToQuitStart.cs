using UnityEngine;
using System.Collections;

public class howToQuitStart : MonoBehaviour {


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
		
		Canvas aSCanvas = GameObject.FindGameObjectWithTag("startupPage").transform.parent.GetComponent<Canvas> ();
		AnimatingSquares aS = aSCanvas.GetComponent<AnimatingSquares> ();
		aS.shouldSpawnSquare = 1;

		playButtonOnClick pBOC = GameObject.FindGameObjectWithTag ("playButton").GetComponent<playButtonOnClick> ();
		pBOC.howToIsUp = false;

		setMainScreenLayout sMSL = GameObject.FindGameObjectWithTag ("startupPage").GetComponent<setMainScreenLayout> ();
		sMSL.howToIsUp = false;
		
		Destroy (gameObject);
	}
}
