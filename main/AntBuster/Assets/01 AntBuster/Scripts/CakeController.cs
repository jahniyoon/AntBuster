using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeController : MonoBehaviour
{
    private Rigidbody cakeRigid;

    void Start()
    {
        cakeRigid = GetComponent<Rigidbody>();
    }

}
