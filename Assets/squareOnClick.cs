using UnityEngine;
using System.Collections;

public class squareOnClick : MonoBehaviour {

	GameObject[,] gridSquares;
	GridSquare[,] gridSquareData;
	GameObject[] squaresToDestroy;
	

	public int x = 0;
	public int y = 0;

	int sizeX = 5;
	int sizeY = 5;

	float translationTime = .2f;

	int amountToAddOnMatchSizeThree = 10;
	int amountToAddOnMatchSizeFour = 20;
	int amountToAddOnMatchSizeFive = 50;

	int currentPosInSquareArray = 0;

	int score;

	GameObject multiplier;
	bool isBeingMultiplied = false;

	gameScript gs;
	setUpGamePage setUpScript;

	GameObject scoreTextGO;

	// Use this for initialization
	void Start () {
		gs = GameObject.FindGameObjectWithTag ("emptyGO").GetComponent<gameScript> ();
		gridSquares = gs.gridSquares;
		gridSquareData = gs.gridSquareData;

		setUpScript = GameObject.FindGameObjectWithTag ("emptyGO").GetComponent<setUpGamePage> ();

		squaresToDestroy = new GameObject[sizeX * sizeY];

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		StartCoroutine (SquareStuff ());
	}

	private IEnumerator SquareStuff()
	{
		int currentColor = gridSquareData [x, y].getColor ();
		int currentY = y;
		int currentX = x;
		
		int upMatch = CheckUp (currentColor, currentY + 1);
		int downMatch = CheckDown (currentColor, currentY - 1);
		int rightMatch = CheckRight (currentColor, currentX + 1);
		int leftMatch = CheckLeft (currentColor, currentX - 1);

		Debug.LogError ("Upmatch= " + upMatch);
		Debug.LogError ("downMatch= " + downMatch);
		Debug.LogError ("rightMatch= " + rightMatch);
		Debug.LogError ("leftMatch= " + leftMatch);
		
		if ((upMatch + downMatch) == 0 && (rightMatch + leftMatch) == 0) {
			Debug.LogError("No match");
			return false;
		}
		
		//Matches both horizontall and vertically
		if ((upMatch + downMatch > 1) && (rightMatch + leftMatch > 1)) {
			if (upMatch + downMatch == 4 && rightMatch + leftMatch == 4) {
				//FIXME: Automatic win. Didn't think this was possible.
			} else {
				//Up
				for (int i = 1; i <= upMatch; i++) {
					if (x + i > sizeY) {
						Debug.LogError ("Went too high trying to delete");
					}
					if(gridSquares[x, y + i] == null)
					{
						Debug.LogError("it was null/" + x +"   " + y +"  "+ i);
					}
					iTween.MoveTo(gridSquares [x, y + i], gameObject.transform.position, translationTime);
					squaresToDestroy[currentPosInSquareArray++] = gridSquares[x, y + i];
					gridSquares [x, y + i] = null;
					gridSquareData [x, y + i].Deoccupy ();
					gs.numSquaresInGrid--;
				}
				//Down
				for (int i = 1; i <= downMatch; i++) {
					if (y - i < 0) {
						Debug.LogError ("Went too low trying to delete");
					}
					if(gridSquares[x, y - i] == null)
					{
						Debug.LogError("it was null/" + x +"  " + y +"  "+ i);
					}
					iTween.MoveTo(gridSquares [x, y - i], gameObject.transform.position, translationTime);
					squaresToDestroy[currentPosInSquareArray++] = gridSquares[x, y - i];
					gridSquares [x, y - i] = null;
					gridSquareData [x, y - i].Deoccupy ();
					gs.numSquaresInGrid--;
				}
				//Right
				for (int i = 1; i <= rightMatch; i++) {
					if (x + i > sizeX) {
						Debug.LogError ("Went too right trying to delete");
					}
					if(gridSquares[x + i, y] == null)
					{
						Debug.LogError("it was null/" + x +"  " + y +"  "+ i);
					}
					iTween.MoveTo(gridSquares [x + i, y], gameObject.transform.position, translationTime);
					squaresToDestroy[currentPosInSquareArray++] = gridSquares[x + i, y];
					gridSquares [x + i, y] = null;
					gridSquareData [x + i, y].Deoccupy ();
					gs.numSquaresInGrid--;
				}
				
				//Left
				for (int i = 1; i <= leftMatch; i++) {
					if (x - i < 0) {
						Debug.LogError ("Went too left trying to delete");
					}
					if(gridSquares[x - i, y] == null)
					{
						Debug.LogError("it was null/" + x +"  " + y +"  " +i);
					}
					iTween.MoveTo(gridSquares [x - i, y], gameObject.transform.position, translationTime);
					squaresToDestroy[currentPosInSquareArray++] = gridSquares[x - i, y];
					gridSquares [x - i, y] = null;
					gridSquareData [x - i, y].Deoccupy ();
					gs.numSquaresInGrid--;
				}
			}
			AddScore((upMatch + downMatch + rightMatch + leftMatch), true);

			squaresToDestroy[currentPosInSquareArray++] = gridSquares[x, y];
			gridSquares [x, y] = null;
			gridSquareData [x, y].Deoccupy ();
			gs.numSquaresInGrid--;
			
			
			yield return new WaitForSeconds (translationTime);
			DestroySquares();


		}
		
		
		//Vertical Match
		else if ((upMatch + downMatch) > (rightMatch + leftMatch) && (upMatch + downMatch > 1)) {
			Debug.LogError("Vert match");
			//Up
			for (int i = 1; i <= upMatch; i++) {
				if (x + i > sizeY) {
					Debug.LogError ("Went too high trying to delete");
				}
				if(gridSquares[x, y + i] == null)
				{
					Debug.LogError("it was null/" + x +"  " + y +"   " +i);
				}
				iTween.MoveTo(gridSquares [x, y + i], gameObject.transform.position, translationTime);
				squaresToDestroy[currentPosInSquareArray++] = gridSquares[x, y + i];
				gridSquares [x, y + i] = null;
				gridSquareData [x, y + i].Deoccupy ();
				gs.numSquaresInGrid--;
			}
			//Down
			for (int i = 1; i <= downMatch; i++) {
				if (y - i < 0) {
					Debug.LogError ("Went too low trying to delete");
				}
				if(gridSquares[x, y - i] == null)
				{
					Debug.LogError("it was null/" + x +" |  " + y + "  | " + i);
				}
				iTween.MoveTo(gridSquares [x, y - i], gameObject.transform.position, translationTime);
				squaresToDestroy[currentPosInSquareArray++] = gridSquares[x, y - i];
				gridSquares [x, y - i] = null;
				gridSquareData [x, y - i].Deoccupy ();
				gs.numSquaresInGrid--;
			}
			AddScore((upMatch + downMatch), false);

			squaresToDestroy[currentPosInSquareArray++] = gridSquares[x, y];
			gridSquares [x, y] = null;
			gridSquareData [x, y].Deoccupy ();
			gs.numSquaresInGrid--;
			
			
			yield return new WaitForSeconds (translationTime);
			DestroySquares();

			
		}
		
		//Horizontal Match
		else if(rightMatch + leftMatch > 1){
			Debug.LogError("Hor match");
			//Right
			for (int i = 1; i <= rightMatch; i++) {
				if (x + i > sizeX) {
					Debug.LogError ("Went too right trying to delete");
				}
				if(gridSquares[x + i, y] == null)
				{
					Debug.LogError("it was null/" + x +"  " + y +"  " +i);
				}
				iTween.MoveTo(gridSquares [x + i, y], gameObject.transform.position, translationTime);
				squaresToDestroy[currentPosInSquareArray++] = gridSquares[x + i, y];
				gridSquares [x + i, y] = null;
				gridSquareData [x + i, y].Deoccupy ();
				gs.numSquaresInGrid--;
			}
			
			//Left
			for (int i = 1; i <= leftMatch; i++) {
				if (x - i < 0) {
					Debug.LogError ("Went too left trying to delete");
				}
				if(gridSquares[x - i, y] == null)
				{
					Debug.LogError("it was null/" + x +"  " + y +"  " +i);
				}
				iTween.MoveTo(gridSquares [x - i, y], gameObject.transform.position, translationTime);
				squaresToDestroy[currentPosInSquareArray++] = gridSquares[x - i, y];
				gridSquares [x - i, y] = null;
				gridSquareData [x - i, y].Deoccupy ();
				gs.numSquaresInGrid--;
			}
			AddScore(rightMatch + leftMatch, false);

			squaresToDestroy[currentPosInSquareArray++] = gridSquares[x, y];
			gridSquares [x, y] = null;
			gridSquareData [x, y].Deoccupy ();
			gs.numSquaresInGrid--;
			
			
			yield return new WaitForSeconds (translationTime);

			DestroySquares();
		}
	}

