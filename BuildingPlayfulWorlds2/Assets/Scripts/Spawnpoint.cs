using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour {

    public bool isAvailable;

    private void Awake()
    {
        isAvailable = true;
    }

    private void Start()
    {
        isAvailable = true;
    }
}
