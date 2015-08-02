using UnityEngine;
using System.Collections;

public class GridSquare{
	private bool occupied;

	//0 = blue, 1 = yellow, 2 = green, 3 = red, -1 = does not exist
	private int color;
	
	public bool IsFree()
	{
		return !occupied;
	}

	public bool isOccupied()
	{
		return occupied;
	}
	
	public void Deoccupy()
	{
		occupied = false;
		color = -1;
	}
	
	public int getColor()
	{
		return color;
	}
	
	public GridSquare()
	{
		occupied = false;
		color = -1;
	}

	public void AddSquare(int colorIn)
	{
		occupied = true;
		color = colorIn;
	}
	
	
}


public class gameScript : MonoBehaviour {
	public GameObject[,] grid;
	public bool canMakeSquares;

	public float minSwipeDistY;
	public float minSwipeDistX;

	public int numSquaresInGrid;
	public int numDestroysThisRound = 1;

	float translationTime = .2f;



	

//	public enum colorOfSquare {blue, gray, green, red};
	
	int sizeY = 5;
	int sizeX = 5;

	//0 = up, 1 = right, 2 = down, 3 = left
	int gravtiy;

	public GameObject[,] gridSquares = new GameObject[5, 5];

	public GridSquare[,] gridSquareData = new GridSquare[5, 5];

	float startPosX;
	float startPosY;
	float endPosX;
	float endPosY;

	public bool howToMenuCreated = false;

	Vector2 startPos;

	public bool howToIsUp = false;


	// Use this for initialization
	void Start () {
		canMakeSquares = false;
		gravtiy = 2;
		for (int i = 0; i < sizeX; i++) {
			for (int j = 0; j < sizeY; j++)
			{
				gridSquareData[i, j] = new GridSquare();
			}
		}
		minSwipeDistY = 30f;
		minSwipeDistX = 30f;
	}
	// Update is called once per frame
	void Update () {
		if(canMakeSquares)
		{
			if (Input.touchCount > 0) 	
			{
				Touch touch = Input.touches[0];

				switch(touch.phase){
				case TouchPhase.Began:
					startPosX = touch.position.x;
					startPosY = touch.position.y;
					startPos = touch.position;
					break;
				case TouchPhase.Ended:
					Debug.LogError("Swipe Dist Vertical: " + (touch.position.y - startPosY));
					Debug.LogError("MinSwipeDistVertical: " + minSwipeDistY);
					Debug.LogError("Swipe Dist Horizontal: " + (touch.position.x - startPosX));
					Debug.LogError("Minimum Hor Distance: " + minSwipeDistX);
					endPosX = touch.position.x;
					endPosY = touch.position.y;
					float swipeDistVertical = endPosY - startPosY;
					float swipeDistHorizontal = endPosX - startPosX;

					Debug.LogError("Is hor swipe true: " + (Mathf.Abs ( swipeDistHorizontal) > minSwipeDistX));
					Debug.LogError("Is vert swipe true: " + (Mathf.Abs ( swipeDistVertical) > minSwipeDistY));

					if((Mathf.Abs(swipeDistVertical) > Mathf.Abs(swipeDistHorizontal)) && (Mathf.Abs ( swipeDistVertical) > minSwipeDistY))
					{
						float swipeValue = Mathf.Sign(swipeDistVertical);
						//up swipe		
						if (swipeValue > 0)
						{
//							GameObject newInsert = (GameObject)Instantiate (Resources.Load ("Prefabs/blueSquareNoMovement"), 
//							                                                new Vector3 (grid [2, 4].transform.position.x, grid [2, 4].transform.position.y, 5f), 
//							                                                Quaternion.identity);

							Debug.LogError("UpSwipe");
							ChangeGravity(0);
//							AddSquare ();
							
						}
						//down swipe
						else if (swipeValue < 0)
						{
//							GameObject newInsert = (GameObject)Instantiate (Resources.Load ("Prefabs/blueSquareNoMovement"), 
//							                                                new Vector3 (grid [2, 0].transform.position.x, grid [2, 0].transform.position.y, 5f), 
							Debug.LogError("DownSwipe");
							ChangeGravity(2);
//							AddSquare ();
						}
					}

					else if ((Mathf.Abs ( swipeDistHorizontal) > minSwipeDistX) && (Mathf.Abs(swipeDistHorizontal) > Mathf.Abs(swipeDistVertical)))
					{
						float swipeValue = Mathf.Sign(swipeDistHorizontal);
						//right swipe
						if (swipeValue > 0)
						{
//							GameObject newInsert = (GameObject)Instantiate (Resources.Load ("Prefabs/blueSquareNoMovement"), 
//							                                                new Vector3 (grid [4, 2].transform.position.x, grid [2, 2].transform.position.y, 5f), 
//							                                                Quaternion.identity);

							Debug.LogError("RightSwipe");
							ChangeGravity(1);						
//							AddSquare ();
						}
						//left swipe
						else if (swipeValue < 0)
						{
//							GameObject newInsert = (GameObject)Instantiate (Resources.Load ("Prefabs/blueSquareNoMovement"), 
//							                                                new Vector3 (grid [0, 2].transform.position.x, grid [2, 2].transform.position.y, 5f), 
//							                                                Quaternion.identity);


							Debug.LogError("LeftSwipe");
							ChangeGravity(3);
//							AddSquare ();
						}
					}
					break;
				}
			}

			if (Input.GetKeyDown(KeyCode.Escape) && !howToMenuCreated)
			{
				RestartQuitOrHowToPlay();
			}
		}
	}	

