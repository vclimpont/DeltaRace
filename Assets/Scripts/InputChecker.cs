using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class InputChecker
{
    public bool IsDiving { get; set; }
    public float HorizontalMovement { get; set; }

    private Vector2 previousPosition;

    public void CheckDive()
    {
        IsDiving = Input.touchCount > 0 || Input.GetMouseButton(0); // Player dives on touch / click on screen

        if(!IsDiving)
        {
            previousPosition = Vector2.zero;
            HorizontalMovement = 0f;
        }
    }

    public void CheckHorizontalMovement(Vector3 playerPosition)
    {
        if (IsDiving)
        {
            Vector2 deltaPosition;

            if (Input.touchCount > 0)
            {
                deltaPosition = Input.GetTouch(0).deltaPosition;
            }
            else if (Input.GetMouseButton(0))
            {
                if (!IsMouseOnScreen())
                {
                    HorizontalMovement = 0f;
                    return; 
                }

                Vector2 mousePosition = Input.mousePosition;
                deltaPosition = mousePosition - previousPosition;
                previousPosition = mousePosition;

                if(deltaPosition == mousePosition)
                {
                    return;
                }
            }
            else
            {
                throw new System.Exception("Player is diving with no inputs triggered");
            }

            HorizontalMovement = Mathf.Clamp(deltaPosition.x / (Screen.width / 2f), -0.5f, 0.5f);
        }
    }

    bool IsMouseOnScreen()
    {
        Vector3 p = Input.mousePosition;
        return p.x >= 0 && p.x < Screen.width && p.y >= 0 && p.y < Screen.height;
    }
}
