using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtension 
{
    public static int GetVector2IntSize(Vector2Int vector2Int)
    {
        return Mathf.Abs(vector2Int.x) + Mathf.Abs(vector2Int.y);
    }
}
