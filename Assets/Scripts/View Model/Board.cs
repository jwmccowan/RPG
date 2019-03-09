using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();

    public void Load(LevelData data)
    {
        List<Vector3> tilePositions = data.tilePositions;
        for (int i = 0; i < tilePositions.Count; i++)
        {
            GameObject instance = Instantiate(tilePrefab);
            Tile t = instance.GetComponent<Tile>();
            t.Load(tilePositions[i]);
            tiles.Add(t.pos, t);
        }  
    }
}
