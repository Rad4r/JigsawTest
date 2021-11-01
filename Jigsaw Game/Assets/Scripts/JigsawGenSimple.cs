using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawGenSimple : MonoBehaviour
{
    public GameObject[] jigsawPieces;
    public int horizontalPieces;
    public int verticalPieces;
    private List<GameObject> pieces;
    void Start()
    {
        pieces = new List<GameObject>();
        GeneratePuzzle();
    }
    
    void GeneratePuzzle()
    {
        Vector3 startPoint = Vector3.zero;
        int horizontalIterations = Mathf.RoundToInt((horizontalPieces - 2)/2);
        int verticalIterations = Mathf.RoundToInt((verticalPieces - 2)/2);
        float scaleDistance = 1.7f;
        float horizontalDistance = scaleDistance * horizontalPieces;
        float verticleDistance = scaleDistance * verticalPieces;
        
        //Corner Pieces
            pieces.Add(Instantiate(jigsawPieces[0], startPoint , Quaternion.identity));
            pieces.Add(Instantiate(jigsawPieces[0], startPoint + new Vector3( horizontalDistance,0,0), Quaternion.Euler(0,0,-90)));
            pieces.Add(Instantiate(jigsawPieces[0], startPoint + new Vector3(0,-verticleDistance,0), Quaternion.Euler(0,0,90)));
            pieces.Add(Instantiate(jigsawPieces[0], startPoint + new Vector3( horizontalDistance,-verticleDistance,0), Quaternion.Euler(0,0,180)));
            
        //Top and Bottom Pieces
        Vector3 pieceTwoStart = startPoint + new Vector3(scaleDistance, 0 , 0);
        Vector3 pieceThreeStart = startPoint + new Vector3(scaleDistance *2,0,0);
        for (int i = 0; i < horizontalIterations; i++)
        {
            pieces.Add(Instantiate(jigsawPieces[1], pieceTwoStart + new Vector3(scaleDistance * i,0,0), Quaternion.identity));
            pieces.Add(Instantiate(jigsawPieces[2], pieceThreeStart + new Vector3(scaleDistance * i,0,0), Quaternion.Euler(0,0,-90)));
            pieces.Add(Instantiate(jigsawPieces[1], pieceThreeStart + new Vector3(scaleDistance * i ,-verticleDistance,0), Quaternion.Euler(0,0,180)));
            pieces.Add(Instantiate(jigsawPieces[2], pieceTwoStart + new Vector3(scaleDistance * i,-verticleDistance,0), Quaternion.Euler(0,0,90)));
        }
        
        //Left and Right Pieces
        // Vector3 leftPieceTwo = startPoint + new Vector3(-0.01f * scaleValue, -3.14f * scaleValue,0);
        // Vector3 leftPieceThree = startPoint + new Vector3(0.24f * scaleValue, -1.45f * scaleValue,0);
        // for (int i = 0; i < verticalIterations; i++)
        // {
        //     pieces.Add(Instantiate(jigsawPieces[2], leftPieceThree + new Vector3(-0, -scaleFactor * i,0), Quaternion.identity));
        //     pieces.Add(Instantiate(jigsawPieces[1], leftPieceTwo + new Vector3(-0, -scaleFactor * i,0), Quaternion.Euler(0,0,90)));
        //     pieces.Add(Instantiate(jigsawPieces[2], leftPieceTwo + new Vector3(1.44f *  scaleValue + horizontalDistance , -scaleFactor * i ,0), Quaternion.Euler(0,0,180))); //6.45
        //     pieces.Add(Instantiate(jigsawPieces[1], leftPieceThree + new Vector3(1.44f * scaleValue + horizontalDistance , -scaleFactor * i ,0), Quaternion.Euler(0,0,-90))); //6.45  need to fix the divisiion factor
        // }
        //
        // //Middle Pieces
        // Vector3 midStart = startPoint + new Vector3(1.69f * scaleValue,-1.45f * scaleValue,0);
        // Vector3 oddmidStart = startPoint + new Vector3(3.4f * scaleValue,-1.45f * scaleValue,0);
        // for (int i = 0; i < verticalIterations *2; i++)
        // {
        //     float yChange = -scaleFactor/2 *i;
        //     if (i % 2 == 0)
        //     {
        //         for (int j = 0; j < horizontalIterations; j++)
        //         {
        //             pieces.Add(Instantiate(jigsawPieces[3], oddmidStart + new Vector3(scaleFactor *j, yChange,0), Quaternion.Euler(0,0,90)));
        //             pieces.Add(Instantiate(jigsawPieces[3], midStart + new Vector3(scaleFactor *j, yChange,0), Quaternion.identity));
        //         }
        //     }
        //     else
        //     {
        //         for (int j = 0; j < horizontalIterations; j++)
        //         {
        //             pieces.Add(Instantiate(jigsawPieces[3], oddmidStart + new Vector3(scaleFactor * j, yChange, 0), Quaternion.identity));
        //             pieces.Add(Instantiate(jigsawPieces[3], midStart + new Vector3(scaleFactor * j, yChange, 0), Quaternion.Euler(0, 0, 90)));
        //         }
        //     }
        // }

        //InsertPicture();
    }
}
