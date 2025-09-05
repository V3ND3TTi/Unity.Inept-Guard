using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin : Character
{
    public Weapon PrimaryWeapon;
   public Paladin(string name, Weapon weapon) : base(name)
    {
        this.PrimaryWeapon = weapon;
    }

    public override void PrintStatsInfo()
    {
        Debug.LogFormat($"Hail, {this.Name} - take up your {this.PrimaryWeapon.Name}!");
    }
}
