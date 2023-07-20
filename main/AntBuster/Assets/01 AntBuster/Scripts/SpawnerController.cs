using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private Rigidbody spawnerRigid;

    void Start()
    {
        spawnerRigid = GetComponent<Rigidbody>();
    }

}
