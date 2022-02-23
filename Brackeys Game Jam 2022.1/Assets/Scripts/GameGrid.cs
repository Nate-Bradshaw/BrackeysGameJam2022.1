using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    // https://www.youtube.com/watch?v=qkSSdqOAAl4 tutorial, currently at 7 mins

    private int hieght = 10;
    private int width = 10;
    private float gridSpaceSize = 5f;

    [SerializeField] private GameObject gridCellPrefab;
    private GameObject[,] gameGrid;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        gameGrid = new GameObject[hieght, width];

        if(gridCellPrefab == null) //catch
        {
            Debug.LogError("ERROR: Grid Cell Prefab is unassigned");
        }

        for(int y = 0; y < hieght; y++) //make grid, y axis first
        {
            for (int x = 0; x < width; x++)
            {
                gameGrid[x, y] = Instantiate(gridCellPrefab, new Vector3(x * gridSpaceSize, y * gridSpaceSize), Quaternion.identity);
                gameGrid[x, y].transform.parent = transform;
                gameGrid[x, y].gameObject.name = "Grid Space ( X: " + x.ToString() + " Y: " + y.ToString() + ")";
            }
        }
        this.transform.position = new Vector3(-2.5f, -2.5f, 0); // for simplicty, could remove
    }
}
