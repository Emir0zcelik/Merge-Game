using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
using System;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Tower> _towerPrefabList;

    GridManager gridManager;
    Grid<FloorTile> _floorGrid;
    Action tileSpawnedEvent;



    private void Awake()
    {
        gridManager = GetComponent<GridManager>();
        _floorGrid = gridManager._floorGrid;
        FillEmptyTiles();
    }

    private void OnEnable()
    {
        gridManager.destroyedEvent += FillEmptyTiles;
    }

    private void OnDisable()
    {
        gridManager.destroyedEvent -= FillEmptyTiles;
    }

    public bool TrySpawn(TowerType towerType,int level,Vector2Int gridPosition, out Tower tower)
    {
        Tower towerToInstantiate = default;
        bool isTowerFound = false;
        
        for (int i = 0; i < _towerPrefabList.Count; i++)
        {
            if (_towerPrefabList[i].towerType == towerType && _towerPrefabList[i].towerLevel == level)
            {
                towerToInstantiate = _towerPrefabList[i];
                isTowerFound = true;
                break;
            }
        }

        if (isTowerFound)
        {
                    
            Tower instantiatedTower = Instantiate(towerToInstantiate);
            tower = instantiatedTower;
            
            _floorGrid._grid[gridPosition.x, gridPosition.y] = new FloorTile
            {
                Tower = instantiatedTower,
                TileBackground = _floorGrid._grid[gridPosition.x, gridPosition.y].TileBackground,
            };

            instantiatedTower.transform.position = _floorGrid.GridToWorldPosition(gridPosition);
            instantiatedTower.transform.localScale = Vector3.zero;
            instantiatedTower.transform.DORotate(new Vector3(-180f, 360f, 360f), 1f);
            instantiatedTower.transform.DOScale(new Vector3(1f, 1f, 1f), 1f);

            return true;
        }
        else
        {
            tower = default;

            return false;
        }
        

    }

    private void FillEmptyTiles()
    {
        
        int enumCount = Enum.GetNames(typeof(TowerType)).Length;
        int enumStartIndex = 1;

        for (int x = 0; x < _floorGrid._grid.GetLength(0); x++)
        {
            for (int y = 0; y < _floorGrid._grid.GetLength(1); y++)
            {
                if(_floorGrid._grid[x,y].Tower != null)
                    continue;

                TowerType randomType = (TowerType)UnityEngine.Random.Range(enumStartIndex, enumCount);

                Vector2Int gridPosition = new Vector2Int(x, y);
                
                if (!TrySpawn(randomType, 0,gridPosition , out Tower spawnedTower))
                {
                    Debug.Log("MISSING PREFAB!!");
                }
            }
        }
    }
}
