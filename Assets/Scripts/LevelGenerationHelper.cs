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
            if((value % 2) != 0)
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
        createdRooms = new List<Vector2>(); //This list will have all the rooms cordinates that are created.
        //This is to be able to randomly pick one of them easily
        //Setup grid with a zero in every cell. Zero means no room.
        GenerateEmptyGrid();
        //Populate center piece and add it to the locator list. One means room.
        PlaceRoom(gridSize / 2, gridSize / 2);

        //This statment checks if enough rooms have been created.
        while(createdRooms.Count < levelSize)
        {
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

    private void PlaceRoom(int x, int y)
    {
        //makes the selected tile 1 and add it to the room list
        roomPlacementGrid[x, y] = 1;
        createdRooms.Add(new Vector2(x, y));
    }

    private void PickARandomRoomAndDirection()
    {
        int chosenRoomNumber, chosenDirection;
        //variables to hold the random values
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

    private Vector2 Direction(int index)
    {
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

    private bool Obstructed(Vector2 chosenRoom, int chosenDirection)
    {
        /*
         * 1 = NORTH
         * 2 = WEST
         * 3 = SOUTH
         * 4 = EAST
         */

        //TOP
        if (chosenDirection == 1)
        {
            if (roomPlacementGrid[(int)chosenRoom.x, (int)chosenRoom.y + 1] == 1)
                return true;
            else
                return false;
        }
        // WEST
        if (chosenDirection == 2)
        {
            if (roomPlacementGrid[(int)chosenRoom.x - 1, (int)chosenRoom.y] == 1)
                return true;
            else
                return false;
        }
        // SOUTH
        if (chosenDirection == 3)
        {
            if (roomPlacementGrid[(int)chosenRoom.x, (int)chosenRoom.y - 1] == 1)
                return true;
            else
                return false;
        }
        // EAST
        if (chosenDirection == 4)
        {
            if (roomPlacementGrid[(int)chosenRoom.x + 1, (int)chosenRoom.y] == 1)
                return true;
            else
                return false;
        }

        return false;
    }

    private int NumberOfOccupiedNeighbours(Vector2 centerRoom)
    {
        int counter = 0;
        for(int i = 1; i < 5; i++)
        {
            //Direction(i) is for the the suronding rooms
            if (roomPlacementGrid[(int)(centerRoom + Direction(i)).x, (int)(centerRoom + Direction(i)).y] != 0)
            {
                counter++;
            }
        }
        return counter;
    }

    private void WriteGridToConsole()
    {
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
