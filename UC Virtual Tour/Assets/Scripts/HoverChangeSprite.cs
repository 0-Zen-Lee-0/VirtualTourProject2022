using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverChangeSprite : MonoBehaviour
{
    float activeHoverTime;
    // implemented to avoid calling SwitchHover every frame
    bool isHoverActive;
    [SerializeField] float hoverTime;
    [SerializeField] HoverMove hoverMove;

    [SerializeField] SpriteRenderer dot;

    [SerializeField] Sprite originalSprite;
    [SerializeField] Sprite leftSprite;
    [SerializeField] Sprite rightSprite;
    [SerializeField] Sprite upSprite;
    [SerializeField] Sprite downSprite;

    void Update()
    {
        activeHoverTime -= Time.deltaTime;

        if (activeHoverTime > 0 && !isHoverActive)
        {
            SwitchHover();
        }
        else if (activeHoverTime <= 0)
        {
            ReturnHover();
        }
    }

    // switch hover sprite
    void SwitchHover()
    {
        isHoverActive = true;

        switch(hoverMove)
        {
            case HoverMove.None:
                break;
            case HoverMove.Left:
                dot.sprite = leftSprite;
                break;
            case HoverMove.Right:
                dot.sprite = rightSprite;
                break;
            case HoverMove.Up:
                dot.sprite = upSprite;
                break;
            case HoverMove.Down:
                dot.sprite = downSprite;
                break;
        }
    }

    // return to original sprite
    void ReturnHover()
    {
        isHoverActive = false;
    }

    void OnMouseOver()
    {
        activeHoverTime = hoverTime;
    }
}
