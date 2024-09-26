using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
//using GG.Infrastructure.Utils.Swipe;

public class PlayerMovement : MonoBehaviour
{
    public enum SwipeDirection
    {
        Up, Down, Left, Right
    }

    public static event Action<SwipeDirection> Swipe;
    private bool swiping = false;
    private bool eventSent = false;
    private Vector2 lastPosition;
    private Vector3 pos;
    private Vector3 targetPos;
    private bool isMove;
    private Vector3 moveDir = Vector3.forward;  
    [SerializeField] private float speed;

    private void Start()
    {
        targetPos = transform.position; 
    }

   
    private void Update()
    {

        //Movement

        //Swipe Detecter
        if (Input.touchCount == 0)
            return;

        if (Input.GetTouch(0).deltaPosition.sqrMagnitude != 0)
        {
            if (swiping ==  false)
            {
                swiping = true;
                lastPosition = Input.GetTouch(0).position;  
                return; 
            }
            else
            {
                if (!eventSent)
                {
                    if (Swipe != null)
                    {
                        Vector2 direction = Input.GetTouch(0).position - lastPosition;  
                        //check huong vuot cua nguoi choi
                        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                        {
                            if (direction.x > 0)
                            {
                                Swipe(SwipeDirection.Right);
                            }
                            else
                            {
                                Swipe(SwipeDirection.Left);
                            }
                        }
                        else
                        {
                            if (direction.y > 0)
                            {
                                Swipe(SwipeDirection.Up);
                            }
                            else
                            {
                                Swipe(SwipeDirection.Down);
                            }
                        }

                        eventSent = true;           
                    }
                }
            }
        }
        else
        {
            swiping = false;
            eventSent = false;
        }
    }
}
