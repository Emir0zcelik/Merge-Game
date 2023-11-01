using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;

    private void Start()
    {
        gameObject.transform.position = _gridManager._floorGrid.GetCenterPosition();
    }
}
