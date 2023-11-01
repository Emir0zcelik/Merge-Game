using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Grid<FloorTile> _floorGrid;

    [SerializeField] private Camera _camera;

    [SerializeField] private GameObject _tileBackground;

    [SerializeField] private Color orginalColor;
    [SerializeField] private Color offSetColor;

    private void Awake()
    {
        InitizalizeGrid();
    }

    private void Update()
    {
        Vector3 cameraOffset = new Vector3(0, 0, (new Vector3(0, 0, 0) - _camera.transform.position).magnitude);

        Vector3 worldPoint = _camera.ScreenToWorldPoint(Input.mousePosition + cameraOffset);
        Vector2Int gridPosition = _floorGrid.WorldToGridPosition(worldPoint);



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

    private void InitizalizeGrid()
    {
        _floorGrid = new Grid<FloorTile>(new FloorTile[10, 10], 1);

        FloorTile floorTile = new FloorTile();
    }
}
