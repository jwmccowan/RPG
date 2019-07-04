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
    public Text strLabel;
    public Text acLabel;

    public void Display(GameObject obj)
    {
        background.sprite = Random.value > .5f ? enemyBackground : allyBackground;
        nameLabel.text = obj.name;
        Stats stats = obj.GetComponent<Stats>();
        if (stats)
        {/*TOREMOVE:
            hpLabel.text = string.Format("{0} / {1}", stats[StatTypes.HP], stats[StatTypes.Max_HP]);
            strLabel.text = string.Format("Acc.: {0}", stats[StatTypes.Accuracy]);
            acLabel.text = string.Format("Def.: {0}", stats[StatTypes.Deflection]);
            */
        }
    }
}
