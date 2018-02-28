using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerationHelper {

    private int[,] _roomPlacementGrid;
    private List<Vector2> _createdRooms;
    private int _gridSize, _levelSize; //gridSize defines the dimensions of the grid. LevelSize defines the amount of rooms.
    #region properties

    public int[,] roomPlacementGrid
    {
        get { return _roomPlacementGrid; }
        set { _roomPlacementGrid = value; }
    }
    public List<Vector2> createdRooms
    {
        get { return _createdRooms; }
        set { _createdRooms = value; }
    }
    public int gridSize
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
    public int levelSize
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
        roomPlacementGrid = new int[gridSize, gridSize];//Creates the main grid
        createdRooms = new List<Vector2>(); //This list include have all the cordinates of the rooms that are created.
        //This is to be able to easily randomly pick one of them.
        //Setup grid with a zero in every cell. Zero means no room.
        GenerateEmptyGrid();
        //Populate center piece and add it to the locator list. Two means starting room.
        PlaceRoom(gridSize / 2, gridSize / 2, 2);

        //This statment checks if enough rooms have been created.
        while (createdRooms.Count < levelSize)
        {
            if (createdRooms.Count == levelSize - 1)
                PlaceSpecialRoom(3);
            else
                PickARandomRoomAndDirection();
        }
    }

    private void GenerateEmptyGrid()
    {
        //Loops through every space in the grid and sets it to 0 (empty)
        for (int y = 0; y < gridSize; y++)
            for (int x = 0; x < gridSize; x++)
                roomPlacementGrid[x, y] = 0;
    }

    private void PlaceRoom(int x, int y, int roomIndex)
    {
        //makes the selected cell 1 and add it to the room list
        roomPlacementGrid[x, y] = roomIndex;
        createdRooms.Add(new Vector2(x, y));
    }

    private void PickARandomRoomAndDirection()
    {
        int chosenRoomNumber, chosenDirection;
        //Local Variables to hold the random values
        do
        {
            chosenRoomNumber = Random.Range(0, createdRooms.Count);
            chosenDirection = Random.Range(1, 5);
            //Checks if the selected slot is occupied or not
        } while (ObstructedAndNeighbours(createdRooms[chosenRoomNumber], chosenDirection));
        //If the room isn't obstucted then we place a room. createdRooms is a Vector2 list.
        PlaceRoom(createdRooms[chosenRoomNumber], chosenDirection);
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

        roomPlacementGrid[(int)resultingRoom.x, (int)resultingRoom.y] = 1;
        createdRooms.Add(new Vector2((int)resultingRoom.x, (int)resultingRoom.y));

    }
    private void PlaceSpecialRoom(int roomIndex)
    {
        int chosenRoomNumber, chosenDirection;
        //variables to hold the random values
        do
        {
            chosenRoomNumber = Random.Range(0, createdRooms.Count);
            chosenDirection = Random.Range(1, 5);
            //Checks if the selected slot is occupied or not
        } while (ObstructedAndNeighbours(createdRooms[chosenRoomNumber], chosenDirection));

        Vector2 resultingRoom;

        resultingRoom = createdRooms[chosenRoomNumber] + Direction(chosenDirection);

        roomPlacementGrid[(int)resultingRoom.x, (int)resultingRoom.y] = roomIndex;
        createdRooms.Add(new Vector2((int)resultingRoom.x, (int)resultingRoom.y));

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
            if (roomPlacementGrid[(int)(centerRoom + Direction(i)).x, (int)(centerRoom + Direction(i)).y] != 0)
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
        if (roomPlacementGrid[(int)resultingPosition.x, (int)resultingPosition.y] == 0)
            return false;
        else
            return true;
    }

    
    private void WriteGridToConsole()
    {
        //Used for debug
        for (int y = 0; y < gridSize; y++)
        {
            string output = "";
            for (int x = 0; x < gridSize; x++)
            {
                output += roomPlacementGrid[x, y].ToString();
            }
            Debug.Log(output);
        }
    }

}
