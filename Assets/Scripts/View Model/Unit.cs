using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Tile tile { get; protected set; }
    public Directions dir;

    public void Place(Tile t)
    {
        if (tile != null && tile.content == gameObject)
        {
            tile.content = null;
        }

        tile = t;

        if (tile != null)
        {
            tile.content = gameObject;
        }
    }

    public void Match()
    {
        transform.localPosition = tile.center;
        transform.localEulerAngles = dir.ToEuler();
    }
}
