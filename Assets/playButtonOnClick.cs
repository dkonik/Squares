using UnityEngine;
using System.Collections;

public class playButtonOnClick : MonoBehaviour {

	float yPosPlaybutton;
	float torqueOnIncomingSquare = -100f;
	float forceOnIncomingSquare;
	string squareColor;

	public bool howToIsUp = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseDown()
	{
		if (!howToIsUp) {
			StartCoroutine (StartGamePage ());
		}

	}

	private IEnumerator StartGamePage()
	{
		if (Screen.height > Screen.width) {
			forceOnIncomingSquare = 1500f;
		} else {
			forceOnIncomingSquare = 3000f;
		}
		
		yPosPlaybutton = transform.position.y;
		Vector3 incomingSquareStartingPoint = Camera.main.ViewportToWorldPoint (new Vector3(-.1f, 0f, 0f));
		incomingSquareStartingPoint.y = yPosPlaybutton;
		incomingSquareStartingPoint.z = 0f;
		
		//0 = blue, 1 = gray, 2 = green, 3 = red
		int whichSquare = Random.Range (0, 4);
		
		
		
		
		switch (whichSquare) {
		case 0: 
			squareColor = "blue";
			break;
		case 1:
			squareColor = "gray";
			break;
		case 2:
			squareColor = "green";
			break;
		case 3:
			squareColor = "red";
			break;
		}
		
		GameObject incomingSquare = (GameObject) Instantiate (Resources.Load ("Prefabs/" + squareColor + "Square"), incomingSquareStartingPoint, Quaternion.identity);
		Rigidbody2D incomingSquareRB = incomingSquare.AddComponent<Rigidbody2D> ();
		incomingSquare.AddComponent<BoxCollider2D> ();
		incomingSquareRB.mass = 1;
		incomingSquareRB.velocity = new Vector2(0f, 0f);
		incomingSquareRB.gravityScale = 0;
		incomingSquareRB.angularDrag = 0;
		
		//Removes the automatic positioning of the play button
		PositioningPlayButton playButtonScript = this.GetComponent<PositioningPlayButton> ();
		playButtonScript.wasClicked = true;
		
		//Adds rigidbody to play button
		Rigidbody2D playButtonRigidBody = this.gameObject.AddComponent<Rigidbody2D> ();
		if (playButtonRigidBody == null) {
			Debug.LogError ("Rigid body doesn't exist");
		}
		playButtonRigidBody.gravityScale = 0;
		playButtonRigidBody.mass = .5f;
		
		
		incomingSquareRB.AddTorque (torqueOnIncomingSquare);
		incomingSquareRB.AddForce (new Vector2(forceOnIncomingSquare, forceOnIncomingSquare / 12f));

		yield return new WaitForSeconds (.85f);

		//Moves Camera to gamepage
		Camera mainCamera = Camera.main;
		Vector3 newCameraPos = new Vector3 (100f, 100f, 0f);

		//Destroys Play button
		Destroy (gameObject);
		Destroy (incomingSquare);



		GameObject startUpPageGo = GameObject.FindGameObjectWithTag ("startupPage");
		Canvas startPageCanvas = startUpPageGo.transform.parent.GetComponent<Canvas> ();
		if (startPageCanvas == null) {
			Debug.LogError ("startPageCanvas not found");
		}
		startPageCanvas.renderMode = RenderMode.WorldSpace;

		//Renders new Canvas
		Canvas gamePageCanvas = GameObject.FindGameObjectWithTag ("emptyGO").transform.parent.GetComponent<Canvas> ();
		gamePageCanvas.enabled = true;
		gamePageCanvas.renderMode = RenderMode.ScreenSpaceCamera;

		mainCamera.transform.position = newCameraPos;

		//Makes grid get setup
		setUpGamePage startPageScript = GameObject.FindGameObjectWithTag("emptyGO").GetComponent<setUpGamePage>();
		startPageScript.shouldSetPosition = true;


		AnimatingSquares asScript = startPageCanvas.GetComponent<AnimatingSquares> ();
		asScript.shouldSpawnSquare = 2;
		startPageCanvas.enabled = false;






	}
}
