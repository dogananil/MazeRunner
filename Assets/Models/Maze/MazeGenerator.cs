using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    // Public variables
    public int mazeWidth = 10;
    public int mazeHeight = 10;
    public float wallLength = 1f;
    public Transform wallPrefab;

    // Private variables
    private int[,] maze;
    private Vector2 mazeStartPosition;

    // Generate maze function
    public void GenerateMaze()
    {
        // Initialize maze array
        maze = new int[mazeWidth, mazeHeight];

        // Set entrance and exit positions
        mazeStartPosition = new Vector2(0, Random.Range(0, mazeHeight));
        Vector2 mazeEndPosition = new Vector2(mazeWidth - 1, Random.Range(0, mazeHeight));

        // Mark entrance and exit positions in maze
        maze[(int)mazeStartPosition.x, (int)mazeStartPosition.y] = 2;
        maze[(int)mazeEndPosition.x, (int)mazeEndPosition.y] = 3;

        // Generate maze walls
        Vector3 initialPosition = new Vector3(-mazeWidth / 2f + 0.5f, 0, -mazeHeight / 2f + 0.5f);
        List<Transform> walls = new List<Transform>();

        // Generate left and right walls
        for (int row = 0; row < mazeHeight; row++)
        {
            for (int col = 0; col <= mazeWidth; col++)
            {
                if (maze[col, row] == 1 || col == 0 || col == mazeWidth)
                {
                    Transform wall = Instantiate(wallPrefab, new Vector3(initialPosition.x + (col * wallLength) - (wallLength / 2), 0, initialPosition.z + (row * wallLength)), Quaternion.Euler(0, 90, 0)) as Transform;
                    walls.Add(wall);
                }
            }
        }

        // Generate top and bottom walls
        for (int row = 0; row <= mazeHeight; row++)
        {
            for (int col = 0; col < mazeWidth; col++)
            {
                if (maze[col, row] == 1 || row == 0 || row == mazeHeight)
                {
                    Transform wall = Instantiate(wallPrefab, new Vector3(initialPosition.x + (col * wallLength), 0, initialPosition.z + (row * wallLength) - (wallLength / 2)), Quaternion.identity) as Transform;
                    walls.Add(wall);
                }
            }
        }
    }
}
