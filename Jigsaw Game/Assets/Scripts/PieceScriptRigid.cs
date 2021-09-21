using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Rendering;

public class PieceScriptRigid : MonoBehaviour
{
    private Vector3 newPosition; //use outside
    private PointerRemoteRigid pr;
    private bool movable;
    private bool positionSet;
    private GameManager GM;
    private Vector3 correctPosition;
    private SortingGroup sortGroup;
    //private bool moved;

    private void Start()
    {
        // sorting group set
        pr = FindObjectOfType<PointerRemoteRigid>();
        GM = FindObjectOfType<GameManager>();
        sortGroup = GetComponent<SortingGroup>();
        correctPosition = transform.position;
        if (Random.Range(1, 101) < 50)
            newPosition = new Vector3(Random.Range(5f, 8f), Random.Range(-3.0f, 3.0f), GM.zIndex);
        else
            newPosition = new Vector3(Random.Range(-5f, -8f), Random.Range(-3.0f, 2.0f), GM.zIndex);
        
        GetComponent<SortingGroup>().sortingOrder = GM.zIndex;
        GM.zIndex++;
        GetComponent<SpriteRenderer>().color = Color.gray;
    }

    private void Update()
    {
        Invoke("AnimatePieceMove", 1f);
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
        if (Vector2.Distance(transform.position, correctPosition) < 0.5f)
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
            
            GetComponent<PieceScriptRigid>().enabled = false;
            transform.position = correctPosition;
            sortGroup.sortingOrder = 0;
        }
    }

    private void AnimatePieceMove()
    {
        if(CompareTag("OpenPiece") && transform.position != newPosition && pr.holdingObject == false)
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);
        //moved = true;
    }
}
