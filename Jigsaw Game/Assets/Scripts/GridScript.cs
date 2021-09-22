using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour
{
    public int maxPieces;
    private List<GameObject> puzzlePieces;
    private List<Vector2> puzzlePositions;
    private List<Vector2> puzzleScales;
    private Vector2 currentPosition;

    private void Start()
    {
        puzzlePieces = new List<GameObject>(maxPieces);
        puzzlePositions = new List<Vector2>();
        puzzleScales = new List<Vector2>();
        // RectTransform ret = (RectTransform) transform;
        // Vector3 newVector = Camera.main.ScreenToWorldPoint(new Vector3(0, ret.rect.height * transform.localScale.y, 0)) + Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        // currentPosition = transform.position + newVector + new Vector3(0,0.5f, 0);
        currentPosition = transform.position + new Vector3(-1, 3);
        
        //Debug.Log("4%4 8%4 12%4 16%4:     " + 4%4  + " " + 8%4 + " " + 12%4  + " " + 15%4);
    }

    public void AddPiece(GameObject piece)
    {
        puzzlePieces.Add(piece);
        puzzleScales.Add(piece.transform.localScale);
        puzzlePositions.Add(currentPosition);
        piece.transform.position = currentPosition;
        currentPosition += new Vector2(2f, 0f);
        if(GetGridCount()%2 == 0)
            currentPosition += new Vector2(-4f, -2f);
        //could return false or something
    }
    
    public GameObject GetPiece(int piece)
    {
        return puzzlePieces[piece];
    }

    public Vector2 GetPiecePosition(int piece)
    {
        return puzzlePositions[piece];
    }

    public int GetGridCount()
    {
        return puzzlePieces.Count;
    }

    // public GameObject Piece
    // {
    //     get { return puzzlePieces[0];}
    //     set { puzzlePieces[0] = value; }
    // }
}
