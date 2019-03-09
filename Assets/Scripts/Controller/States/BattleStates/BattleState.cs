using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : State
{
    BattleController owner;
    public CameraRig cameraRig { get { return owner.cameraRig; } }
    public Board board { get { return owner.board; } }
    public LevelData levelData { get { return owner.levelData; } }
    public Transform tileSelectionIndicator { get { return owner.tileSelectionIndicator; } }
    public Point pos { get { return owner.pos; } set { owner.pos = value; } }

    protected virtual void Awake()
    {
        owner = GetComponent<BattleController>();
    }

    protected override void AddListeners()
    {
        this.AddListener(OnMove, InputController.moveEventNotification);
        this.AddListener(OnFire, InputController.moveEventNotification);
    }

    protected override void RemoveListeners()
    {
        this.RemoveListener(OnMove, InputController.moveEventNotification);
        this.RemoveListener(OnFire, InputController.moveEventNotification);
    }

    protected virtual void OnMove(object sender, object e)
    {

    }

    protected virtual void OnFire(object sender, object e)
    {

    }

    protected virtual void SelectTile(Point p)
    {
        if (pos == p || !board.tiles.ContainsKey(p))
        {
            return;
        }

        pos = p;
        tileSelectionIndicator.localPosition = board.tiles[p].center;
    }
}
