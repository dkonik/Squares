using UnityEngine;
using System.Collections;

public class setUpGamePage : MonoBehaviour {

	public bool shouldSetPosition;
	int numColumns = 5;
	int numRows = 5;
	float tileOffset = 1f;
	public float gridTileWidth;
	public Vector3 bottomLeftTilePos;
	public gameScript gs;
	GameObject scoreBox;
	GameObject bestBox;
	GameObject scoreBoxTextGO;
	GameObject bestBoxTextGO;

	public TextMesh scoreText;
	public TextMesh bestText;
	int fontSize = 42;
	float charSize = .2f;

	public int score = 0;
	public int highScore;


	//Grid: (0,0) is bottom left?
	public GameObject[,] grid;
	
	// Use this for initialization
	void Start () {
		gs = gameObject.GetComponent<gameScript> ();
		shouldSetPosition = false;
		grid = new GameObject[numRows, numColumns];
		gs.grid = grid;
		gs.canMakeSquares = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (shouldSetPosition) {
			shouldSetPosition = false;
			gs.canMakeSquares = true;

			GameObject.FindGameObjectWithTag("startupPage").GetComponent<setMainScreenLayout>().enabled = false;

			Vector3 topLeftLogoPos = Camera.main.ViewportToWorldPoint (new Vector3(.45f, .85f, 10f));
			GameObject topLeftLogo = (GameObject)Instantiate (Resources.Load ("Prefabs/squaresLogo"), topLeftLogoPos, Quaternion.identity);

			//Creates the scorebox with default value 0
			scoreBox = (GameObject)Instantiate(Resources.Load ("Prefabs/scoreBox"), Camera.main.ViewportToWorldPoint(new Vector3(.4f, .7f, 10f)), Quaternion.identity);
			scoreBoxTextGO = (GameObject) Instantiate(Resources.Load ("Prefabs/scoreTextGO"), Camera.main.ViewportToWorldPoint(new Vector3(.4f, .68f, 9f)), Quaternion.identity);
			scoreText = scoreBoxTextGO.GetComponent<TextMesh>();
			scoreText.text = "" + score;
			scoreText.fontSize = fontSize;
			scoreText.characterSize = charSize;

			highScore = PlayerPrefs.GetInt("highScore", 0);
			bestBox = (GameObject)Instantiate(Resources.Load("Prefabs/bestBox"), Camera.main.ViewportToWorldPoint(new Vector3( .8f, .7f, 10f)), Quaternion.identity);
			bestBoxTextGO = (GameObject) Instantiate(Resources.Load ("Prefabs/bestTextGO"), Camera.main.ViewportToWorldPoint(new Vector3(.8f, .68f, 9f)), Quaternion.identity);
			bestText = bestBoxTextGO.GetComponent<TextMesh>();
			bestText.text = "" + highScore;
			bestText.fontSize = fontSize;
			bestText.characterSize = charSize;

			Instantiate(Resources.Load("Prefabs/restartButton"), Camera.main.ViewportToWorldPoint( new Vector3( .93f, .95f, 4f)), Quaternion.identity);

			SetPosition ();
		}
	}
	
	void SetPosition()
	{
		Vector3 gamePageBackgroundPos = Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, 20f));
		GameObject gamePageBackground = (GameObject) Instantiate (Resources.Load ("Prefabs/gamePageBackground"), gamePageBackgroundPos, Quaternion.identity);
		gamePageBackground.transform.SetParent(gameObject.transform);

		CreateGrid ();
	}

	void CreateGrid()
	{
		GameObject emptyGO2 = GameObject.FindGameObjectWithTag("emptyGO");
		Vector3 middleTilePos = Camera.main.ViewportToWorldPoint(new Vector3(.5f, .35f, 10f));
		GameObject middleTile = (GameObject) Instantiate (Resources.Load ("Prefabs/tile"), middleTilePos, Quaternion.identity);
		grid [2, 2] = middleTile;
		middleTile.transform.SetParent(emptyGO2.transform);
		
		Renderer rend = middleTile.GetComponent<Renderer>();
		gridTileWidth = rend.bounds.max.x - rend.bounds.min.x;
		
		
		for(int i = 0; i < numRows; i++)
		{
			int adjustedX = i - 2;
			for(int j = 0; j < numColumns; j++)
			{

				if(i != 2 || j != 2)
				{
					int adjustedY = j - 2;
					Vector3 newGridTilePos;

					newGridTilePos = new Vector3(middleTile.transform.position.x + (adjustedX * gridTileWidth), 
					                             middleTile.transform.position.y + (adjustedY * gridTileWidth),
					                             10f);

					GameObject gridTile = (GameObject) Instantiate(Resources.Load("Prefabs/tile"), newGridTilePos, Quaternion.identity);
					grid[i, j] = gridTile;
					gridTile.transform.SetParent(emptyGO2.transform);

				}


				                                         
			}
		}

		gs.AddSquare();

//		GameObject newInsert = (GameObject)Instantiate (Resources.Load ("Prefabs/blueSquareNoMovement"), new Vector3 (grid [2, 2].transform.position.x, grid [2, 2].transform.position.y,
//		                                                                                                    5f), Quaternion.identity);
//		GameObject newInsert2 = (GameObject)Instantiate (Resources.Load ("Prefabs/blueSquareNoMovement"), new Vector3 (grid [0, 0].transform.position.x, grid [0, 0].transform.position.y,
//		                                                                                                              5f), Quaternion.identity);
	}
				                                              
	
}















