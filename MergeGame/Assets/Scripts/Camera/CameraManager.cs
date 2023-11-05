using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;

    private void Start()
    {
        Vector3 centerPosition = _gridManager._floorGrid.GetCenterPosition();
        centerPosition.z = gameObject.transform.position.z;
        gameObject.transform.position = centerPosition;
    }
}
