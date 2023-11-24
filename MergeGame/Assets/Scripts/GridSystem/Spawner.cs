using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Tower> _towerPrefabList;

    GridManager manager;
    Grid<FloorTile> _floorGrid;

    public void Start()
    {
        manager = GetComponent<GridManager>();
        _floorGrid = manager._floorGrid;

        for (int x = 0; x < _floorGrid._grid.GetLength(0); x++)
        {
            for (int y = 0; y < _floorGrid._grid.GetLength(1); y++)
            {
                Vector2Int gridPosition = new Vector2Int(x, y);
                
                int random = Random.Range(0, _towerPrefabList.Count);

                Tower tower = Instantiate(_towerPrefabList[random], _floorGrid.GridToWorldPosition(gridPosition), Quaternion.identity);

                tower.towerType = (TowerType)random + 1;

                tower.transform.localScale = Vector3.zero;

                tower.transform.DOScale(new Vector3(1, 1, 1), 1f);

                _floorGrid._grid[x,y] = new FloorTile 
                {
                    Tower = tower, 
                    TileBackground = _floorGrid._grid[x,y].TileBackground,
                };

            }
        }
    }

    public void Spawn(List<Vector2Int> matchedXnY)
    {
        for (int x = 0; x < _floorGrid._grid.GetLength(0); x++)
        {
            for (int y = 0; y < _floorGrid._grid.GetLength(1); y++)
            {
                for (int i = 0; i < matchedXnY.Count; i++)
                {
                    if(x == matchedXnY[i].x && y == matchedXnY[i].y)
                    {
                        Vector2Int gridPosition = new Vector2Int(x, y);

                        int random = Random.Range(0, _towerPrefabList.Count);

                        Tower tower = Instantiate(_towerPrefabList[random], _floorGrid.GridToWorldPosition(gridPosition), Quaternion.identity);

                        tower.towerType = (TowerType)random + 1;

                        tower.transform.localScale = Vector3.zero;

                        tower.transform.DOScale(new Vector3(1, 1, 1), 1f);

                        _floorGrid._grid[x, y] = new FloorTile
                        {
                            Tower = tower,
                            TileBackground = _floorGrid._grid[x, y].TileBackground,
                        };
                    }
                }
            }
        }
    }
}
