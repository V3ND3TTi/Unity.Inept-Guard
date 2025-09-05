using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string Name;
    public int Exp = 0;

    public Character()
    {
        this.Name = "Not Assigned";
    }

    public Character(string name)
    {
        this.Name = name;
    }

    public virtual void PrintStatsInfo()
    {
        Debug.LogFormat($"Hero: {this.Name} - {this.Exp} XP");
    }

    private void ResetStats()
    {
        this.Name = "Not Assigned";
        this.Exp = 0;
    }
}