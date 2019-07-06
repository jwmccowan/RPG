using UnityEngine;
using System.Collections.Generic;

public class CharacterSheet : MonoBehaviour
{
    #region fields
    public StatCollection stats;
    public Level level;
    #endregion

    #region MonoBehaviour
    void OnEnable()
    {
    }

    void Awake()
    {
        stats = gameObject.AddComponent<DefaultStats>();
        level = gameObject.AddComponent<Level>();
    }
    #endregion

    #region events
    #endregion

    #region public
    #endregion

    #region private
    #endregion
}
