using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkMovement : Movement
{
    //Animator anim;

    //protected override void Awake()
    //{
    //    base.Awake();
    //    anim = jumper.GetComponent<Animator>();
    //}

    protected override bool ExpandSearch(Tile from, Tile to)
    {
        if (Mathf.Abs(from.height - to.height) > jumpHeight)
        {
            return false;
        }

        if (to.content != null)
        {
            return false;
        }

        return base.ExpandSearch(from, to);
    }
    public override IEnumerator Traverse(Tile t)
    {
        unit.Place(t);
        List<Tile> moveTargets = new List<Tile>();

        while (t != null)
        {
            moveTargets.Insert(0,t);
            t = t.prev;
        }

        //anim.SetBool("isWalking", true);

        for (int i = 1; i < moveTargets.Count; i++)
        {
            Tile from = moveTargets[i - 1];
            Tile to = moveTargets[i];

            Directions dir = from.GetDirection(to);
            if (unit.dir != dir)
            {
                yield return StartCoroutine(Turn(dir));
            }
            if (from.height == to.height)
            {
                yield return StartCoroutine(Walk(to));
            }
            else
            {
                yield return StartCoroutine(Jump(to));
            }
        }
        //anim.SetBool("isWalking", false);
        yield return null;
    }

    IEnumerator Walk(Tile t)
    {
        Tweener tweener = transform.MoveTo(t.center, 0.5f, EasingEquations.Linear);
        while (tweener != null)
        {
            yield return null;
        }
    }

    IEnumerator Jump(Tile t)
    {
        Tweener walkTweener = transform.MoveTo(t.center, 0.5f, EasingEquations.Linear);

        Tweener jumpTweener = jumper.MoveToLocal(new Vector3(0, Tile.stepHeight * 2f, 0), walkTweener.duration / 2f, EasingEquations.EaseOutQuad);
        jumpTweener.loopCount = 1;
        jumpTweener.loopType = EasingControl.LoopType.PingPong;

        //anim.SetBool("isWalking", false);

        while (walkTweener != null)
        {
            yield return null;
        }
        //anim.SetBool("isWalking", true);
    }
}
