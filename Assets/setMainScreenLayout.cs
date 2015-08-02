using UnityEngine;
using System.Collections;

public class setMainScreenLayout : MonoBehaviour {

	public bool howToIsUp = false;

	// Use this for initialization
	void Start () {	
		Canvas gamePageCanvas = GameObject.FindGameObjectWithTag ("emptyGO").transform.parent.GetComponent<Canvas> ();
		gamePageCanvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if( !howToIsUp)
			{
				Application.Quit ();
			}
		}
			
		
		
	}
}
