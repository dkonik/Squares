using UnityEngine;
using System.Collections;

public class AnimatingSquares : MonoBehaviour {
	
	float screenHeight;
	float screenWidth;
	
	float startingY;
	float startingX;
	string squareColor;
	public int shouldSpawnSquare;

	// Use this for initialization
	void Start () {
		shouldSpawnSquare = 1;
	}
	
	// Update is called once per frame
	void Update () {

		if (shouldSpawnSquare == 1) {
			shouldSpawnSquare = 0;
			StartCoroutine( CreateAndAnimateSquare ());
		}
	}

	private IEnumerator CreateAndAnimateSquare()
	{
		//0 = blue, 1 = gray, 2 = green, 3 = red
		int whichSquare = Random.Range (0, 4);
		//0 = top, 1 = right, 2 = bottom, 3 = left 
		int whichSide = Random.Range (0, 4);
	

		switch (whichSquare) {
		case 0: 
			squareColor = "blue";
			break;
		case 1:
			squareColor = "yellow";
			break;
		case 2:
			squareColor = "green";
			break;
		case 3:
			squareColor = "red";
			break;
		}


		switch (whichSide) {
		//Top
		case 0:
			startingX = Random.Range (0f, 1f);
			startingY = 1.1f;
			break;
		//Right
		case 1:
			startingX = 1.1f;
			startingY = Random.Range (0f, 1f);
			break;
		//Bottom
		case 2:
			startingX = Random.Range (0f, 1f);
			startingY = -.1f;
			break;
		//Left
		case 3:
			startingX = -.1f;
			startingY = Random.Range (0f, 1f);
			break;
		};

		Vector3 newSquarePos = Camera.main.ViewportToWorldPoint(new Vector3(startingX, startingY, 15f));
		GameObject square1 = (GameObject) Instantiate (Resources.Load ("Prefabs/" + squareColor + "Square"),newSquarePos, Quaternion.identity);
		square1.transform.SetParent (gameObject.transform);

		if (square1 == null) {
			Debug.LogError("Fuck");
		}

		RotateAndTranslateSquares squareScript = square1.GetComponent<RotateAndTranslateSquares> ();

		squareScript.whichSide = whichSide;
		squareScript.startingX = startingX;
		squareScript.startingY = startingY;

		yield return new WaitForSeconds (2);
		if (shouldSpawnSquare == 0)
			shouldSpawnSquare = 1;
		Destroy (square1, 10f);
		}
	
	}
















