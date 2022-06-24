using UnityEngine;
using UnityEngine.EventSystems;

// Class for hiding the Hider component in UI
// Had to create a new class because of IPointerDownHandler
// Initially, HideUI is only called when a mouse button press is released; this implementation allows the invoking of HideUI when the mouse button is pressed
public class HideHider : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        UIControl.Instance.HideUI();
    }
}
