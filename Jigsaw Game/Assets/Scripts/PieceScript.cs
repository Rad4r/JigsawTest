using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Rendering;
using Rewired;

public class PieceScript : MonoBehaviour
{
    private bool movable;
    private bool positionSet;
    private GameManager GM;
    private Vector3 correctPosition;
    private SortingGroup sortGroup;
    private Player player;

    private void Start()
    {
        // sorting group set
        player = ReInput.players.GetPlayer(0);
        GM = FindObjectOfType<GameManager>();
        sortGroup = GetComponent<SortingGroup>();
        correctPosition = transform.position;
        if (Random.Range(1, 101) < 50)
        {
            transform.position = new Vector3(Random.Range(5f, 8f), Random.Range(-3.0f, 3.0f), GM.zIndex);
            GetComponent<SortingGroup>().sortingOrder = GM.zIndex;
        }
        
        else
        {
            transform.position = new Vector3(Random.Range(-5f, -8f), Random.Range(-3.0f, 2.0f), GM.zIndex);
            GetComponent<SortingGroup>().sortingOrder = GM.zIndex;
        }
        
        //SetTarget position and move it there with lerp or something
        GM.zIndex++;
    }

    private void Update()
    {
        Invoke("PositionCheck",1f);
        //TouchMove();
        //RewiredMove();
    }

    private void RewiredMove()
    {
        if (player.GetAxis("Drag Horizontal") == 0f && player.GetAxis("Drag Vertical") == 0f)
        {
            //fwafa
            if (player.GetButtonDown("TouchClick"))
            {
                
            }
        }
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
                tag = "ClosedPiece";
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
