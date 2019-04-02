using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : State
{
    protected BattleController owner;
    public AbilityMenuPanelController abilityMenuPanelController { get { return owner.abilityMenuPanelController; } }
    public StatPanelController statPanelController { get { return owner.statPanelController; } }
    public CameraRig cameraRig { get { return owner.cameraRig; } }
    public Board board { get { return owner.board; } }
    public LevelData levelData { get { return owner.levelData; } }
    public Transform tileSelectionIndicator { get { return owner.tileSelectionIndicator; } }
    public Turn turn { get { return owner.turn; } }
    public List<Unit> units { get { return owner.units; } }
    public Point pos { get { return owner.pos; } set { owner.pos = value; } }

    protected virtual void Awake()
    {
        owner = GetComponent<BattleController>();
    }

    protected override void AddListeners()
    {
        this.AddListener(OnMove, InputController.moveEventNotification);
        this.AddListener(OnFire, InputController.fireEventNotification);
    }

    protected override void RemoveListeners()
    {
        this.RemoveListener(OnMove, InputController.moveEventNotification);
        this.RemoveListener(OnFire, InputController.fireEventNotification);
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

    protected virtual Unit GetUnit(Point p)
    {
        Tile t = board.GetTile(p);
        if (t.content == null) return null;
        Unit unit = t.content.GetComponent<Unit>();
        if (unit == null) return null;
        return unit;
    }

    protected virtual void RefreshPrimaryStatPanel(Point p)
    {
        Unit unit = GetUnit(p);
        if (unit == null)
        {
            statPanelController.HidePrimary();
        }
        else
        {
            statPanelController.ShowPrimary(unit.gameObject);
        }
    }

    protected virtual void RefreshSecondaryStatPanel(Point p)
    {
        Unit unit = GetUnit(p);
        if (unit == null)
        {
            Debug.Log("hide");
            statPanelController.HideSecondary();
        }
        else
        {
            Debug.Log("show");
            statPanelController.ShowSecondary(unit.gameObject);
        }
    }
}