	void ChangeGravity(int newGravDirection)
	{
		numDestroysThisRound = 1;
		gravtiy = newGravDirection;
		switch (newGravDirection) {
			//Gravity up
		case 0:
			for(int y = sizeY - 2; y >= 0; y--)
			{
				for (int x = 0; x < sizeX; x++)
				{
					if(gridSquareData[x, y].isOccupied() && gridSquareData[x, y + 1].IsFree())
					{
						gridSquareData[x, y + 1].AddSquare(gridSquareData[x, y].getColor());
						gridSquareData[x, y].Deoccupy();
						gridSquares[x, y + 1] = gridSquares[x, y];
						gridSquares[x, y] = null;
//						gridSquares[x, y + 1].transform.position = new Vector3(grid [x, y + 1].transform.position.x, grid [x, y + 1].transform.position.y,
//						                                                       5f);
						iTween.MoveTo(gridSquares[x, y + 1], new Vector3(grid [x, y + 1].transform.position.x, grid [x, y + 1].transform.position.y, 5f), translationTime);
						squareOnClick sOC = gridSquares[x, y + 1].GetComponent<squareOnClick>();
						sOC.y += 1;
					}
				}
			}
			break;
			//Gravity Right
		case 1:
			for(int x = sizeX - 2; x >= 0; x--)
			{
				for (int y = 0; y < sizeY; y++)
				{
					if(gridSquareData[x, y].isOccupied() &&  gridSquareData[x + 1, y].IsFree())
					{
						gridSquareData[x + 1, y].AddSquare(gridSquareData[x, y].getColor());
						gridSquareData[x, y].Deoccupy();
						gridSquares[x + 1, y] = gridSquares[x, y];
						gridSquares[x, y] = null;
//						gridSquares[x + 1, y].transform.position = new Vector3(grid [x + 1, y].transform.position.x, grid [x + 1, y].transform.position.y,
//						                                                       5f);
						iTween.MoveTo(gridSquares[x +1, y], new Vector3 (grid [x + 1, y].transform.position.x, grid [x + 1, y].transform.position.y, 5f), translationTime);
						squareOnClick sOC = gridSquares[x + 1, y].GetComponent<squareOnClick>();
						sOC.x += 1;
					}
				}
			}
			break;
			//Gravity Down
		case 2:
			for(int y = 1; y < sizeY; y++)
			{
				for (int x = 0; x < sizeX; x++)
				{
					if(gridSquareData[x, y].isOccupied() && gridSquareData[x, y - 1].IsFree())
					{
						gridSquareData[x, y - 1].AddSquare(gridSquareData[x, y].getColor());
						gridSquareData[x, y].Deoccupy();
						gridSquares[x, y - 1] = gridSquares[x, y];
						gridSquares[x, y] = null;
//						gridSquares[x, y - 1].transform.position = new Vector3(grid [x, y - 1].transform.position.x, grid [x, y - 1].transform.position.y,
//						                                                       5f);
						iTween.MoveTo(gridSquares[x, y - 1], new Vector3(grid [x, y - 1].transform.position.x, grid [x, y - 1].transform.position.y, 5f), translationTime);
						squareOnClick sOC = gridSquares[x, y - 1].GetComponent<squareOnClick>();
						sOC.y -= 1;
					}
				}
			}
			break;
			//Gravity Left
		case 3:
			for(int x = 1; x < sizeX; x++)
			{
				for (int y = 0; y < sizeY; y++)
				{
					if(gridSquareData[x, y].isOccupied() && gridSquareData[x - 1, y].IsFree())
					{
						gridSquareData[x - 1, y].AddSquare(gridSquareData[x, y].getColor());
						gridSquareData[x, y].Deoccupy();
						gridSquares[x - 1, y] = gridSquares[x, y];
						gridSquares[x, y] = null;
//						gridSquares[x - 1, y].transform.position = new Vector3(grid [x - 1, y].transform.position.x, grid [x - 1, y].transform.position.y,
//						                                                       5f);
						iTween.MoveTo(gridSquares[x - 1, y], new Vector3(grid [x - 1, y].transform.position.x, grid [x - 1, y].transform.position.y, 5f), translationTime);
						squareOnClick sOC = gridSquares[x - 1, y].GetComponent<squareOnClick>();
						sOC.x -= 1;
					}
				}
			}
			break;
		}
		AddSquare ();
	}

