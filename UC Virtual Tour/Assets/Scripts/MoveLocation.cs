using UnityEngine;

// Class for transitioning to other location sphere when a move button is pressed
public class MoveLocation : MonoBehaviour
{
    [SerializeField] int locationIndex = 0;

    // When a move button is pressed, the lookRotation assigned to this will be used by the new location sphere; will be ignored if empty (Vector3.zero)
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
