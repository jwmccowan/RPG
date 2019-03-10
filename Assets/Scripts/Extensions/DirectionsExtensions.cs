using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionsExtensions
{
    /// <summary>
    /// Returns closest Directions between two tiles
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static Directions GetDirections(this Tile from, Tile to)
    {
        Point p = to.pos - from.pos;
        if (Mathf.Abs(p.x) > Mathf.Abs(p.y))
        {
            if (from.pos.x < to.pos.x)
            {
                return Directions.East;
            }
            return Directions.West;
        }
        else
        {
            if (from.pos.y < to.pos.y)
            {
                return Directions.North;
            }
            return Directions.South;
        }
    }

    /// <summary>
    /// A faster but less accurate version of GetDirections for when the to tile is adjacent to the from tile
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static Directions GetDirectionsFast(this Tile from, Tile to)
    {
        if (from.pos.y < to.pos.y)
        {
            return Directions.North;
        }
        if (from.pos.y > to.pos.y)
        {
            return Directions.South;
        }
        if (from.pos.x < to.pos.x)
        {
            return Directions.East;
        }
        return Directions.West;
    }

    /// <summary>
    /// Turns a direction into a Vector3
    /// </summary>
    /// <param name="d">Direction to turn</param>
    /// <returns>Vector3 with a y rotation equal to the degree of the direction</returns>
    public static Vector3 ToEuler(this Directions d)
    {
        return new Vector3(0, (int)d * 90, 0);
    }
}
