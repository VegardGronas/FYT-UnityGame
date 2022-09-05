using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectScript
{
    public float Health { get; set; }
    public string Name { get;}
    public int Index { get;}

    public PlayerObjectScript(string name, float health, int index)
    {
        Name = name;
        Health = health;
        Index = index;
    }
}