	void DestroySquares()
	{
		if(isBeingMultiplied)
		{
			Debug.LogError("Got here assdfasdf");
			isBeingMultiplied = false;
			Destroy(multiplier);
		}
		for (int i = 0; i < currentPosInSquareArray; i++) {
			Destroy(squaresToDestroy[i]);
			squaresToDestroy[i] = null;
			Debug.LogError("i= " + i);
		}
		currentPosInSquareArray = 0;
	}

	//Searches the whole grid for any matches, returns true if there is
	public bool CheckForMatches()
	{
		int currentColor = gridSquareData [x, y].getColor ();
		int currentY = y;
		int currentX = x;
		
		int upMatch = CheckUp (currentColor, currentY + 1);
		int downMatch = CheckDown (currentColor, currentY - 1);
		int rightMatch = CheckRight (currentColor, currentX + 1);
		int leftMatch = CheckLeft (currentColor, currentX - 1);

		return ((upMatch + downMatch > 1) || (rightMatch + leftMatch > 1));
	}

	int CheckDown(int currentColor, int currentY)
	{
		if (currentY == -1) {
			return 0;
		}


		if(gridSquareData[x, currentY].getColor() != currentColor)
		{
			return 0;
		}
		else {
			return 1 + CheckDown(currentColor, currentY - 1);
		}
	}

