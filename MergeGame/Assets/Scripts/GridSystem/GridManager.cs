using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private void Start()
    {
        

    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(-Vector3.forward, Vector3.zero);

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 worldPosition = ray.GetPoint(distance);

                Vector2Int gridPosition = _floorGrid.WorldToGridPosition(worldPosition);


            }
        }
    }

    private void InitizalizeGrid()
    {
        _floorGrid = new Grid<FloorTile>(new FloorTile[6, 6], 1);

        FloorTile floorTile = new FloorTile();
    }
}
