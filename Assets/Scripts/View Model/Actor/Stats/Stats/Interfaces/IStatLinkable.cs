using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatLinkable
{
    float statLinkerValue { get; }

    void AddLinker(StatLinker statLinker);
    void ClearLinkers();
    void UpdateLinkers();
}
