using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerationHelper {

    private int[,] _roomPlacementGrid;
    private List<Vector2> _createdRooms;
    private int _gridSize, _levelSize; //gridSize defines the dimensions of the grid. LevelSize defines the amount of rooms.
    #region properties

    public int[,] RoomPlacementGrid
    {
        get { return _roomPlacementGrid; }
        set { _roomPlacementGrid = value; }
    }
    public List<Vector2> CreatedRooms
    {
        get { return _createdRooms; }
        set { _createdRooms = value; }
    }
    public int GridSize
    {
        get { return _gridSize; }
        private set //Every grid needs to have a center so this part makes sure that the sides are uneven
        {
            if ((value % 2) != 0)
            {
                _gridSize = value;
            }
            else
            {
                _gridSize = value + 1;
            }
        }
    }
    public int LevelSize
    {
        get { return _levelSize; }
    }

    public LevelGenerationHelper(int levelSize)
    {
        _gridSize = levelSize * 2;
        _levelSize = levelSize;

        GenerateLevel(); //Needs to be done if i want to regenerate at a later time
    }
    #endregion

    private void GenerateLevel()
    {
        RoomPlacementGrid = new int[GridSize, GridSize];//Creates the main grid
        CreatedRooms = new List<Vector2>(); //This list include have all the cordinates of the rooms that are created.
        //This is to be able to easily randomly pick one of them.
        //Setup grid with a zero in every cell. Zero means no room.
        GenerateEmptyGrid();
        //Populate center piece and add it to the locator list. Two means starting room.
        PlaceRoom(GridSize / 2, GridSize / 2, 2);

        //This statment checks if enough rooms have been created.
        while (CreatedRooms.Count < LevelSize)
        {
            if (CreatedRooms.Count == LevelSize - 2)
                PlaceSpecialRoom(3); // End room
            else if (CreatedRooms.Count == LevelSize - 1)
                PlaceSpecialRoom(4); // Loot Room
            else
                PickARandomRoomAndDirection();
        }

    }

    private void GenerateEmptyGrid()
    {
        //Loops through every space in the grid and sets it to 0 (empty)
        for (int y = 0; y < GridSize; y++)
            for (int x = 0; x < GridSize; x++)
                RoomPlacementGrid[x, y] = 0;
    }

    private void PlaceRoom(int x, int y, int roomIndex)
    {
        //makes the selected cell 1 and add it to the room list
        RoomPlacementGrid[x, y] = roomIndex;
        CreatedRooms.Add(new Vector2(x, y));
    }

    private void PickARandomRoomAndDirection()
    {
        int chosenRoomNumber, chosenDirection;
        //Local Variables to hold the random values
        do
        {
            chosenRoomNumber = Random.Range(0, CreatedRooms.Count);
            chosenDirection = Random.Range(1, 5);
            //Checks if the selected slot is occupied or not
        } while (ObstructedAndNeighbours(CreatedRooms[chosenRoomNumber], chosenDirection));
        //If the room isn't obstucted then we place a room. createdRooms is a Vector2 list.
        PlaceRoom(CreatedRooms[chosenRoomNumber], chosenDirection);
    }


    private bool ObstructedAndNeighbours(Vector2 chosenRoom, int chosenDirection)
    {
        //Checks if there is a room where you are about to place yours and if the room has more than 1 neighbor
        //Makes the level more branching 
        Vector2 resultingVector = chosenRoom + Direction(chosenDirection);
        if (NumberOfOccupiedNeighbours(resultingVector) <= 1 && !Obstructed(chosenRoom, chosenDirection))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void PlaceRoom(Vector2 roomCordinates, int directionIndex)
    {
        //Sets the direction Vector based on the direction specified by the properties
        Vector2 direction = Vector2.zero;
        Vector2 resultingRoom;
        direction = Direction(directionIndex);

        resultingRoom = roomCordinates + direction;

        RoomPlacementGrid[(int)resultingRoom.x, (int)resultingRoom.y] = 1;
        CreatedRooms.Add(new Vector2((int)resultingRoom.x, (int)resultingRoom.y));

    }
    private void PlaceSpecialRoom(int roomIndex)
    {
        int chosenRoomNumber, chosenDirection;
        //variables to hold the random values
        do
        {
            chosenRoomNumber = Random.Range(0, CreatedRooms.Count);
            chosenDirection = Random.Range(1, 5);
            //Checks if the selected slot is occupied or not
        } while (ObstructedAndNeighbours(CreatedRooms[chosenRoomNumber], chosenDirection));


        Vector2 resultingRoom = CreatedRooms[chosenRoomNumber] + Direction(chosenDirection);

        RoomPlacementGrid[(int)resultingRoom.x, (int)resultingRoom.y] = roomIndex;
        CreatedRooms.Add(new Vector2((int)resultingRoom.x, (int)resultingRoom.y));

    }

    private void ReplaceRoom(int roomIndex)
    {
        int chosenRoomNumber;
        Vector2 resultingRoom;
        //variables to hold the random values
        do
        {
            chosenRoomNumber = Random.Range(0, CreatedRooms.Count);
            resultingRoom = CreatedRooms[chosenRoomNumber];
            //Checks if the selected slot is occupied or not
        } while (NumberOfOccupiedNeighbours(resultingRoom) > 1 && 
        RoomPlacementGrid[(int)resultingRoom.x, (int)resultingRoom.y] != 1);

        RoomPlacementGrid[(int)resultingRoom.x, (int)resultingRoom.y] = roomIndex;
    }

    private Vector2 Direction(int index)
    {
        //Method for converting a number to a direction
        /*
        * 1 = NORTH
        * 2 = WEST
        * 3 = SOUTH
        * 4 = EAST
        */

        if (index == 1)
            return Vector2.up;
        else if (index == 2)
            return Vector2.left;
        else if (index == 3)
            return Vector2.down;
        else if (index == 4)
            return Vector2.right;
        else
            return Vector2.zero;
    }

    private int NumberOfOccupiedNeighbours(Vector2 centerRoom)
    {
        //Checking all the neighbors of a selected room
        int counter = 0;
        for (int i = 1; i < 5; i++)
        {
            //Direction(i) is for the the suronding rooms
            if (RoomPlacementGrid[(int)(centerRoom + Direction(i)).x, (int)(centerRoom + Direction(i)).y] != 0)
            {
                counter++;
            }
        }
        return counter;
    }

    private bool Obstructed(Vector2 chosenRoom, int chosenDirection)
    {
        /*
         * 1 = NORTH
         * 2 = WEST
         * 3 = SOUTH
         * 4 = EAST
         */
        Vector2 resultingPosition = chosenRoom + Direction(chosenDirection);
        if (RoomPlacementGrid[(int)resultingPosition.x, (int)resultingPosition.y] == 0)
            return false;
        else
            return true;
    }

    
    private void WriteGridToConsole()
    {
        //Used for debug
        for (int y = 0; y < GridSize; y++)
        {
            string output = "";
            for (int x = 0; x < GridSize; x++)
            {
                output += RoomPlacementGrid[x, y].ToString();
            }
            Debug.Log(output);
        }
    }

}
