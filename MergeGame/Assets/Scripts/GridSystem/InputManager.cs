using UnityEngine;

namespace GridSystem
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private GridManager _gridManager;

        [SerializeField] private Camera _camera;



        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Plane plane = new Plane(-Vector3.forward, Vector3.zero);

                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (plane.Raycast(ray, out float distance))
                {
                    Vector3 worldPosition = ray.GetPoint(distance);

                    Vector2Int gridPosition = _gridManager._floorGrid.WorldToGridPosition(worldPosition);

                    _gridManager.firstTowerPosition = gridPosition;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                Plane plane = new Plane(-Vector3.forward, Vector3.zero);

                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (plane.Raycast(ray, out float distance))
                {
                    Vector3 worldPosition = ray.GetPoint(distance);

                    Vector2Int gridPosition = _gridManager._floorGrid.WorldToGridPosition(worldPosition);

                    Vector2Int gridDifference = gridPosition - _gridManager.firstTowerPosition;

                    _gridManager.lastTowerPosition = gridPosition;

                    _gridManager.lastTowerType = _gridManager._floorGrid._grid[_gridManager.firstTowerPosition.x, _gridManager.firstTowerPosition.y].Tower.towerType;


                    if (VectorExtension.GetVector2IntSize(gridDifference) < 2 && _gridManager._floorGrid._grid[_gridManager.firstTowerPosition.x, _gridManager.firstTowerPosition.y].Tower != null && _gridManager._floorGrid._grid[gridPosition.x, gridPosition.y].Tower != null)
                    {
                        _gridManager.SwapTower(_gridManager.firstTowerPosition, gridPosition);

                        _gridManager.CheckMatches();
                    }
                }
            }
        }

    }
}