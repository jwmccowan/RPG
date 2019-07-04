using UnityEngine;
using System.Collections.Generic;

public class CharacterSheet : MonoBehaviour
{
    static Dictionary<StatTypes, string> _willChangeBonusNotifications = new Dictionary<StatTypes, string>();
    static Dictionary<StatTypes, string> _didChangeBonusNotifications = new Dictionary<StatTypes, string>();
    static Dictionary<StatTypes, string> _didChangeFinalNotifications = new Dictionary<StatTypes, string>();

    public static string WillChangeBonusNotification(StatTypes type)
    {
        if (!_willChangeBonusNotifications.ContainsKey(type))
        {
            _willChangeBonusNotifications.Add(type, string.Format("Stats.{0}WillChange", type.ToString()));
        }
        return _willChangeBonusNotifications[type];
    }

    public static string DidChangeNotification(StatTypes type)
    {
        /*if (!_didChangeBonusNotifications.ContainsKey(type))
        {
            _didChangeNotifications.Add(type, string.Format("Stats.{0}DidChange", type.ToString()));
        }
        return _didChangeNotifications[type];
        */
        return "";
    }

    #region fields
    public Stats stats;
    AbilityScores abilityScores;
    DerivedStats derivedStats;
    BonusHandler bonusHandler;
    public Health health;
    public Level level;
    public Accuracy accuracy;
    #endregion

    #region MonoBehaviour
    void OnEnable()
    {
        Init();
    }

    void Awake()
    {
        stats = gameObject.AddComponent<Stats>();
        derivedStats = gameObject.AddComponent<DerivedStats>();
        bonusHandler = gameObject.AddComponent<BonusHandler>();
        level = gameObject.AddComponent<Level>();
        health = gameObject.AddComponent<Health>();
        accuracy = gameObject.AddComponent<Accuracy>();
    }
    #endregion

    #region events
    #endregion

    #region public
    public void AddBonus(Bonus bonus)
    {
        bonusHandler.AddBonus(bonus);
    }

    public void RemoveBonus(Bonus bonus)
    {
        bonusHandler.RemoveBonus(bonus);
    }
    #endregion

    #region private
    void Init()
    {
        stats[StatTypes.Stat_Deflection] += 10;
        health.hp = health.maxMaxHP;
    }
    #endregion
}
