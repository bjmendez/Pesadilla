﻿using System.Collections;
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
	public IntRange numRooms = new IntRange (15, 20);         // The range of the number of rooms there can be.
	public IntRange roomWidth = new IntRange (3, 10);         // The range of widths rooms can have.
	public IntRange roomHeight = new IntRange (3, 10);        // The range of heights rooms can have.
	public IntRange corridorLength = new IntRange (6, 10);    // The range of lengths corridors between rooms can have.
	public int corridorWidth;
	public GameObject[] floorTiles;                           // An array of floor tile prefabs.                          
	public GameObject[] outerWallTiles;                       // An array of outer wall tile prefabs.
	public GameObject player;
	public GameObject enemy; 
	public GameObject exit;

	private int ranNum; 
	private TileType[][] tiles;                               // A jagged array of tile types representing the board, like a grid.
	public Room[] rooms;                                     // All the rooms that are created for this board.
	private Corridor[] corridors;                             // All the corridors that connect the rooms.
	private GameObject boardHolder;                           // GameObject that acts as a container for all other tiles.
	private Text UIText;									 //Text object displaying our controls GUI.

	public void BoardSceneSetUp ()
	{
		// Create the board holder.
		boardHolder = new GameObject("BoardHolder");

		SetupTilesArray ();

		CreateRoomsAndCorridors ();

		SetTilesValuesForRooms ();

		SetTilesValuesForCorridors ();

		InstantiateTiles ();

		DisplayUIText ();

		
		//InstantiateOuterWalls ();
	}

	void DisplayUIText()
	//Displays controls for the user to play the game, object is destroyed after '4' seconds
	{
		UIText = GameObject.Find ("ControlsGUI").GetComponent<Text>();
		Destroy (UIText, 10);
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
		rooms = new Room[numRooms.Random];

		// There should be one less corridor than there is rooms.


		// Create the first room and corridor.
		rooms[0] = new Room ();
	

		// Setup the first room, there is no previous corridor so we do not use one.
		rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);
		if (rooms.Length == 1) {
			Vector3 playerPos = new Vector3 (rooms[0].xPos+5, rooms[0].yPos, 0);
			Instantiate(player, playerPos, Quaternion.identity);
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

			if (i == Mathf.Floor(rooms.Length * .5f))
			{
				Vector3 playerPos = new Vector3 (rooms[i].xPos + 10, rooms[i].yPos+10, 0);
				Instantiate(player, playerPos, Quaternion.identity);



			}


		}
		ranNum = Random.Range(0,1); 

		if (ranNum == 0) {
			Vector3 exitPos = new Vector3 (rooms[0].xPos + 10, rooms[0].yPos + 20, 0);
			Instantiate(exit, exitPos, Quaternion.identity);
		}
		else{
			Vector3 exitPos = new Vector3 (rooms[1].xPos + 10, rooms[1].yPos + 20, 0);
			Instantiate(exit, exitPos, Quaternion.identity);
		}

	}


	public int GetRoomLength(){
		return rooms.Length;
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
					ranNum = Random.Range(0,250); 
					if (ranNum == 5 && i != Mathf.Floor(rooms.Length * .5f)) {
						Vector3 enemyPos = new Vector3 (xCoord, yCoord, 0);
						Instantiate(enemy, enemyPos, Quaternion.identity);
					}

					tiles[xCoord][yCoord] = TileType.Floor;

				}
			}
		}

	}


	void SetTilesValuesForCorridors ()
	{
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
		// Go through all the tiles in the jagged array...
		for (int i = 0; i < tiles.Length; i++)
		{
			for (int j = 0; j < tiles[i].Length; j++)
			{

				if (tiles [i] [j] == TileType.Floor) {
					// ... and instantiate a floor tile for it.

					InstantiateFromArray (floorTiles, i, j);
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
