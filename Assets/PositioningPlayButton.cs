using UnityEngine;
using System.Collections;

public class PositioningPlayButton : MonoBehaviour {

	float playButtonOffsetY = .45f;
	float playButtonOffsetWideScreen = .3f;
	public bool wasClicked = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (!wasClicked) {
			if (Screen.height > Screen.width) {
				var newButtonPos = Camera.main.ViewportToWorldPoint (new Vector3 (0f, playButtonOffsetY, 0f));
				newButtonPos.x = transform.position.x;
				newButtonPos.z = transform.position.z;
				transform.position = newButtonPos;
			} else {
				var newButtonPos = Camera.main.ViewportToWorldPoint (new Vector3 (0f, playButtonOffsetWideScreen, 0f));
				newButtonPos.x = transform.position.x;
				newButtonPos.z = transform.position.z;
				transform.position = newButtonPos;
			}
		}
	}
}
