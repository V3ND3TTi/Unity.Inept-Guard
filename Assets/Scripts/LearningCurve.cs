using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LearningCurve : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Paladin knight = new("Sir Arthur", new Weapon("Hunting Bow", 105));
        // knight.PrintStatsInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
