using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLocation : MonoBehaviour
{
    [SerializeField] int locationIndex = 0;

    [SerializeField] Vector3 lookRotation;

    public int GetLocationIndex()
    {
        return locationIndex;
    }

    public Vector3 GetLookRotation()
    {
        return lookRotation;
    }
}
