﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    CharacterSheet sheet;
    public int range { get { return Mathf.FloorToInt(sheet.stats[StatTypes.Stat_Movement]); } }
    public int jumpHeight { get { return Mathf.FloorToInt(sheet.stats[StatTypes.Stat_Movement] / 4); } }
    protected Unit unit;
    protected Transform jumper;

    protected virtual void Awake()
    {
        jumper = transform.Find("Jumper");
        unit = GetComponent<Unit>();
    }

    protected virtual void Start()
    {
        sheet = GetComponent<CharacterSheet>();
    }

    public List<Tile> GetTilesInRange(Board board)
    {
        List<Tile> list = board.Search(unit.tile, ExpandSearch);
        Filter(list);
        return list;
    }

    protected virtual bool ExpandSearch(Tile from, Tile to)
    {
        return to.distance <= range;
    }

    protected virtual void Filter(List<Tile> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].content != null)
            {
                list.RemoveAt(i);
            }
        }
    }

    public abstract IEnumerator Traverse(Tile destination);

    protected virtual IEnumerator Turn(Directions dir)
    {
        
        TransformLocalEulerTweener t = (TransformLocalEulerTweener)transform.RotateToLocal(dir.ToEuler(), 0.25f, EasingEquations.EaseInOutQuad);

        if (Mathf.Approximately(t.startTweenValue.y, 0f) && Mathf.Approximately(t.endTweenValue.y, 270f))
        {
            t.startTweenValue = new Vector3(t.startTweenValue.x, 360f, t.startTweenValue.z);
        }
        else if (Mathf.Approximately(t.startTweenValue.y, 270f) && Mathf.Approximately(t.endTweenValue.y, 0f))
        {
            t.endTweenValue = new Vector3(t.startTweenValue.x, 360f, t.startTweenValue.z);
        }
        unit.dir = dir;

        while (t != null)
        {
            yield return null;
        }
    }
}
