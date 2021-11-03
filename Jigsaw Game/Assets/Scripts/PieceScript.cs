using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Rendering;

public class PieceScript : MonoBehaviour
{
    [Range(4f,8f)]public float lowerRangeLimit;
    [Range(4f,8f)]public float upperRangeLimit;
    private Vector3 newPosition;
    private PointerRemote pr;
    private bool movable;
    private bool positionSet;
    private GameManager GM;
    private Vector3 correctPosition;
    private SortingGroup sortGroup;
    //private bool moved;

    private void Start()
    {
        // sorting group set
        pr = FindObjectOfType<PointerRemote>();
        GM = FindObjectOfType<GameManager>();
        sortGroup = GetComponent<SortingGroup>();
        correctPosition = transform.position;
        if (Random.Range(1, 101) < 50)
            newPosition = new Vector3(Random.Range(lowerRangeLimit, upperRangeLimit), Random.Range(-3.0f, 3.0f), GM.zIndex); // 5.8f,8f default
        else
            newPosition = new Vector3(Random.Range(-lowerRangeLimit, -upperRangeLimit), Random.Range(-3.0f, 2.0f), GM.zIndex);
        
        GetComponent<SortingGroup>().sortingOrder = GM.zIndex;
        GetComponent<SpriteRenderer>().color = Color.white;
        GM.zIndex++;
    }

    private void Update()
    {
        Invoke("AnimatePieceMove", 1f);
        Invoke("PositionCheck", 2f);
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
        if(CompareTag("OpenPiece") && transform.position != newPosition && pr.holdingObject == false)
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);
    }
}
