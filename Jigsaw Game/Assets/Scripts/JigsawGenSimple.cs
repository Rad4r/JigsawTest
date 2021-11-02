using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawGenSimple : MonoBehaviour
{
    public GameObject[] jigsawPieces;
    public int horizontalPieces;
    public int verticalPieces;
    public GameObject puzzleImage;
    //public Sprite
    private List<GameObject> pieces;
    private Vector3 startPoint;
    
    private float scaleDistance;
    

    public float defaultScale;
    void Start()
    {


        for (int i = 0; i < jigsawPieces.Length; i++)
        {
            jigsawPieces[i].transform.localScale = new Vector3(defaultScale, defaultScale, 1);
            //jigsawPieces[i].transform.GetChild(0).localScale = new Vector3(1, 1, 1);
        }
            
        
        scaleDistance = defaultScale*1.7f; //was 1.7 default
        pieces = new List<GameObject>();
        GeneratePuzzle();
    }
    
    void GeneratePuzzle()
    {
        startPoint = new Vector3(-6,4,0);
        float doubleDistance = scaleDistance * 2;
        int horizontalIterations = Mathf.RoundToInt((horizontalPieces - 2)/2);
        int verticalIterations = Mathf.RoundToInt((verticalPieces - 2)/2);
        float horizontalDistance = scaleDistance * (horizontalPieces -1);
        float verticleDistance = scaleDistance * (verticalPieces -1);
        
        //Corner Pieces
            pieces.Add(Instantiate(jigsawPieces[0], startPoint , Quaternion.identity));
            pieces.Add(Instantiate(jigsawPieces[0], startPoint + new Vector3( horizontalDistance,0,0), Quaternion.Euler(0,0,-90)));
            pieces.Add(Instantiate(jigsawPieces[0], startPoint + new Vector3(0,-verticleDistance,0), Quaternion.Euler(0,0,90)));
            pieces.Add(Instantiate(jigsawPieces[0], startPoint + new Vector3( horizontalDistance,-verticleDistance,0), Quaternion.Euler(0,0,180)));
            
        //Top and Bottom Pieces
        Vector3 pieceTwoStart = startPoint + new Vector3(scaleDistance, 0 , 0);
        Vector3 pieceThreeStart = startPoint + new Vector3(doubleDistance,0,0); //horizonal 3
        for (int i = 0; i < horizontalIterations; i++)
        {
            pieces.Add(Instantiate(jigsawPieces[1], pieceTwoStart + new Vector3(doubleDistance * i,0,0), Quaternion.identity));
            pieces.Add(Instantiate(jigsawPieces[2], pieceThreeStart + new Vector3(doubleDistance *  i,0,0), Quaternion.Euler(0,0,-90)));
            pieces.Add(Instantiate(jigsawPieces[1], pieceThreeStart + new Vector3(doubleDistance * i ,-verticleDistance,0), Quaternion.Euler(0,0,180)));
            pieces.Add(Instantiate(jigsawPieces[2], pieceTwoStart + new Vector3(doubleDistance * i,-verticleDistance,0), Quaternion.Euler(0,0,90)));
        }
        
        //Left and Right Pieces
         Vector3 leftPieceTwo = startPoint + new Vector3(0, -doubleDistance,0);
         Vector3 leftPieceThree = startPoint + new Vector3(0, -scaleDistance,0);
         for (int i = 0; i < verticalIterations; i++)
         {
             pieces.Add(Instantiate(jigsawPieces[2], leftPieceThree + new Vector3(0, -doubleDistance * i,0), Quaternion.identity));
             pieces.Add(Instantiate(jigsawPieces[1], leftPieceTwo + new Vector3(0, -doubleDistance * i,0), Quaternion.Euler(0,0,90)));
             pieces.Add(Instantiate(jigsawPieces[2], leftPieceTwo + new Vector3(horizontalDistance , -doubleDistance * i ,0), Quaternion.Euler(0,0,180))); //6.45
             pieces.Add(Instantiate(jigsawPieces[1], leftPieceThree + new Vector3(horizontalDistance , -doubleDistance * i ,0), Quaternion.Euler(0,0,-90))); //6.45  need to fix the divisiion factor
         }
        
        //Middle Pieces
        Vector3 midStart = startPoint + new Vector3(scaleDistance,-scaleDistance,0);
        Vector3 oddmidStart = startPoint + new Vector3(doubleDistance,-scaleDistance,0);
        for (int i = 0; i < verticalIterations *2; i++)
        {
            //float yChange = -scaleFactor/2 *i;
            if (i % 2 == 0)
            {
                for (int j = 0; j < horizontalIterations; j++)
                {
                    pieces.Add(Instantiate(jigsawPieces[3], oddmidStart + new Vector3(doubleDistance *j, -scaleDistance*i,0), Quaternion.Euler(0,0,90)));
                    pieces.Add(Instantiate(jigsawPieces[3], midStart + new Vector3(doubleDistance *j, -scaleDistance*i,0), Quaternion.identity));
                }
            }
            else
            {
                for (int j = 0; j < horizontalIterations; j++)
                {
                    pieces.Add(Instantiate(jigsawPieces[3], oddmidStart + new Vector3(doubleDistance * j, -scaleDistance*i, 0), Quaternion.identity));
                    pieces.Add(Instantiate(jigsawPieces[3], midStart + new Vector3(doubleDistance * j, -scaleDistance*i, 0), Quaternion.Euler(0, 0, 90)));
                }
            }
        }

        //InsertPicture();
    }
    
    // void InsertPicture()
    // {
    //     //GameObject piGO = Instantiate(pi, Vector3.zero, Quaternion.identity);
    //     pi.GetComponent<SpriteRenderer>().sprite = puzzleImage;
    //     
    //     foreach (var piece in pieces)
    //     {
    //         GameObject piGO = Instantiate(pi, Vector3.zero, Quaternion.identity);
    //         piGO.transform.parent = piece.transform;
    //     }
    // }
}