	public void AddSquare()
	{
		if (numSquaresInGrid == sizeX * sizeY) {
			for (int row = 0; row < sizeX; row++) {
				for( int column = 0; column < sizeY; column++)
				{
					squareOnClick sOC2 = gridSquares[row, column].GetComponent<squareOnClick>();
					if( sOC2.CheckForMatches())
					{
						return;
					}
				}
			}
			Debug.LogError("Here");
			YouLose();
			return;
		}
		bool foundPos = false;
		int x = 0;
		int y = 0;
		string squareColor = "blue";

		while (!foundPos) {
			x = Random.Range(0, sizeX);
			y = Random.Range (0, sizeY);


			if(gridSquares[x,y] == null) { foundPos = true; }

		}


		//0 = blue, 1 = gray, 2 = green, 3 = red
		int whichSquare = Random.Range (0, 4);

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



		GameObject newObj = (GameObject) Instantiate (Resources.Load ("Prefabs/" + squareColor + "SquareNoMovement"), new Vector3(grid [x, y].transform.position.x, grid [x, y].transform.position.y,
		                                                                                         5f), Quaternion.identity);
		gridSquares [x, y] = newObj;
		gridSquareData [x, y].AddSquare (whichSquare);

		squareOnClick sOC = newObj.GetComponent<squareOnClick> ();
		sOC.x = x;
		sOC.y = y;
		numSquaresInGrid++;
		
		
	}

