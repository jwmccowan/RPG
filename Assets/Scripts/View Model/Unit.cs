using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Tile tile { get; protected set; }
    public Directions dir;

    public void Place(Tile t)
    {
        if (tile.content != null)
        {
            return;
        }

        if (tile != null && tile.content == gameObject)
        {
            tile = null;
        }

        tile = t;

        if (tile != null)
        {
            transform.position = tile.center;
        }
    }
}
