using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class GridManager : MonoBehaviour
{
    public Grid<FloorTile> _floorGrid;

    [SerializeField] private Camera _camera;

    [SerializeField] private GameObject _tileBackground;

    [SerializeField] private Color orginalColor;
    [SerializeField] private Color offSetColor;

    private Spawner _spawner;

    private bool isDestroy = false;

    private Vector2Int firstTowerPosition;
    private Vector2Int lastTowerPosition;
    private TowerType lastTowerType;

    public Action destroyedEvent;


    private void Awake()
    {
        _spawner = GetComponent<Spawner>();

        InitizalizeGrid();

        for (int x = 0; x < _floorGrid._grid.GetLength(0); x++)
        {
            for (int y = 0; y < _floorGrid._grid.GetLength(1); y++)
            {
                Vector3 worldPosition = _floorGrid.GridToWorldPosition(new Vector2Int(x, y));

                GameObject background = Instantiate(_tileBackground, worldPosition, Quaternion.identity);
                

                _floorGrid._grid[x, y].TileBackground = background;
            }
        }
    }

    private void Start()
    {
        CheckMatches();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(-Vector3.forward, Vector3.zero);

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 worldPosition = ray.GetPoint(distance);

                Vector2Int gridPosition = _floorGrid.WorldToGridPosition(worldPosition);

                firstTowerPosition = gridPosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Plane plane = new Plane(-Vector3.forward, Vector3.zero);

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 worldPosition = ray.GetPoint(distance);

                Vector2Int gridPosition = _floorGrid.WorldToGridPosition(worldPosition);

                Vector2Int gridDifference = gridPosition - firstTowerPosition;

                lastTowerPosition = gridPosition;

                lastTowerType = _floorGrid._grid[firstTowerPosition.x, firstTowerPosition.y].Tower.towerType;

                if (VectorExtension.GetVector2IntSize(gridDifference) < 2 && _floorGrid._grid[firstTowerPosition.x, firstTowerPosition.y].Tower != null && _floorGrid._grid[gridPosition.x, gridPosition.y].Tower != null)
                {
                    SwapTower(firstTowerPosition, gridPosition);

                    CheckMatches();
                }
            }
        }
    }

    private void InitizalizeGrid()
    {
        _floorGrid = new Grid<FloorTile>(new FloorTile[6, 6], 1);

        FloorTile floorTile = new FloorTile();
    }

    private void SwapTower(Vector2Int first, Vector2Int second)
    {
        FloorTile tempTile;

        tempTile = _floorGrid._grid[first.x, first.y];
        _floorGrid._grid[first.x, first.y] = _floorGrid._grid[second.x, second.y];
        _floorGrid._grid[second.x, second.y] = tempTile;


        _floorGrid._grid[first.x, first.y].Tower.transform.position = _floorGrid.GridToWorldPosition(first);
        _floorGrid._grid[second.x, second.y].Tower.transform.position = _floorGrid.GridToWorldPosition(second);
    }

    void CheckMatches()
    {
        GetMatches(out List<Vector2Int> matchedXnY);

        print("matchedXnY.count: " + matchedXnY.Count);

        if (matchedXnY.Count <= 0)
        {
            return;
        }

        DestroyMatches(matchedXnY);
    }

    void DestroyMatches(List<Vector2Int> matchedXnY)
    {
        for (int i = 0; i < matchedXnY.Count; i++)
        {
            if (_floorGrid._grid[matchedXnY[i].x, matchedXnY[i].y].Tower == null)
            {
                continue;
            }

            //print(_floorGrid._grid[matchedXnY[i].x + matchedXnY[i].y)
            Destroy(_floorGrid._grid[matchedXnY[i].x, matchedXnY[i].y].Tower.gameObject);
            _floorGrid.SetItem(matchedXnY[i], new FloorTile());
        }

        _spawner.TrySpawn(lastTowerType, 1,lastTowerPosition, out Tower tower);
        
        destroyedEvent.Invoke();

        matchedXnY.Clear();
    }

    void GetMatches(out List<Vector2Int> matchedXnY)
    {
        matchedXnY = new List<Vector2Int>();

        for (int x = 0; x < _floorGrid._grid.GetLength(0); x++)
        {
            for (int y = 0; y < _floorGrid._grid.GetLength(1); y++)
            {
                if (_floorGrid._grid[x,y].Tower != null)
                {
                    GetNeighboor(x, y, _floorGrid._grid[x, y].Tower, out int countX, out int countY);

                    if (countX >= 2 || countY >= 2)
                    {
                        matchedXnY.Add(new Vector2Int(x, y));
                    }
                }
            }
        }
    }

    void GetNeighboor(int x, int y, Tower targetTower, out int countX, out int countY)
    {
        int currentX = 0;
        int currentY = 0;

        countX = 0;
        countY = 0;
        
        currentX = x + 1;
        while (_floorGrid.IsInDimensions(currentX,y)  && _floorGrid._grid[currentX, y].Tower != null && _floorGrid._grid[currentX, y].Tower.IsEquals(targetTower))
        {
            countX++;
            currentX++;
        }
        
        currentX = x - 1;
        while (_floorGrid.IsInDimensions(currentX,y)  && _floorGrid._grid[currentX, y].Tower != null && _floorGrid._grid[currentX, y].Tower.IsEquals(targetTower))
        {
            countX++;
            currentX--;
        }

        currentY = y + 1;
        while (_floorGrid.IsInDimensions(x,currentY)  && _floorGrid._grid[x,currentY].Tower != null && _floorGrid._grid[x,currentY].Tower.IsEquals(targetTower))
        {
            countY++; 
            currentY++;
        }

        currentY = y - 1;
        while (_floorGrid.IsInDimensions(x,currentY)  && _floorGrid._grid[x,currentY].Tower != null && _floorGrid._grid[x,currentY].Tower.IsEquals(targetTower))
        {
            countY++;
            currentY--;
        }
    }
}
