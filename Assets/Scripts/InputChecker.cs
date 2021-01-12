using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class InputChecker
{
    public bool IsDiving { get; set; }
    public float HorizontalMovement { get; set; }

    public void CheckDive()
    {
        IsDiving = Input.touchCount > 0 || Input.GetMouseButton(0); // Player dives on touch / click on screen
    }

    public void CheckHorizontalMovement(Vector3 playerPosition)
    {
        if (IsDiving)
        {
            Vector2 touchPosition = Vector2.zero;

            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
            }
            else if (Input.GetMouseButton(0))
            {
                if(!IsMouseOnScreen())
                { return; }

                touchPosition = Input.mousePosition;
            }
            else
            {
                throw new System.Exception("Player is diving with no inputs triggered");
            }

            Vector2 playerScreenPosition = Camera.main.WorldToScreenPoint(playerPosition);
            HorizontalMovement = (touchPosition.x - playerScreenPosition.x) / Screen.width;
        }
    }

    bool IsMouseOnScreen()
    {
        Vector3 p = Input.mousePosition;
        return p.x >= 0 && p.x < Screen.width && p.y >= 0 && p.y < Screen.height;
    }
}
