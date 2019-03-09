using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class BoardEditor : MonoBehaviour
{
    #region Properties
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject tileSelectorPrefab;
    [SerializeField] LevelData savedLevelData;

    // Max width, length, and height of our battlefield
    [SerializeField] int width = 10;
    [SerializeField] int length = 10;
    [SerializeField] int height = 8;
    [SerializeField] Point pos;

    /*
     * Lazy Load our Tile Selection Indicator so it is there when we need it
    */
    public Transform selector
    {
        get
        {
            if (_selector == null)
            {
                GameObject instance = Instantiate(tileSelectorPrefab);
                _selector = instance.transform;
            }
            return _selector;
        }
    }
    Transform _selector;

    Dictionary<Point, Tile> tileData = new Dictionary<Point, Tile>();
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
        RaiseSingle(pos);
    }

    public void Lower()
    {
        LowerSingle(pos);
    }

    /* 
     * Simply updates the Tile Selection Indicator
     */
    public void UpdateMarker()
    {
        if (tileData.ContainsKey(pos))
        {
            selector.localPosition = tileData[pos].center;
        }
        else
        {
            selector.localPosition = new Vector3(pos.x, 0.0f, pos.y);
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
        tileData.Clear();
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
        board.tilePositions = new List<Vector3>(tileData.Count);
        foreach (Tile t in tileData.Values)
            board.tilePositions.Add(new Vector3(t.pos.x, t.height, t.pos.y));

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
        if (savedLevelData == null)
        {
            return;
        }

        foreach (Vector3 v in savedLevelData.tilePositions)
        {
            Tile t = Create();
            t.Load(v);
            tileData.Add(t.pos, t);
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
        if (t.height < height)
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
        if (!tileData.ContainsKey(p))
        {
            return;
        }

        Tile t = tileData[p];
        t.Shrink();

        if (t.height <= 0)
        {
            tileData.Remove(p);
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
        int x = Random.Range(0, width);
        int y = Random.Range(0, length);
        int w = Random.Range(1, width - x + 1);
        int l = Random.Range(1, length - y + 1);
        return new Rect(x, y, w, l);
    }

    /*
     * Returns the Tile at Point p and creates it
     * first if it doesnt exist
     * 
     */
    Tile GetOrCreate(Point p)
    {
        if (tileData.ContainsKey(p))
        {
            return tileData[p];
        }

        Tile t = Create();
        t.Load(p, 0);
        tileData[p] = t;

        return t;
    }

    /*
     * Simple function that creates a tile and parents it
     * to BoardCreator within the scene
     */
    Tile Create()
    {
        GameObject instance = Instantiate(tilePrefab);
        instance.transform.parent = transform;
        return instance.GetComponent<Tile>();
    }
    #endregion
}
