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

    private float speed;
    //private bool moved;

    private void Start()
    {
        Vector3 positionLimit = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)) + Vector3.left * .7f;
        
        GetComponent<Collider2D>().enabled = false;
        Invoke("ResetPiece", 2f);
        movable = true;
        // sorting group set
        GM = FindObjectOfType<GameManager>();
        sortGroup = GetComponent<SortingGroup>();
        correctPosition = transform.position;
        if (Random.Range(1, 101) < 50)
            newPosition = new Vector3(Random.Range(lowerRangeLimit, positionLimit.x), Random.Range(-3.0f, 3.0f), GM.zIndex); // 5.8f,8f default
        else
            newPosition = new Vector3(Random.Range(-lowerRangeLimit, -positionLimit.x), Random.Range(-3.0f, 2.0f), GM.zIndex);

        speed = Vector3.Distance(transform.position, newPosition);
        GetComponent<SortingGroup>().sortingOrder = GM.zIndex;
        GetComponent<SpriteRenderer>().color = Color.white;
        GM.zIndex++;
    }

    private void ResetPiece()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    private void Update()
    {
        Invoke("AnimatePieceMove", 1f);
        // PositionCheck();
    }

    private void AnimatePieceMove() //Check if the piece is being moved
    {
        if(CompareTag("OpenPiece") && transform.position != newPosition && movable)//not null
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
    }

    public Vector3 GetCorrectPosition()
    {
        return correctPosition;
    }

    public void SetMovable(bool move)
    {
        movable = move;
    }
    
    // private void PositionCheck()
    // {
    //     if (Vector2.Distance(transform.position, correctPosition) < 0.5f)
    //     {
    //         if (!positionSet) // change probably
    //         {
    //             tag = "ClosedPiece";
    //             GetComponent<SpriteRenderer>().color = Color.clear;
    //             GM.CorrectPieceSoundPlay();
    //             GM.piecesRemaining--;
    //             positionSet = true;
    //         }
    //         GetComponent<PieceScript>().enabled = false;
    //         transform.position = correctPosition;
    //         GetComponent<SortingGroup>().sortingOrder = 0;
    //     }
    // }
}
