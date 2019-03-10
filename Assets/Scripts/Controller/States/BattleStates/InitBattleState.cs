using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBattleState : BattleState
{
    public override void Enter()
    {
        Debug.Log("InitBattleState Enter");
        base.Enter();
        StartCoroutine(Init());
    }

    /// <summary>
    /// Inits the battle
    /// </summary>
    /// <returns></returns>
    IEnumerator Init()
    {
        board.Load(levelData);
        Point p = new Point((int)levelData.tilePositions[0].x, (int)levelData.tilePositions[0].z);
        SelectTile(p);
        SpawnTestUnits();
        yield return null;
        owner.ChangeState<SelectUnitState>();
    }

    void SpawnTestUnits()
    {
        System.Type[] components = new System.Type[] { typeof(WalkMovement), typeof(FlyMovement), typeof(TeleportMovement) };

        for (int i = 0; i < components.Length; i++)
        {
            GameObject instance = Instantiate(owner.heroPrefab);
            Point p = new Point((int)levelData.tilePositions[i * 5].x, (int)levelData.tilePositions[i * 5].y);

            Unit unit = instance.GetComponent<Unit>();
            unit.Place(board.GetTile(p));
            unit.Match();

            Movement m = instance.AddComponent(components[i]) as Movement;
            m.range = 5;
            m.jumpHeight = 2;
        }
    }
}
