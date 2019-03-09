using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleController : StateMachine
{
    public CameraRig cameraRig;
    public Board board;
    public LevelData levelData;
    public Transform tileSelectionIndicator;
    public Point pos;

    private void Start()
    {
        //Asuming we'll have something like this
        //Will remove comment when implemented
        //ChangeState<InitBattleState>();
    }
}
