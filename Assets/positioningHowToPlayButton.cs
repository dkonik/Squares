using UnityEngine;
using System.Collections;

public class positioningHowToPlayButton : MonoBehaviour {

	bool howToIsUp = false;

	float yOffset = .3f;

	// Use this for initialization
	void Start () {
		var newButtonPos = Camera.main.ViewportToWorldPoint (new Vector3 (0f, yOffset, 0f));
		newButtonPos.x = transform.position.x;
		newButtonPos.z = transform.position.z;
		transform.position = newButtonPos;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{

		setMainScreenLayout sMSL = GameObject.FindGameObjectWithTag ("startupPage").GetComponent<setMainScreenLayout> ();

		if (!sMSL.howToIsUp) {
			sMSL.howToIsUp = true;


			playButtonOnClick pBOC = GameObject.FindGameObjectWithTag ("playButton").GetComponent<playButtonOnClick> ();
			pBOC.howToIsUp = true;

			GameObject scrollable = GameObject.FindGameObjectWithTag ("howToPlayScrollable");

			scrollable.transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (.5f, .5f, 10f));

			Canvas aSCanvas = GameObject.FindGameObjectWithTag ("startupPage").transform.parent.GetComponent<Canvas> ();
			AnimatingSquares aS = aSCanvas.GetComponent<AnimatingSquares> ();
			aS.shouldSpawnSquare = 2;

			GameObject outSideBox = GameObject.FindGameObjectWithTag ("howToPlayScrollable");

			Vector3 exitSignPos = Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, 3f));
			exitSignPos.x = exitSignPos.x + (outSideBox.GetComponent<Renderer> ().bounds.size.x * 8 / 19);
			exitSignPos.y = exitSignPos.y + (outSideBox.GetComponent<Renderer> ().bounds.size.y * 9 / 20);


			Instantiate (Resources.Load ("Prefabs/howToQuitButtonStart"), exitSignPos, Quaternion.identity);
		}
	}
}
