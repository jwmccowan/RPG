using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    /* 
     * This component stores the coordinates and dimensions of a tile in our game board,
     * as well as a few helper functions for dealing with them, including way of raising
     * or lowering its height
    */

    #region Fields
    /* Pos holds the x, y coordinates of the tile
     * Height is how many steps up the tile is from the base height
     * But the actual height is Height * StepHeight
    */
    public Point Pos;
    public int Height;

    public const float StepHeight = 0.25f;
    // A way for a client to center itself on the tile
    public Vector3 Center
    {
        get
        {
            return new Vector3(Pos.x, Height * StepHeight, Pos.y);
        }
    }
    #endregion

    #region Public
    public void Load(Point pos, int height)
    {
        Pos = pos;
        Height = height;
        Match();
    }

    public void Load(Vector3 v)
    {
        Load(new Point((int)v.x, (int)v.z), (int)v.y);
    }

    // We call this method after changing coordinates to reflect changes in scale/position
    public void Match()
    {
        transform.position = new Vector3(Pos.x, (Height * StepHeight) / 2f, Pos.y);
        transform.localScale = new Vector3(1, Height * StepHeight, 1);
    }

    // Some methods for the board editor to use to create a board organically
    public void Grow()
    {
        Height++;
        Match();
    }

    public void Shrink()
    {
        Height--;
        Match();
    }
    #endregion
}
