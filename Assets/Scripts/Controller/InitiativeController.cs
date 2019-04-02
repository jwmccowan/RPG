using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeController : MonoBehaviour
{
    #region notifications
    const string InitiativeRollNotification = "InitiativeController.InitiativeRollNotification";
    const string RoundBeganNotification = "InitiativeController.RoundBeganNotification";
    const string TurnBeganNotification = "InitiativeController.TurnBeganNotification";
    const string TurnEndedNotification = "InitiativeController.TurnEndedNotification";
    const string RoundEndedNotification = "InitiativeController.RoundEndedNotification";
    #endregion

    #region fields
    Dictionary<Unit, int> initiativeTracker = new Dictionary<Unit, int>();
    Roll d20;
    #endregion

    #region public
    public IEnumerator Round()
    {
        BattleController bc = GetComponent<BattleController>();

        List<Unit> units = new List<Unit>(bc.units);
        initiativeTracker.Clear();
        d20 = new Roll(20);

        for (int i = 0; i < bc.units.Count; i++)
        {
            RollInitiative(units[i]);
        }

        while (true)
        {
            this.PostNotification(RoundBeganNotification);

            units.Sort((a, b) => initiativeTracker[a].CompareTo(initiativeTracker[b]));

            for (int i = units.Count - 1; i >= 0; i--)
            {
                if (CanTakeTurn(units[i]))
                {
                    bc.turn.Change(units[i]);
                    yield return units[i];
                }
            }

            this.PostNotification(RoundEndedNotification);
        }
    }
    #endregion

    #region private
    bool CanTakeTurn(Unit unit)
    {
        BaseException exc = new BaseException(true);
        this.PostNotification(TurnBeganNotification, exc);
        return exc.toggle;
    }

    void RollInitiative(Unit unit)
    {
        Stats s = unit.GetComponent<Stats>();
        d20.NewRoll();
        this.PostNotification(InitiativeRollNotification, unit);
        Debug.Log(string.Format("Rolled a {0} for initiative, plus {1}.", d20.value, s[StatTypes.Initiative]));
        initiativeTracker[unit] = d20.value + s[StatTypes.Initiative];
    }
    #endregion
}
