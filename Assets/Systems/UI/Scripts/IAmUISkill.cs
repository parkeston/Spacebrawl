using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAmUISkill
{
    void SetIcon(Sprite icon, Color color);
    void FillDescription(string name, string description, string timings);
}
