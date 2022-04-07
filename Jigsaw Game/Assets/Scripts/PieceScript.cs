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
        movable = true;
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
}
