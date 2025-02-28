using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCup", menuName = "ScriptableObjects/CupBase", order = 1)]
public class CupBase : ScriptableObject
{
    public CupType type;
    public Sprite sprite;
}

public enum CupType
{
    Empty,
    Red,
    Orange,
    Yellow,
    Green,
    Blue,
    Indigo,
    Violet,
}
