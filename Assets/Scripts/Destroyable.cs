using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable<T> : MonoBehaviour where T : MonoBehaviour
{
    public float OnscreenDelay = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, OnscreenDelay);
    }
}
