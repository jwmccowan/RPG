using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class BoardEditor : MonoBehaviour
{
    #region Properties
    [SerializeField] GameObject TilePrefab;
    [SerializeField] GameObject TileSelectorPrefab;
    [SerializeField] LevelData SavedLevelData;

    // Max width, length, and height of our battlefield
    [SerializeField] int Width = 10;
    [SerializeField] int Length = 10;
    [SerializeField] int Height = 8;
    [SerializeField] Point Pos;

    /*
     * Lazy Load our Tile Selection Indicator so it is there when we need it
    */
    public Transform Selector
    {
        get
        {
            if (_selector == null)
            {
                GameObject instance = Instantiate(TileSelectorPrefab);
                _selector = instance.transform;
            }
            return _selector;
        }
    }
    Transform _selector;

    Dictionary<Point, Tile> TileData = new Dictionary<Point, Tile>();
    #endregion

    #region Public
    /*
     * Public funtions to raise or lower a random rectagle
     * within the boards range by one height unit
     */
    public void RaiseArea()
    {
        Rect r = GetRandomRect();
        RaiseRect(r);
    }

    public void LowerArea()
    {
        Rect r = GetRandomRect();
        RaiseRect(r);
    }

    /*
     * Public functions to raise or lower single 
     * tile by one height unit
     */
    public void Raise()
    {
        RaiseSingle(Pos);
    }

    public void Lower()
    {
        LowerSingle(Pos);
    }

    /* 
     * Simply updates the Tile Selection Indicator
     */
    public void UpdateMarker()
    {
        if (TileData.ContainsKey(Pos))
        {
            Selector.localPosition = TileData[Pos].Center;
        }
        else
        {
            Selector.localPosition = new Vector3(Pos.x, 0.0f, Pos.y);
        }
    }

    /*
     * Clears the battle board.  Easy.
     */
    public void Clear()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        TileData.Clear();
    }

    /*
     * The following fuctions save and load a serialized list of Vector3 
     * representing the board to Assets/Resources/Levels/BoardEditor.asset
     */
    public void Save()
    {
        string filePath = Application.dataPath + "/Resources/Levels";
        if (!Directory.Exists(filePath))
            CreateSaveDirectory();

        LevelData board = ScriptableObject.CreateInstance<LevelData>();
        board.TilePositions = new List<Vector3>(TileData.Count);
        foreach (Tile t in TileData.Values)
            board.TilePositions.Add(new Vector3(t.Pos.x, t.Height, t.Pos.y));

        string fileName = string.Format("Assets/Resources/Levels/{1}.asset", filePath, name);
        AssetDatabase.CreateAsset(board, fileName);
    }

    void CreateSaveDirectory()
    {
        // We look universally but create locally for permissions reasons
        string filePath = Application.dataPath + "/Resources";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets", "Resources");
        filePath += "/Levels";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets/Resources", "Levels");
        AssetDatabase.Refresh();
    }

    public void Load()
    {
        Clear();
        if (SavedLevelData == null)
        {
            return;
        }

        foreach (Vector3 v in SavedLevelData.TilePositions)
        {
            Tile t = Create();
            t.Load(v);
            TileData.Add(t.Pos, t);
        }
    }
    #endregion

    #region Private
    /*
     * Raises a single tile at a given Point, instead of at Pos
     */
    void RaiseSingle(Point p)
    {
        Tile t = GetOrCreate(p);
        if (t.Height < Height)
        {
            t.Grow();
        }
    }

    /*
     * Lowers a single tile at a given Point, instead of at Pos
     * We destroy the Tiles game object if we get to height 0
     */
    void LowerSingle(Point p)
    {
        if (!TileData.ContainsKey(p))
        {
            return;
        }

        Tile t = TileData[p];
        t.Shrink();

        if (t.Height <= 0)
        {
            TileData.Remove(p);
            Destroy(t.gameObject);
        }
    }

    /*
     * The following two functions simply loop through a given 
     * rects coordinates and raise or lower all tiles within
     */
    void RaiseRect(Rect r)
    {
        for (int x = (int)r.xMin; x < (int)r.xMax; x++)
        {
            for (int y = (int)r.yMin; y < (int)r.yMax; y++)
            {
                RaiseSingle(new Point(x, y));
            }
        }
    }

    void LowerRect(Rect r)
    {
        for (int x = (int)r.xMin; x < (int)r.xMax; x++)
        {
            for (int y = (int)r.yMin; y < (int)r.yMax; y++)
            {
                LowerSingle(new Point(x, y));
            }
        }
    }

    /*
     * Returns a random Rect which fit within the range of
     * the game board
     */
    Rect GetRandomRect()
    {
        int x = Random.Range(0, Width);
        int y = Random.Range(0, Length);
        int w = Random.Range(1, Width - x + 1);
        int l = Random.Range(1, Length - y + 1);
        return new Rect(x, y, w, l);
    }

    /*
     * Returns the Tile at Point p and creates it
     * first if it doesnt exist
     * 
     */
    Tile GetOrCreate(Point p)
    {
        if (TileData.ContainsKey(p))
        {
            return TileData[p];
        }

        Tile t = Create();
        t.Load(p, 0);
        TileData[p] = t;

        return t;
    }

    /*
     * Simple function that creates a tile and parents it
     * to BoardCreator within the scene
     */
    Tile Create()
    {
        GameObject instance = Instantiate(TilePrefab);
        instance.transform.parent = transform;
        return instance.GetComponent<Tile>();
    }
    #endregion
}
