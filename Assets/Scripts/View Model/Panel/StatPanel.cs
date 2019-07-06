using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatPanel : MonoBehaviour
{
    public Panel panel;
    public Sprite enemyBackground;
    public Sprite allyBackground;
    public Image background;
    public Image avatar;
    public Text nameLabel;
    public Text hpLabel;
    public Text accLabel;
    public Text defLabel;

    public void Display(GameObject obj)
    {
        background.sprite = Random.value > .5f ? enemyBackground : allyBackground;
        nameLabel.text = obj.name;
        CharacterSheet sheet = obj.GetComponent<CharacterSheet>();
        if (sheet)
        {
            hpLabel.text = string.Format("{0} / {1}", Mathf.FloorToInt(sheet.hp), Mathf.FloorToInt(sheet.stats[StatTypes.Stat_Max_HP]));
            accLabel.text = string.Format("Acc.: {0}", Mathf.FloorToInt(sheet.stats[StatTypes.Stat_Accuracy]));
            defLabel.text = string.Format("Def.: {0}", Mathf.FloorToInt(sheet.stats[StatTypes.Stat_Deflection]));
        }
    }
}
