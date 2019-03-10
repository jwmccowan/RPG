using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : StateMachine
{
    public CameraRig cameraRig;
    public Board board;
    public LevelData levelData;
    public Transform tileSelectionIndicator;
    public Point pos;
    public GameObject heroPrefab;
    public Unit currentUnit;
    public Tile currentTile { get { return board.GetTile(pos); } }

    private void Start()
    {
        ChangeState<InitBattleState>();
    }
}
