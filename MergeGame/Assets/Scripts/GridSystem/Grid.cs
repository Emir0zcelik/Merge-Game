using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class Grid<T>
{
    private int _cellSize;

    public T[,] _grid;

    public void SetItem(Vector2Int gridPosition, T item)
    {
        _grid[gridPosition.x, gridPosition.y] = item;
    }

    public T GetItem(Vector2Int gridPosition)
    {
        return _grid[gridPosition.x, gridPosition.y];
    }

    public Grid(T[,] grid, int cellSize)
    {
        _grid = grid;
        _cellSize = cellSize;
    }

    public Vector3 GridToWorldPosition(Vector2Int gridPosition)
    {
        return (new Vector3(gridPosition.x, gridPosition.y) * _cellSize) + (Vector3)(Vector2.one * _cellSize / 2);
    }

    public Vector2Int WorldToGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / _cellSize);
        int y = Mathf.FloorToInt(worldPosition.y / _cellSize);

        return new Vector2Int(x, y);
    }

    public Vector3 GetCenterPosition()
    {
        return new Vector3(_grid.GetLength(0) * _cellSize * 0.5f, _grid.GetLength(1) * _cellSize * 0.5f, 0);
    }

    public bool IsClicked(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit);
    }

}
