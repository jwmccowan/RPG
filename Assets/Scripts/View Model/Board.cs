﻿using System.Collections.Generic;
using System;
using UnityEngine;

public class Board : MonoBehaviour
{
    #region Properties
    [SerializeField] GameObject tilePrefab;
    public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();

    Color selectedTileColor = new Color(0, 1, 1, 1);
    Color defaultTileColor = new Color(1, 1, 1, 1);

    Point[] dirs = new Point[4]
    {
        new Point(0,1),
        new Point(0,-1),
        new Point(1,0),
        new Point(-1,0)
    };
    #endregion

    #region
    public List<Tile> Search(Tile start, Func<Tile,Tile, bool> addTile)
    {
        List<Tile> list = new List<Tile>();
        list.Add(start);
        ClearSearch();
        Queue<Tile> checkNow = new Queue<Tile>();
        Queue<Tile> checkNext = new Queue<Tile>();

        start.distance = 0;
        checkNow.Enqueue(start);

        while(checkNow.Count > 0)
        {
            Tile t = checkNow.Dequeue();
            for (int i = 0; i < 4; i++)
            {
                Tile next = GetTile(t.pos + dirs[i]);
                if (next == null)
                {
                    continue;
                }

                if (next.distance < t.distance || list.Contains(next))
                {
                    continue;
                }

                next.distance = t.distance + 1;
                next.prev = t;

                if (addTile(t, next))
                {
                    checkNext.Enqueue(next);
                    list.Add(next);
                }
            }

            if (checkNow.Count == 0)
            {
                SwapReference(ref checkNow, ref checkNext);
            }
        }


        return list;
    }

    void SwapReference(ref Queue<Tile> a, ref Queue<Tile> b)
    {
        Queue<Tile> tmp = a;
        a = b;
        b = tmp;
    }

    void ClearSearch()
    {
        foreach(Tile t in tiles.Values)
        {
            t.prev = null;
            t.distance = int.MaxValue;
        }
    }

    public Tile GetTile(Point p)
    {
        if (tiles.ContainsKey(p))
        {
            return tiles[p];
        }
        return null;
    }

    public void Load(LevelData data)
    {
        List<Vector3> tilePositions = data.tilePositions;
        for (int i = 0; i < tilePositions.Count; i++)
        {
            GameObject instance = Instantiate(tilePrefab);
            Tile t = instance.GetComponent<Tile>();
            t.Load(tilePositions[i]);
            tiles.Add(t.pos, t);
        }  
    }
    #endregion
}
