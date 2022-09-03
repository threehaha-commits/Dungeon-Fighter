using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IChangeBarColor
{
    void ChangeColor(Color color);
}

public class BarColorHandler : MonoBehaviour, IChangeBarColor
{
    private SpriteRenderer Render;

    private void Start()
    {
        Render = GetComponent<SpriteRenderer>();
    }

    void IChangeBarColor.ChangeColor(Color color)
    {
        Render.color = color;
    }
}
