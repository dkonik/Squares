using UnityEngine;
using System.Collections;

public class RotateAndTranslateSquares : MonoBehaviour {
	float rotateSpeed = 90f;

	public int whichSide;
	public float startingX;
	public float startingY;

	float rotateDirection;
	float deltaX;
	float deltaY;
	float translateSpeed = 2f;

	// Use this for initialization
	void Start () {
		rotateDirection = 1f;
		if (Random.value > 0.5)
			rotateDirection = -1f;

		switch(whichSide)
		{
		case 0:
			deltaX = Random.Range(-2f, 2f);
			deltaY = Random.Range (-2f, -.5f);
			break;
		case 1:
			deltaX = Random.Range(-2f, -.5f);
			deltaY = Random.Range (-2f, 2f);
			break;
		case 2:
			deltaX = Random.Range(-2f, 2f);
			deltaY = Random.Range (.5f, 2f);
			break;
		case 3:
			deltaX = Random.Range(.5f, 2f);
			deltaY = Random.Range (-2f, -2f);
			break;

		};
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0f, 0f, rotateDirection * rotateSpeed * Time.deltaTime);

		transform.Translate (deltaX * Time.deltaTime * translateSpeed, deltaY * Time.deltaTime * translateSpeed, 0f, Space.World);

//		Debug.LogError ("deltaX = " + deltaX);
//		Debug.LogError ("deltaY = " + deltaY);
	}
}
