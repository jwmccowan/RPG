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
        {
            hpLabel.text = string.Format("{0} / {1}", stats[StatTypes.HP], stats[StatTypes.Max_HP]);
            strLabel.text = string.Format("To Hit: {0}", stats[StatTypes.Strength]);
            acLabel.text = string.Format("AC: {0}", stats[StatTypes.AC]);
        }
    }
}
