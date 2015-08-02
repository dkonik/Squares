using UnityEngine;
using System.Collections;

public class positioningLogo : MonoBehaviour {

	float logoOffset = .67f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		var newLogoPos = Camera.main.ViewportToWorldPoint (new Vector3 (0f, logoOffset, 0f));
		newLogoPos.x = transform.position.x;
		newLogoPos.z = transform.position.z;
		transform.position = newLogoPos;
	}
}
