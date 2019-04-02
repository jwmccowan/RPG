using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAbilityRange : AbilityRange
{
    public override bool directionOriented { get { return true; } }

    public override List<Tile> GetTilesInRange(Board board)
    {
        Point start = unit.tile.pos;
        Point end;
        List<Tile> retValue = new List<Tile>();

        switch (unit.dir)
        {
            case Directions.North:
                end = new Point(start.x, board.max.y);
                break;
            case Directions.East:
                end = new Point(board.max.x, start.y);
                break;
            case Directions.South:
                end = new Point(start.x, board.min.y);
                break;
            default:
                end = new Point(board.min.x, start.y);
                break;
        }

        while (start != end)
        {
            if (start.x < end.x) start.x++;
            else if (start.x > end.x) start.x--;

            if (start.y < end.y) start.y++;
            else if (start.y > end.y) start.y--;

            Tile t = board.GetTile(start);
            if (t != null && Mathf.Abs(unit.tile.height - t.height) <= vertical)
            {
                retValue.Add(t);
            }
        }

        return retValue;
    }
}
