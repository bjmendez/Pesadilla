using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class BoardCreator : MonoBehaviour
{
	// The type of tile that will be laid in a specific position.
	public enum TileType
	{
		Wall, Floor,
	}



	public int columns = 100;                                 // The number of columns on the board (how wide it will be).
	public int rows = 100;                                    // The number of rows on the board (how tall it will be).
	public int numRooms;        // The range of the number of rooms there can be.
	public IntRange roomWidth = new IntRange (3, 10);         // The range of widths rooms can have.
	public IntRange roomHeight = new IntRange (3, 10);        // The range of heights rooms can have.
	public IntRange corridorLength = new IntRange (6, 10);    // The range of lengths corridors between rooms can have.
	public int corridorWidth;
	public GameObject[] floorTiles;                           // An array of floor tile prefabs.
	public GameObject floorTile;
	public GameObject[] outerWallTiles;                       // An array of outer wall tile prefabs.
	public GameObject player;								  // player game object
	public GameObject enemy; 								 // enemy game object
	public GameObject enemy2;
	public GameObject exit;									// exit game object
	public GameObject boss1;

	private int ranNum; 									// random number
	public int enemycount =0;
	private TileType[][] tiles;                               // A jagged array of tile types representing the board, like a grid.
	public Room[] rooms;                                     // All the rooms that are created for this board.
	private Corridor[] corridors;                             // All the corridors that connect the rooms.
	private GameObject boardHolder;                           // GameObject that acts as a container for all other tiles.
    private Image UIImage;                                   //Image object displaying our controls GUI.
	private int enemynumber = 225;
	private Player playerScript;


	void Awake(){
		//playerScript = player.GetComponent<Player> ();

	}



    public void BoardSceneSetUp ()
	{
		// Create the board holder.



		playerScript = player.GetComponent<Player> ();

		boardHolder = new GameObject("BoardHolder");

		SetupTilesArray ();

		CreateRoomsAndCorridors ();

		SetTilesValuesForRooms ();

		SetTilesValuesForCorridors ();

		InstantiateTiles ();


		//InstantiateOuterWalls ();
	}

	void SetupTilesArray ()
	{
		// Set the tiles jagged array to the correct width.
		tiles = new TileType[columns][];

		// Go through all the tile arrays...
		for (int i = 0; i < tiles.Length; i++)
		{
			// ... and set each tile array is the correct height.
			tiles[i] = new TileType[rows];
		}
	}


	void CreateRoomsAndCorridors ()
	{
		// Create the rooms array with a random size.
		if(numRooms != 1){
			numRooms +=  playerScript.GetLevelCount();
			if (numRooms > 5) {
				numRooms = 5;
			}

		}

		rooms = new Room[numRooms];
		// There should be one less corridor than there is rooms.


		// Create the first room and corridor.
		rooms[0] = new Room ();


		// Setup the first room, there is no previous corridor so we do not use one.
		//primary use is boss room
		rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);
		if (rooms.Length == 1) {
			//spawn player in front of room
			Vector3 playerPos = new Vector3 (rooms[0].xPos+5, rooms[0].yPos + 3, 0);
			Instantiate(player, playerPos, Quaternion.identity);
			Vector3 BossPos = new Vector3  (rooms[0].xPos+5, rooms[0].yPos + 7, 0);
			enemycount++;
			//spawn boss
			Instantiate(boss1, BossPos, Quaternion.identity);

			//put exit door behind boss
			Vector3 exitPos = new Vector3 (rooms[0].xPos + 7, rooms[0].yPos + 15, 0);
			Instantiate(exit, exitPos, Quaternion.identity);
			return;
		}
		// Setup the first corridor using the first room.
		corridors = new Corridor[rooms.Length - 1];
		corridors[0] = new Corridor ();
		corridors[0].SetupCorridor(rooms[0], corridorLength, roomWidth, roomHeight, columns, rows, true);

		for (int i = 1; i < rooms.Length; i++)
		{
			// Create a room.
			rooms[i] = new Room ();

			// Setup the room based on the previous corridor.
			rooms[i].SetupRoom (roomWidth, roomHeight, columns, rows, corridors[i - 1]);

			// If we haven't reached the end of the corridors array...
			if (i < corridors.Length)
			{
				// ... create a corridor.
				corridors[i] = new Corridor ();

				// Setup the corridor based on the room that was just created.
				corridors[i].SetupCorridor(rooms[i], corridorLength, roomWidth, roomHeight, columns, rows, false);
			}

			//if  the first room
			if (i == Mathf.Floor(rooms.Length * .5f))
			{
				//spawn player in middle of room
				Vector3 playerPos = new Vector3 (rooms[i].xPos + 10, rooms[i].yPos+10, 0);
				Instantiate(player, playerPos, Quaternion.identity);



			}


		}
		ranNum = Random.Range(0,1);
		//randomly choose one of the furthest rooms on either the left or right to put exit to next level
		if (ranNum == 0) {
			Vector3 exitPos = new Vector3 (rooms[0].xPos + 12, rooms[0].yPos + 20, 0);
			Instantiate(exit, exitPos, Quaternion.identity);
		}
		else{
			Vector3 exitPos = new Vector3 (rooms[rooms.Length-1].xPos + 12, rooms[1].yPos + 20, 0);
			Instantiate(exit, exitPos, Quaternion.identity);
		}

	}

	//get the number of rooms
	public int GetRoomLength(){
		return rooms.Length;
	}

	public void decreaseEnemyCount(){
		enemycount--;
	}

	public int GetEnemyCount(){
		return enemycount;
	}

	void SetTilesValuesForRooms ()
	{
		// Go through all the rooms...
		for (int i = 0; i < rooms.Length; i++)
		{
			Room currentRoom = rooms[i];

			// ... and for each room go through it's width.
			for (int j = 0; j < currentRoom.roomWidth; j++)
			{
				int xCoord = currentRoom.xPos + j;

				// For each horizontal tile, go up vertically through the room's height.
				for (int k = 0; k < currentRoom.roomHeight; k++)
				{
					int yCoord = currentRoom.yPos + k;

					// The coordinates in the jagged array are based on the room's position and it's width and height.
					//give every tile a 1 in X chance of spawning an enemy on top of it

					int tempy = 0;
					tempy = playerScript.GetLevelCount();
					int rng = 25 * tempy;

					ranNum = Random.Range(0,enemynumber-rng);
					if (ranNum == 5 && i != Mathf.Floor(rooms.Length * .5f) && enemycount < tempy +1) {
						Vector3 enemyPos = new Vector3 (xCoord, yCoord, 0);
						Vector3 enemy2Pos = new Vector3 (xCoord, yCoord, 0);

						enemycount+=2;

						Instantiate(enemy, enemyPos, Quaternion.identity);
						Instantiate (enemy2, enemy2Pos, Quaternion.identity);

					}

					tiles[xCoord][yCoord] = TileType.Floor;

				}
			}
		}

	}


	void SetTilesValuesForCorridors ()
	{
		//no need for corrdiors if only one room
		if (rooms.Length == 1) {
			return;
		}
		// Go through every corridor...
		for (int i = 0; i < corridors.Length; i++)
		{
			Corridor currentCorridor = corridors[i];




			// and go through it's length.
			for (int j = 0; j < currentCorridor.corridorLength; j++) {
				// Start the coordinates at the start of the corridor.
				int xCoord = currentCorridor.startXPos;
				int yCoord = currentCorridor.startYPos;
				// Depending on the direction, add or subtract from the appropriate
				// coordinate based on how far through the length the loop is.
				switch (currentCorridor.direction) {
				case Direction.North:
					yCoord += j;
					break;
				case Direction.East:
					xCoord += j;
					break;
				case Direction.South:
					yCoord -= j;
					break;
				case Direction.West:
					xCoord -= j;
					break;
				}

				//Similar to doing the corridor length as above this does corridor width
				for (int k = 0; k < corridorWidth; k++) {


					switch (currentCorridor.direction) {
					case Direction.North:
						xCoord = currentCorridor.startXPos;
						xCoord += k;
						break;
					case Direction.East:
						yCoord = currentCorridor.startYPos;
						yCoord += k;
						break;
					case Direction.South:
						xCoord = currentCorridor.startXPos;
						xCoord -= k;
						break;
					case Direction.West:
						yCoord = currentCorridor.startYPos;
						yCoord -= k;
						break;
					}

					tiles [xCoord] [yCoord] = TileType.Floor;
				}
				// Set the tile at these coordinates to Floor.

			}


		}
	}


	void InstantiateTiles ()
	{
		int temp = 0;
		temp = Random.Range (0, 4);
		if (temp == 0) {
			floorTile = floorTiles [0];
		} else if (temp == 1) {
			floorTile = floorTiles [1];
		} else if (temp == 2) {
			floorTile = floorTiles [2];
		} else if (temp == 3) {
			floorTile = floorTiles [3];
		} else {
			floorTile = floorTiles [4];
		}
		// Go through all the tiles in the jagged array...
		for (int i = 0; i < tiles.Length; i++)
		{
			for (int j = 0; j < tiles[i].Length; j++)
			{

				if (tiles [i] [j] == TileType.Floor) {
					// ... and instantiate a floor tile for it.
					Vector3 tilePos = new Vector3(i,j,0);
					Instantiate (floorTile, tilePos, Quaternion.identity);
				}

				// If the tile type is Wall...
				else if (tiles[i][j] == TileType.Wall)
				{
					// ... instantiate a wall over the top.
					InstantiateFromArray (outerWallTiles, i, j);
				}
			}
		}
	}




	void InstantiateFromArray (GameObject[] prefabs, float xCoord, float yCoord)
	{
		// Create a random index for the array.
		int randomIndex = Random.Range(0, prefabs.Length);

		// The position to be instantiated at is based on the coordinates.
		Vector3 position = new Vector3(xCoord, yCoord, 0f);

		// Create an instance of the prefab from the random index of the array.
		GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

		// Set the tile's parent to the board holder.
		tileInstance.transform.parent = boardHolder.transform;
	}
}
