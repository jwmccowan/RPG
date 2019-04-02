using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This component stores the coordinates and dimensions of a tile in our game board,
/// as well as a few helper functions for dealing with them, including way of raising
/// or lowering its height
/// </summary>
public class Tile : MonoBehaviour
{
    /* 
     * This component stores the coordinates and dimensions of a tile in our game board,
     * as well as a few helper functions for dealing with them, including way of raising
     * or lowering its height
    */

    #region Fields
    /// <summary>
    /// The x, y coordinates of the tile
    /// </summary>
    public Point pos;

    /// <summary>
    /// The number of steps up the tile is from base height (0)
    ///     Actual height of the tile is height * stepHeight
    /// </summary>
    public int height
    {
        get { return _height; }
        set { _height = value; Match(); }
    }
    int _height;

    /// <summary>
    /// The gameObject currently standing on the tile
    ///     As of now, this is a Unit, but this might hold traps, items, w/e in the future
    /// </summary>
    public GameObject content;

    /// <summary>
    /// Height of each 'step'
    /// </summary>
    public const float stepHeight = 0.25f;

    /// <summary>
    /// The center of the top of a tile
    /// </summary>
    public Vector3 center
    {
        get
        {
            return new Vector3(pos.x, _height * stepHeight, pos.y);
        }
    }

    /// <summary>
    /// Only used in pathfinding
    /// </summary>
    [HideInInspector] public Tile prev;

    /// <summary>
    /// Only used in pathfinding
    /// </summary>
    [HideInInspector] public int distance;
    #endregion

    #region Public
    /// <summary>
    /// Set the position and height of the Tile and transform
    /// </summary>
    /// <param name="pos">(x, y) coordinates of the tile</param>
    /// <param name="height">height in stepHeight units of the tile</param>
    public void Load(Point pos, int height)
    {
        this.pos = pos;
        this._height = height;
        Match();
    }

    /// <summary>
    /// Set the position and height of the Tile and transform
    /// </summary>
    /// <param name="v">Vector3 coordinates of the tile, with height = v.y</param>
    public void Load(Vector3 v)
    {
        Load(new Point((int)v.x, (int)v.z), (int)v.y);
    }

    /// <summary>
    /// Called after changing the x, y, or height of a Tile, only use in emergency
    /// </summary>
    public void Match()
    {
        transform.position = new Vector3(pos.x, (_height * stepHeight) / 2f, pos.y);
        transform.localScale = new Vector3(1, _height * stepHeight, 1);
    }

    /// <summary>
    /// Raises height by one
    /// </summary>
    public void Raise()
    {
        _height++;
        Match();
    }

    /// <summary>
    /// Lowers height by one
    /// </summary>
    public void Lower()
    {
        _height--;
        Match();
    }

    public void SetColor(Color color)
    {
        Renderer[] rendererList = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < rendererList.Length; i++)
        {
            rendererList[i].material.SetColor("_Color", color);
        }
    }
    #endregion
}
