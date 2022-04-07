using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Rendering;

public class PieceScript : MonoBehaviour
{
    [Range(4f,8f)]public float lowerRangeLimit;
    [Range(4f,8f)]public float upperRangeLimit;
    private Vector3 newPosition;
    private bool movable;
    private bool positionSet;
    private GameManager GM;
    private Vector3 correctPosition;
    private SortingGroup sortGroup;
    private Transform currentPiece;

    private float speed;
    //private bool moved;

    private void Start()
    {
        // sorting group set
        GM = FindObjectOfType<GameManager>();
        sortGroup = GetComponent<SortingGroup>();
        correctPosition = transform.position;
        if (Random.Range(1, 101) < 50)
            newPosition = new Vector3(Random.Range(lowerRangeLimit, upperRangeLimit), Random.Range(-3.0f, 3.0f), GM.zIndex); // 5.8f,8f default
        else
            newPosition = new Vector3(Random.Range(-lowerRangeLimit, -upperRangeLimit), Random.Range(-3.0f, 2.0f), GM.zIndex);

        speed = Vector3.Distance(transform.position, newPosition);
        GetComponent<SortingGroup>().sortingOrder = GM.zIndex;
        GetComponent<SpriteRenderer>().color = Color.white;
        GM.zIndex++;
    }

    private void Update()
    {
        Invoke("AnimatePieceMove", 1f);
        Invoke("TouchMove",2f);
        Invoke("PositionCheck", 2f);
    }
    
    private void TouchMove()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition); //need to get the top most one
                // Collider2D touchedCollider = ClosestObject(Physics2D.OverlapPointAll(touchPosition));
                
                if(GetComponent<Collider2D>() == touchedCollider)
                {
                    movable = true;
                    sortGroup.sortingOrder = GM.zIndex++;
                    GM.PickUpSoundPlay();
                }
            }
            if (touch.phase == TouchPhase.Moved && movable && CompareTag("OpenPiece"))
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
            GetComponent<PieceScript>().enabled = false;
            transform.position = correctPosition;
            sortGroup.sortingOrder = 0;
        }
    }

    private void AnimatePieceMove()
    {
        if(CompareTag("OpenPiece") && transform.position != newPosition && !movable)
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
    }
    
    private Collider2D ClosestObject(Collider2D[] collisionList)
    {
        float highestValue = 0;
        Collider2D closestObj = collisionList[0];
        
        for (int i = 0; i < collisionList.Length; i++)
        {
            if (collisionList[i].transform.position.z >= highestValue)
            {
                highestValue = collisionList[i].transform.position.z;
                closestObj = collisionList[i];
            }
                
        }

        return closestObj;
    }
}
