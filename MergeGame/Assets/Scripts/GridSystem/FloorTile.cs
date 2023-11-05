using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

class Tower : MonoBehaviour
{
    public TowerType towerType;
}

public struct FloorTile
{
    public GameObject TileBackground;

}

public enum TowerType
{
    Empty, Wood, Stone, Ice, Coal
};

