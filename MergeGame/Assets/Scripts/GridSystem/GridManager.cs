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

    private Vector2Int firstTowerPosition;

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

                var spriteRenderer = background.GetComponent<SpriteRenderer>();

                if ((x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0))
                {
                    spriteRenderer.color = orginalColor;
                }
                else
                {
                    spriteRenderer.color = offSetColor;
                }

                _floorGrid._grid[x, y].TileBackground = background;
            }
        }
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

                if (VectorExtension.GetVector2IntSize(gridDifference) < 2 && _floorGrid._grid[firstTowerPosition.x, firstTowerPosition.y].Tower != null && _floorGrid._grid[gridPosition.x, gridPosition.y].Tower != null)
                {
                    SwapTower(firstTowerPosition, gridPosition);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetMatches(out List<Vector2Int> matchedXnY);

            for (int i = 0; i < matchedXnY.Count; i++)
            {
                if (_floorGrid._grid[matchedXnY[i].x, matchedXnY[i].y].Tower == null)
                {
                    continue;
                }

                Destroy(_floorGrid._grid[matchedXnY[i].x, matchedXnY[i].y].Tower.gameObject);
                _floorGrid.SetItem(matchedXnY[i], new FloorTile());
            }

            _spawner.Spawn(matchedXnY);

            matchedXnY.Clear();
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

    public void GetMatches(out List<Vector2Int> matchedXnY)
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
        int currentX = x + 1;
        int currentY = y + 1;

        countX = 0;
        countY = 0;

        while (x < _floorGrid._grid.GetLength(0) && currentX < _floorGrid._grid.GetLength(0)  && _floorGrid._grid[currentX, y].Tower != null && _floorGrid.IsValidGridPosition(currentX, y) && _floorGrid._grid[currentX, y].Tower.towerType == targetTower.towerType)
        {
            countX++;
            currentX++;
        }


        currentX = x - 1;

        while (x > 0 && currentX >= 0 && _floorGrid._grid[currentX, y].Tower != null && _floorGrid.IsValidGridPosition(currentX, y) && _floorGrid._grid[currentX, y].Tower.towerType == targetTower.towerType)
        {
            countX++;
            currentX--;
            if (currentX < 0)
                break;
        }
        

        while (y < _floorGrid._grid.GetLength(1) && currentY < _floorGrid._grid.GetLength(1) && _floorGrid._grid[x, currentY].Tower != null && _floorGrid.IsValidGridPosition(x, currentY) && _floorGrid._grid[x, currentY].Tower.towerType == targetTower.towerType)
        {
            countY++; 
            currentY++;
        }

        currentY = y - 1;
        while (y > 0 && currentY >= 0 && _floorGrid._grid[x, currentY].Tower != null && _floorGrid.IsValidGridPosition(x, currentY) && _floorGrid._grid[x, currentY].Tower.towerType == targetTower.towerType)
        {
            countY++;
            currentY--;
        }
    }
}
