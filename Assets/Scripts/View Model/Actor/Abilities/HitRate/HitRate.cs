using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitRate : MonoBehaviour
{
    #region notifications
    public const string AutomaticHitNotification = "HitRate.AutomaticHitNotifiacation";
    public const string AutomaticMissNotification = "HitRate.AutomaticMissNotification";
    public const string StatusCheckNotification = "HitRate.StatusCheckNotification";
    #endregion

    #region fields
    protected Unit unit { get { return GetComponentInParent<Unit>(); } }
    #endregion

    #region public
    public abstract int Calculate(Tile tile);
    #endregion

    #region protected
    protected virtual bool AutomaticHit(Tile tile)
    {
        MatchException exc = new MatchException(unit, tile.content.GetComponent<Unit>());
        this.PostNotification(AutomaticHitNotification, exc);
        return exc.toggle;
    }

    protected virtual bool AutomaticMiss(Tile tile)
    {
        MatchException exc = new MatchException(unit, tile.content.GetComponent<Unit>());
        this.PostNotification(AutomaticMissNotification, exc);
        return exc.toggle;
    }

    protected virtual int AdjustForStatusEffects(Tile tile, int toHit)
    {
        Info<Unit, Unit, int> info = new Info<Unit, Unit, int>(unit, tile.content.GetComponent<Unit>(), toHit);
        this.PostNotification(StatusCheckNotification, info);
        return info.arg2;
    }
    #endregion
}
