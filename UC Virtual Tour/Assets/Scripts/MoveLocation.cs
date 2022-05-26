using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLocation : MonoBehaviour
{
    [SerializeField] int locationIndex = 0;

    public int GetLocationIndex()
    {
        return locationIndex;
    }
}
