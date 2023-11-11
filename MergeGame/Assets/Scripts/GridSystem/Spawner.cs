using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Tower> _towerPrefabList;

    public void Start()
    {
        GridManager manager = GetComponent<GridManager>();
        Grid<FloorTile> _floorGrid = manager._floorGrid;

        for (int x = 0; x < _floorGrid._grid.GetLength(0); x++)
        {
            for (int y = 0; y < _floorGrid._grid.GetLength(1); y++)
            {
                Vector2Int gridPosition = new Vector2Int(x, y);
                
                int random = Random.Range(0, _towerPrefabList.Count);

                Tower tower = Instantiate(_towerPrefabList[random], _floorGrid.GridToWorldPosition(gridPosition), Quaternion.identity);

                tower.towerType = (TowerType)random + 1;

                _floorGrid._grid[x,y] = new FloorTile 
                {
                    Tower = tower, 
                    TileBackground = _floorGrid._grid[x,y].TileBackground,
                };

            }
        }
    }
}
