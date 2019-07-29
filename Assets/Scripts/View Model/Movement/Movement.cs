using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    Stats stats;
    public int range { get { return stats[StatTypes.Movement]; } }
    public int jumpHeight { get { return Mathf.FloorToInt(stats[StatTypes.Movement] / 4); } }
    protected Unit unit;
    protected Transform jumper;

    protected virtual void Awake()
    {
        jumper = transform.Find("Jumper");
        unit = GetComponent<Unit>();
    }

    protected virtual void Start()
    {
        stats = GetComponent<Stats>();
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
