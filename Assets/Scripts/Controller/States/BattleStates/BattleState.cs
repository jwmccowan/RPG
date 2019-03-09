using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : State
{
    protected BattleController owner;
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

    /// <summary>
    /// Virtual function that can be overridden to add battle state functionality during a fire event
    /// </summary>
    /// <param name="sender">Input Controller</param>
    /// <param name="e">Point: ([-1 to 1], [-1 to 1])</param>
    protected virtual void OnMove(object sender, object e)
    {

    }

    /// <summary>
    /// Virtual function that can be overridden to add battle state functionality during a fire event
    /// </summary>
    /// <param name="sender">Input Controller</param>
    /// <param name="e">int: 0 is left click, 1 is right click, 2 is middle click</param>
    protected virtual void OnFire(object sender, object e)
    {

    }

    /// <summary>
    /// An example of a method that will be used by multiple battle states
    /// Other shared functionality should be added here
    /// </summary>
    /// <param name="p">Point to select</param>
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
