using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PieceHandler : MonoBehaviour
{
    private GameManager GM;
    private Transform currentPiece;
    private Vector3 pieceCorrectPosition;
    private bool movable;

    private bool positionSet;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }
    
    private void Update()
    {
        // Invoke("TouchMove",2f); //set open piece after a delay in the piece script
        TouchMove();
        if (currentPiece)
        {
            currentPiece.GetComponent<PieceScript>().SetMovable(false);
            PositionCheck();
        }
            
        
        // Debug.Log(currentPiece);
    }
    
    private void TouchMove()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                if(Physics2D.OverlapPointAll(touchPosition).Length > 0 && Physics2D.OverlapPoint(touchPosition).tag == "OpenPiece")
                    currentPiece = ClosestObject(Physics2D.OverlapPointAll(touchPosition)).transform;
                
                if(currentPiece)
                {
                    positionSet = false;
                    pieceCorrectPosition = currentPiece.GetComponent<PieceScript>().GetCorrectPosition();
                    currentPiece.GetComponent<SortingGroup>().sortingOrder = GM.zIndex++;
                    GM.PickUpSoundPlay();
                }
            }
            if (touch.phase == TouchPhase.Moved && currentPiece && currentPiece.tag == "OpenPiece") //not moving
                currentPiece.position = new Vector2(touchPosition.x, touchPosition.y);

            if (touch.phase == TouchPhase.Ended)
            {
                if (currentPiece)
                    currentPiece.GetComponent<PieceScript>().SetMovable(true);
                currentPiece = null;
            }
                
        }
    }
    private void PositionCheck()
    {
        // Debug.Log(Vector2.Distance(transform.position, pieceCorrectPosition));
        // Debug.Log(pieceCorrectPosition);
        if (Vector2.Distance(currentPiece.position, pieceCorrectPosition) < 0.5f)
        {
            if (!positionSet) // change probably
            {
                currentPiece.tag = "ClosedPiece";
                currentPiece.GetComponent<SpriteRenderer>().color = Color.clear;
                GM.CorrectPieceSoundPlay();
                GM.piecesRemaining--;
                positionSet = true;
            }
            currentPiece.GetComponent<PieceScript>().enabled = false;
            currentPiece.position = pieceCorrectPosition;
            currentPiece.GetComponent<SortingGroup>().sortingOrder = 0;
        }
    }
    
    private Collider2D ClosestObject(Collider2D[] collisionList) //don't check closed pieces
    {
        float highestValue = 0;
        Collider2D closestObj = collisionList[0]; //e
        
        for (int i = 0; i < collisionList.Length; i++)
        {
            if (collisionList[i].transform.position.z >= highestValue)
            {
                highestValue = collisionList[i].transform.position.z;
                closestObj = collisionList[i];
            }
                
        }

        // if (closestObj.tag == "OpenPiece")
            return closestObj;
        // return null;
    }
}
