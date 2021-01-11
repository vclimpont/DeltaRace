using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class InputChecker
{
    public bool IsDiving { get; set; }
    public float HorizontalMovement { get; set; }

    public void CheckInputs()
    {
        IsDiving = Input.touchCount > 0 || Input.GetMouseButton(0); // Player dives on touch / click on screen

        if(IsDiving)
        {
            float centerX = Screen.width / 2f;
            Vector2 touchPosition = Vector2.zero;

            if(Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
            }
            else if(Input.GetMouseButton(0))
            {
                touchPosition = Input.mousePosition;
            }
            else
            {
                throw new System.Exception("Player is diving with no inputs triggered");
            }

            Assert.AreNotEqual(centerX, 0);
            HorizontalMovement = Mathf.Abs(touchPosition.x - centerX) / centerX;
        }
    }
}
