using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumGameHandler : MonoBehaviour
{
    private GameObject[] drums;

    void Start()
    {
        drums = GameObject.FindGameObjectsWithTag("drum");
    }


    public GameObject[] getDrums()
    {
        return this.drums;
    }

}
