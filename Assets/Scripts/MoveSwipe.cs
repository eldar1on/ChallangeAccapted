using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSwipe : MonoBehaviour
{
    private Vector3 TouchPos;
    private float FirstValue;
    private float CurrentValue;
    public float SumValue;
    private bool HandsUp;
    private float ScreenWidth;
    public float Sensivity = 100f;
    private float LastSumValue;

    public float X_clamp;
    

    private float ClampFixer;
    
    private void Start()
    {
        ScreenWidth = Screen.width;
    }

    private bool SwipeDisable;
    public void CloseSwipe(float lastSum = 0f)
    {
        SwipeDisable = true;
        SumValue = lastSum;
    }

    private void Update()
    {
        if (SwipeDisable)
        {
            return;
        }
        
        if (Input.GetMouseButton(0))
        {
            if (HandsUp)
            {
                FirstValue = Input.mousePosition.x;
                HandsUp = false;
            }

            CurrentValue = Input.mousePosition.x;

            SumValue = CurrentValue - FirstValue;
            //
            
            //
            SumValue /= ScreenWidth;

            SumValue *= Sensivity;
            SumValue += LastSumValue;
            
//            Debug.Log(SumValue);
        }
        else
        {
            LastSumValue = SumValue;
            HandsUp = true;
        }

        if (SumValue > X_clamp)
        {
            SumValue = X_clamp;
            LastSumValue = X_clamp;
            //LastSumValue = X_clamp - SumValue;
            HandsUp = true;
        }else if (SumValue < -X_clamp)
        {
            SumValue = -X_clamp;
            LastSumValue = -X_clamp;
            HandsUp = true;
        }
        else
        {
            
        }
    }
}

