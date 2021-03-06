using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Rendering;

public class PieceScript : MonoBehaviour
{
    private bool movable;
    private bool positionSet;
    private GameManager GM;
    private Vector3 correctPosition;
    private SortingGroup sortGroup;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        sortGroup = GetComponent<SortingGroup>();
        correctPosition = transform.position;
        if(Random.Range(1,101) <50)
            transform.position = new Vector3(Random.Range(4.5f, 6f), Random.Range(-3.0f, 3.0f), 0);
        else
            transform.position = new Vector3(Random.Range(-4.5f, -6f), Random.Range(-3.0f, 2.0f), 0);
    }

    private void Update()
    {
        Invoke("PositionCheck",1f);
        TouchMove();
    }

    private void TouchMove()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                if(GetComponent<Collider2D>() == touchedCollider)
                {
                    movable = true;
                    sortGroup.sortingOrder = GM.zIndex++;
                    GM.PickUpSoundPlay();
                }
            }
            if (touch.phase == TouchPhase.Moved && movable)
                transform.position = new Vector2(touchPosition.x, touchPosition.y);
            
            if (touch.phase == TouchPhase.Ended)
                movable = false;
        }
    }
    private void PositionCheck()
    {
        if (Vector3.Distance(transform.position, correctPosition) < 0.5f)
        {
            if (!positionSet)
            {
                GM.CorrectPieceSoundPlay();
                GM.piecesRemaining--;
                sortGroup.sortingOrder = 0;
                GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                positionSet = true;
            }
            
            GetComponent<PieceScript>().enabled = false;
            transform.position = correctPosition;
            sortGroup.sortingOrder = 0;
        }
    }
}
