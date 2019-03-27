using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Party = System.Collections.Generic.List<UnityEngine.GameObject>;

public static class ExperienceController
{
    static int[] experienceChart = new int[] { 200, 400, 600, 800, 1200, 1600, 2400 };

    public static int ExperienceForCR(int cr)
    {
        cr = Mathf.Clamp(cr, 0, experienceChart.Length - 1);
        return experienceChart[cr];
    }

    public static void AwardExperience(int exp, Party party)
    {
        for (int i = 0; i < party.Count; i++)
        {
            Level lvl = party[i].GetComponent<Level>();
            if (lvl != null)
            {
                lvl.experience += exp;
            }
        }
    }

    public static void AwardExperienceForCR(int cr, Party party)
    {
        AwardExperience(ExperienceForCR(cr), party);
    }
}
