﻿using System.Collections;
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
        DataController.instance.Load();
        Point p = new Point((int)levelData.tilePositions[0].x, (int)levelData.tilePositions[0].z);
        SelectTile(p);
        SpawnTestUnits();
        yield return null;
        owner.ChangeState<CutSceneState>();
    }

    void SpawnTestUnits()
    {
        //This is temporary code for testing
        System.Type[] components = new System.Type[] { typeof(WalkMovement), typeof(FlyMovement), typeof(TeleportMovement) };

        for (int i = 0; i < components.Length; i++)
        {
            GameObject instance = Instantiate(owner.heroPrefab);
            CharacterSheet sheet = instance.AddComponent<CharacterSheet>();
            sheet.level.experience += Level.ExperienceForLevel(2);
            sheet.level.AddClassLevel(ClassType.Fighter);
            sheet.level.AddClassLevel(ClassType.Fighter);
            sheet.stats[StatTypes.Dexterity] = 15;
            sheet.stats[StatTypes.Wisdom] = 12;
            sheet.stats[StatTypes.Constitution] = 12;

            Point p = new Point((int)levelData.tilePositions[i * 5 + 8].x, (int)levelData.tilePositions[i * 5 + 8].y);

            Unit unit = instance.GetComponent<Unit>();
            unit.Place(board.GetTile(p));
            unit.Match();

            Movement m = instance.AddComponent(components[i]) as Movement;
            units.Add(unit);
        }
    }
}
