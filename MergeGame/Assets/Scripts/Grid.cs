using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _offsetColor;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private GameObject _highlight;

    public void Init(bool isOffset)
    {
        if(isOffset)
        {
            _sprite.color = _offsetColor;
        }
        else
        {
            _sprite.color = _baseColor;
        }
    }

    void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
    }


}
