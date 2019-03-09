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
     * I tried messing with ways to get the tile to Match whenever these values were
     * changed, but this creates issue in Unity editor.  Not too worried, not too many 
     * clients will change these.
    */
    public Point pos;
    public int height;

    public const float stepHeight = 0.25f;
    // A way for a client to center itself on the tile
    public Vector3 center
    {
        get
        {
            return new Vector3(pos.x, height * stepHeight, pos.y);
        }
    }
    #endregion

    #region Public
    public void Load(Point pos, int height)
    {
        this.pos = pos;
        this.height = height;
        Match();
    }

    public void Load(Vector3 v)
    {
        Load(new Point((int)v.x, (int)v.z), (int)v.y);
    }

    // We call this method after changing coordinates to reflect changes in scale/position
    public void Match()
    {
        transform.position = new Vector3(pos.x, (height * stepHeight) / 2f, pos.y);
        transform.localScale = new Vector3(1, height * stepHeight, 1);
    }

    // Some methods for the board editor to use to create a board organically
    public void Grow()
    {
        height++;
        Match();
    }

    public void Shrink()
    {
        height--;
        Match();
    }
    #endregion
}
