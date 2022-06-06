using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class for rotating buttons to face at the origin
public class RotateTowardsOrigin : MonoBehaviour
{
    [SerializeField] bool rotateOrigin;

    void Start()
    {
        if (rotateOrigin)
        {
            transform.LookAt(Vector3.zero);       
        }
    }
}