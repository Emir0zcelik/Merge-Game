using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;

    [SerializeField] private GameObject _gridPrefab;
    [SerializeField] private Camera _camera;

    private int[,] data;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {

                GameObject grid = Instantiate(_gridPrefab, new Vector3(x, y, 0), Quaternion.identity);
                grid.name = $"Grid {x} {y}";

                bool isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                grid.GetComponent<Grid>().Init(isOffset);

            }
        }
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                data[x, y] = 0;
                Debug.Log(x + " " + y);
            }
        }
        

        _camera.transform.position = new Vector3((float)_width / 2, (float)_height / 2, -10);
    }
}
