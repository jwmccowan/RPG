using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public int range;
    public int jumpHeight;
    protected Unit unit;
    protected Transform jumper;

    protected virtual void Awake()
    {
        jumper = transform.Find("jumper");
        unit = GetComponent<Unit>();
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
        for (int i = list.Count - 1; i >= 0; i++)
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

        if (Mathf.Approximately(t.startValue.y, 0f) && Mathf.Approximately(t.endValue.y, 270f))
        {
            t.startValue = new Vector3(t.startValue.x, 360f, t.startValue.z);
        }
        else if (Mathf.Approximately(t.startValue.y, 270f) && Mathf.Approximately(t.endValue.y, 0f))
        {
            t.endValue = new Vector3(t.startValue.x, 360f, t.startValue.z);
        }
        unit.dir = dir;

        while (t != null)
        {
            yield return null;
        }
    }
}
