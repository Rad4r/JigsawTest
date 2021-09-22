using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Rendering;

public class PieceScriptGrid : MonoBehaviour
{
    public GridScript[] Grids;
    private Vector3 newPosition; //use outside
    private bool movable;
    private bool positionSet;
    private GameManager GM;
    private Vector3 correctPosition;
    private SortingGroup sortGroup;
    //private bool moved;

    private void Awake()
    {
        // sorting group set
        GM = FindObjectOfType<GameManager>();
        sortGroup = GetComponent<SortingGroup>();
        correctPosition = transform.position;

        Invoke("GridFill", 1f);
        
        
        GetComponent<SortingGroup>().sortingOrder = GM.zIndex;
        GetComponent<SpriteRenderer>().color = Color.gray;
        GM.zIndex++;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        //Invoke("AnimatePieceMove", 1f);
        Invoke("PositionCheck", 2f);
        //TouchMove();
        //RewiredMove();
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
        if (Vector2.Distance(transform.position, correctPosition) < 0.5f) // change to vector 2
        {
            if (!positionSet)
            {
                tag = "ClosedPiece";
                GM.CorrectPieceSoundPlay();
                GM.piecesRemaining--;
                sortGroup.sortingOrder = 0;
                GetComponent<SpriteRenderer>().color = Color.clear;
                positionSet = true;
            }
            
            GetComponent<PieceScriptGrid>().enabled = false;
            transform.position = correctPosition;
            sortGroup.sortingOrder = 0;
        }
    }

    private void GridFill()
    {
        if (Random.Range(1, 101) < 50 && Grids[0].GetGridCount() <= Grids[0].maxPieces)
            Grids[0].AddPiece(gameObject);
        else if(Grids[1].GetGridCount() <= Grids[1].maxPieces)
            Grids[1].AddPiece(gameObject);
    }

    // private void AnimatePieceMove()
    // {
    //     if(CompareTag("OpenPiece") && transform.position != newPosition && pr.holdingObject == false)
    //         transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);
    //     //moved = true;
    // }
}