	int CheckUp(int currentColor, int currentY)
	{
		if (currentY == sizeY) {
			return 0;
		}
		
		
		if(gridSquareData[x, currentY].getColor() != currentColor)
		{
			return 0;
		}
		else {
			return 1 + CheckUp(currentColor, currentY + 1);
		}
	}

	int CheckRight(int currentColor, int currentX)
	{
		if (currentX == sizeX) {
			return 0;
		}
		
		
		if(gridSquareData[currentX, y].getColor() != currentColor)
		{
			return 0;
		}
		else {
			return 1 + CheckRight(currentColor, currentX + 1);
		}
	}

	int CheckLeft(int currentColor, int currentX)
	{
		if (currentX == -1) {
			return 0;
		}
		
		
		if(gridSquareData[currentX, y].getColor() != currentColor)
		{
			return 0;
		}
		else {
			return 1 + CheckLeft(currentColor, currentX - 1);
		}
	}

	void AddScore(int numMatches, bool twoDirectional)
	{
		
		scoreTextGO = GameObject.FindGameObjectWithTag ("scoreGO");
		TextMesh scoreText = scoreTextGO.GetComponent<TextMesh> ();

		int currentScore = setUpScript.score;

		if (gs.numDestroysThisRound > 1) {
			string num = "Two";
			switch(gs.numDestroysThisRound)
			{
			case 2: 
				break;
			case 3:
				num = "Three";
				break;
			case 4:
				num = "Four";
				break;
			case 5:
				num = "Five";
				break;
			}

			multiplier = (GameObject)Instantiate(Resources.Load ("Prefabs/times" + num), gameObject.transform.position, Quaternion.identity);
			isBeingMultiplied = true;
		}

		if (!twoDirectional) {
			switch (numMatches) {
			case 2: 
				scoreText.text = "" + (currentScore + (amountToAddOnMatchSizeThree * gs.numDestroysThisRound));
				setUpScript.score = currentScore + (amountToAddOnMatchSizeThree * gs.numDestroysThisRound);
				gs.numDestroysThisRound++;
				break;
			case 3:
				scoreText.text = "" + (currentScore + (amountToAddOnMatchSizeFour * gs.numDestroysThisRound));
				setUpScript.score = currentScore + (amountToAddOnMatchSizeFour * gs.numDestroysThisRound);
				gs.numDestroysThisRound++;
				break;
			case 4:
				scoreText.text = "" + (currentScore + (amountToAddOnMatchSizeFive * gs.numDestroysThisRound));
				setUpScript.score = currentScore + (amountToAddOnMatchSizeFive * gs.numDestroysThisRound);
				gs.numDestroysThisRound++;
				break;
			}
		}
		else{			
			scoreText.text = "" + (currentScore + numMatches * 25 * gs.numDestroysThisRound);
			setUpScript.score = currentScore + numMatches * 25 * gs.numDestroysThisRound;
					gs.numDestroysThisRound++;
		}
		
		if (setUpScript.score > setUpScript.highScore) {
			TextMesh bestText = GameObject.FindGameObjectWithTag("bestTextGO").GetComponent<TextMesh>();
			bestText.text = "" + setUpScript.score;
			setUpScript.highScore = setUpScript.score;
			PlayerPrefs.SetInt("highScore", setUpScript.score);
		}

	}
}
