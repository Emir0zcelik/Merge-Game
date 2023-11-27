using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Tower> _towerPrefabList;

    GridManager manager;
    Grid<FloorTile> _floorGrid;
    Action tileSpawnedEvent;


    private void Awake()
    {
        manager = GetComponent<GridManager>();
        _floorGrid = manager._floorGrid;

        for (int x = 0; x < _floorGrid._grid.GetLength(0); x++)
        {
            for (int y = 0; y < _floorGrid._grid.GetLength(1); y++)
            {
                Vector2Int gridPosition = new Vector2Int(x, y);

                int random = UnityEngine.Random.Range(0, 4);

                Tower tower = Instantiate(_towerPrefabList[random], _floorGrid.GridToWorldPosition(gridPosition), Quaternion.identity);

                tower.towerType = (TowerType)random + 1;
                tower.towerLevel = 0;

                tower.transform.localScale = Vector3.zero;

                if (tower.towerType == TowerType.Wood)
                {
                    tower.transform.DOScale(new Vector3(75 / 2, 75 / 2, 75 / 2), 1f);
                    tower.transform.DORotate(new Vector3(180f, 0f, 0f), 0f);
                }

                if (tower.towerType == TowerType.Stone || tower.towerType == TowerType.Ice)
                {
                    tower.transform.DOScale(new Vector3(100 / 2, 100 / 2, 100 / 2), 1f);
                }

                if (tower.towerType == TowerType.Coal)
                {
                    tower.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 1f);
                }

                _floorGrid._grid[x, y] = new FloorTile
                {
                    Tower = tower,
                    TileBackground = _floorGrid._grid[x, y].TileBackground,
                };

            }
        }
    }



    public void Spawn(List<Vector2Int> matchedXnY, Vector2Int mergedTowerPosition, TowerType mergedTowerType)
    {
        for (int x = 0; x < _floorGrid._grid.GetLength(0); x++)
        {
            for (int y = 0; y < _floorGrid._grid.GetLength(1); y++)
            {
                for (int i = 0; i < matchedXnY.Count; i++)
                {
                    if(x == matchedXnY[i].x && y == matchedXnY[i].y)
                    {
                        Tower tower;

                        Vector2Int gridPosition = new Vector2Int(x, y);

                        if(x == mergedTowerPosition.x && y == mergedTowerPosition.y)
                        {
                            if (mergedTowerType == TowerType.Wood)
                            {
                                tower = Instantiate(_towerPrefabList[4], _floorGrid.GridToWorldPosition(gridPosition), Quaternion.identity);
                                tower.transform.position = new Vector3(tower.transform.position.x, tower.transform.position.y, -0.5f);
                                tower.transform.localScale = Vector3.zero;
                                tower.transform.DOScale(new Vector3(0.15f, 0.15f, 0.15f), 1f);
                                tower.transform.DORotate(new Vector3(270, 0, 0), 0f);

                            }

                            if (mergedTowerType == TowerType.Stone)
                            {
                                tower = Instantiate(_towerPrefabList[5], _floorGrid.GridToWorldPosition(gridPosition), Quaternion.identity);
                                tower.transform.position = new Vector3(tower.transform.position.x, tower.transform.position.y, -0.5f);
                                tower.transform.localScale = Vector3.zero;
                                tower.transform.DOScale(new Vector3(0.15f, 0.15f, 0.15f), 1f);
                                tower.transform.DORotate(new Vector3(270, 0, 0), 0f);
                                
                            }

                            if (mergedTowerType == TowerType.Ice)
                            {
                                tower = Instantiate(_towerPrefabList[6], _floorGrid.GridToWorldPosition(gridPosition), Quaternion.identity);
                                tower.transform.position = new Vector3(tower.transform.position.x, tower.transform.position.y, -0.5f);
                                tower.transform.localScale = Vector3.zero;
                                tower.transform.DOScale(new Vector3(0.15f, 0.15f, 0.15f), 1f);
                                tower.transform.DORotate(new Vector3(270, 0, 0), 0f);
                            }

                            if (mergedTowerType == TowerType.Coal)
                            {
                                tower = Instantiate(_towerPrefabList[7], _floorGrid.GridToWorldPosition(gridPosition), Quaternion.identity);
                                tower.transform.position = new Vector3(tower.transform.position.x, tower.transform.position.y, -0.5f);
                                tower.transform.localScale = Vector3.zero;
                                tower.transform.DOScale(new Vector3(0.15f, 0.15f, 0.15f), 1f);
                                tower.transform.DORotate(new Vector3(270, 0, 0), 0f);
                            }
                        }

                        if(x != mergedTowerPosition.x || y != mergedTowerPosition.y)
                        {
                            int random = UnityEngine.Random.Range(0, 4);

                            tower = Instantiate(_towerPrefabList[random], _floorGrid.GridToWorldPosition(gridPosition), Quaternion.identity);

                            tower.towerType = (TowerType)random + 1;

                            tower.transform.localScale = Vector3.zero;

                            if (tower.towerType == TowerType.Wood)
                            {
                                tower.transform.DOScale(new Vector3(75 / 2, 75 / 2, 75 / 2), 1f);
                                tower.transform.DORotate(new Vector3(180f, 0f, 0f), 0f);
                            }

                            if (tower.towerType == TowerType.Stone || tower.towerType == TowerType.Ice)
                            {
                                tower.transform.DOScale(new Vector3(100 / 2, 100 / 2, 100 / 2), 1f);
                            }

                            if (tower.towerType == TowerType.Coal)
                            {
                                tower.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 1f);
                            }

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
}
