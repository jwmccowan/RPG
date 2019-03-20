using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : StateMachine
{
    public AbilityMenuPanelController abilityMenuPanelController;
    public CameraRig cameraRig;
    public Board board;
    public LevelData levelData;
    public Transform tileSelectionIndicator;
    public Point pos;
    public GameObject heroPrefab;
    public Tile currentTile { get { return board.GetTile(pos); } }
    public Turn turn = new Turn();
    public List<Unit> units = new List<Unit>();

    private void Start()
    {
        ChangeState<InitBattleState>();
    }
}
