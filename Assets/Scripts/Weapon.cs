using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Weapon
{
    public string Name;
    public int Damage;

    public Weapon(string name, int damage)
    {
        this.Name = name;
        this.Damage = damage;
    }

    public void PrintWeaponStats()
    {
        Debug.LogFormat($"Weapon: {this.Name} - {this.Damage} DMG");
    }
}

[Serializable]
public class WeaponShop
{
    public List<Weapon> inventory;
}