	void RestartQuitOrHowToPlay()
	{
		howToMenuCreated = true;
		
		GameObject emptyGOParent = (GameObject)Instantiate (Resources.Load ("Prefabs/howToMenuGO"), new Vector3 (0, 0, 0), Quaternion.identity);
		GameObject wTD = (GameObject) Instantiate (Resources.Load ("Prefabs/whatToDo"), Camera.main.ViewportToWorldPoint (new Vector3 (.5f, .5f, 4f)), Quaternion.identity);
//		GameObject restart = (GameObject)Instantiate (Resources.Load ("Prefabs/Restart"), Camera.main.ViewportToWorldPoint (new Vector3 (.2f, .45f, 3f)), Quaternion.identity);
//		GameObject hTP = (GameObject)Instantiate (Resources.Load ("Prefabs/howToPlay"), Camera.main.ViewportToWorldPoint (new Vector3 (.52f, .45f, 3f)), Quaternion.identity);
//		GameObject quit1 = (GameObject)Instantiate (Resources.Load ("Prefabs/quit"), Camera.main.ViewportToWorldPoint (new Vector3 (.82f, .45f, 3f)), Quaternion.identity);

		Renderer wTDRend = wTD.GetComponent<Renderer> ();

		Vector3 exitSignPos = Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, 3f));
		exitSignPos.x = exitSignPos.x + (wTDRend.bounds.size.x * 9/ 20);
		exitSignPos.y = exitSignPos.y + (wTDRend.bounds.size.y * 5 / 12);
		GameObject exitSign = (GameObject)Instantiate(Resources.Load ("Prefabs/exitSign"), exitSignPos, Quaternion.identity);


		Vector3 restartPos = Camera.main.ViewportToWorldPoint (new Vector3 (.5f, .45f, 3f));
		restartPos.x = restartPos.x - wTDRend.bounds.size.x * 13 / 40;
		GameObject restart = (GameObject)Instantiate (Resources.Load ("Prefabs/Restart"), restartPos, Quaternion.identity);

		Vector3 quitPos = Camera.main.ViewportToWorldPoint (new Vector3 (.5f, .45f, 3f));
		quitPos.x = quitPos.x + wTDRend.bounds.size.x * 27 / 80;
		GameObject quit1 = (GameObject)Instantiate (Resources.Load ("Prefabs/quit"), quitPos, Quaternion.identity);

		Vector3 hTPPos = Camera.main.ViewportToWorldPoint (new Vector3 (.52f, .45f, 3f));
		hTPPos.x = wTD.transform.position.x + wTDRend.bounds.size.x * 1 / 60;

		GameObject hTP = (GameObject)Instantiate (Resources.Load ("Prefabs/howToPlay"), hTPPos, Quaternion.identity);




		GameObject menuParent = GameObject.FindGameObjectWithTag ("howToMenuGO");
		wTD.transform.parent = menuParent.transform;
		restart.transform.parent = menuParent.transform;
		hTP.transform.parent = menuParent.transform;
		quit1.transform.parent = menuParent.transform;
		exitSign.transform.parent = menuParent.transform;
		
	}

	public void RestartGame()
	{

		for (int row = 0; row < sizeX; row++) {
			for( int column = 0; column < sizeY; column++)
			{
				Destroy(gridSquares[row, column]);
				gridSquares[row, column] = null;
				gridSquareData[row, column].Deoccupy();
			}
		}


		setUpGamePage setUpScript = GameObject.FindGameObjectWithTag ("emptyGO").GetComponent<setUpGamePage> ();
		setUpScript.score = 0;


		GameObject scoreTextGO = GameObject.FindGameObjectWithTag ("scoreGO");
		TextMesh scoreText = scoreTextGO.GetComponent<TextMesh> ();


		scoreText.text = "" + 0;


		numSquaresInGrid = 0;
		AddSquare ();

	}

	void YouLose()
	{
		Instantiate (Resources.Load ("Prefabs/gameOverPrefab"), Camera.main.ViewportToWorldPoint (new Vector3 (.5f, .5f, 4.2f)), Quaternion.identity);

		GameObject scoreAndBestText = GameObject.FindGameObjectWithTag ("gameOverText");
		TextMesh textMesh = scoreAndBestText.GetComponent<TextMesh> ();

		setUpGamePage setUpScript = GameObject.FindGameObjectWithTag ("emptyGO").GetComponent<setUpGamePage> ();

		textMesh.text =  ("Score:\t" + setUpScript.score + "\nBest:\t\t" + setUpScript.highScore);
	}
}








