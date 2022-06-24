using UnityEngine;

// Class used by move button to change sprites when hovered
public class HoverChangeSprite : MonoBehaviour
{
    // Duration of the current hover
    float activeHoverTime;
    // Used to avoid calling SwitchHover every frame
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

    // Function used to switch hover sprite
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

    // Function used to return the move button sprite to original sprite
    void ReturnHover()
    {
        isHoverActive = false;
        dot.sprite = originalSprite;
    }

    // Function for setting the active hover time when hovered
    void OnMouseOver()
    {
        activeHoverTime = hoverTime;
    }
}
