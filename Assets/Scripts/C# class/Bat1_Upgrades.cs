using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Bat1_Upgrades
{
    public int PeopleNeed;
    public string Name;
    public Sprite Sprite;
    public Sprite SpriteUpdate;
    [HideInInspector] public int Price = 20;
    [TextArea]
    public string Description;
    [HideInInspector] public int ClicNeed = 10;
}
