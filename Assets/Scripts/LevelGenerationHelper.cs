using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerationHelper {

    private int[,] _roomPlacementGrid;
    private List<Vector2> _createdRooms;
    private int _gridSize, _levelSize; //gridSize defines the dimensions of the grid. LevelSize defines the amount of rooms.
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

    public LevelGenerationHelper(int gridSize, int levelSize)
    {
        _gridSize = gridSize;
        _levelSize = levelSize;
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        roomPlacementGrid = new int[gridSize, gridSize];
        createdRooms = new List<Vector2>();
        //Setup grid with a zero in every cell. Zero means no room.
        GenerateEmptyGrid();
        //Populate center piece and add it to the locator list. One means room.
        PlaceRoom(gridSize / 2, gridSize / 2);

        while(createdRooms.Count < levelSize)
        {
            PickARandomRoomAndDirection();
        }
        WriteGridToConsole();
    }

    private void GenerateEmptyGrid()
    {
        for (int y = 0; y < gridSize; y++)
            for (int x = 0; x < gridSize; x++)
                roomPlacementGrid[x, y] = 0;
    }

    private void PlaceRoom(int x, int y)
    {
        roomPlacementGrid[x, y] = 1;
        createdRooms.Add(new Vector2(x, y));
    }

    private void PlaceRoom(Vector2 roomCordinates, int directionIndex)
    {
        Vector2 direction = Vector2.zero;
        Vector2 resultingRoom;
        if (directionIndex == 1)
            direction = Vector2.up;
        else if (directionIndex == 2)
            direction = Vector2.left;
        else if (directionIndex == 3)
            direction = Vector2.down;
        else if (directionIndex == 4)
            direction = Vector2.right;

        resultingRoom = roomCordinates + direction;

        roomPlacementGrid[(int)resultingRoom.x, (int)resultingRoom.y] = 1;
        createdRooms.Add(new Vector2((int)resultingRoom.x, (int)resultingRoom.y));

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

    private void PickARandomRoomAndDirection()
    {
        int chosenRoomNumber, chosenDirection;
        do
        {
            chosenRoomNumber = Random.Range(0, createdRooms.Count);
            chosenDirection = Random.Range(1, 4);
        } while (Obstructed(createdRooms[chosenRoomNumber], chosenDirection));

        PlaceRoom(createdRooms[chosenRoomNumber], chosenDirection);
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
