using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class JigsawGenThree : MonoBehaviour
{
    public GameObject[] jigsawPieces;
    public Sprite puzzleImage;
    public GameObject pi;
    public float horizontalPieces;
    public float verticlPieces;
    private float scaleValueX;
    private float scaleValueY;
    private float scaleValue;
    private Vector3 startPoint;
    private float scaleFactor;
    private List<GameObject> pieces;

    private void Start()
    {
        pieces = new List<GameObject>();
        //divide the height and width by the x and y   //will need to seperate the scale value to x AND y to properly work
        float scaleX = puzzleImage.rect.size.x / horizontalPieces;
        scaleValueX = scaleX / 170;
        scaleValue = scaleValueX;
        // float scaleY = puzzleImage.rect.size.y / verticlPieces;
        // scaleValueY = scaleY / 170;
        //convert the pixels to normal world scale // 170 is one square pixel
        
        //scaleValue = 1; //need changing with image size
        for (int i = 0; i < jigsawPieces.Length; i++)
        {
            jigsawPieces[i].transform.localScale = new Vector3(scaleValue, scaleValue, 1);
        }

        float halfed = puzzleImage.rect.size.x / 2;
        float worldHalf = halfed / 170 ;
        startPoint = new Vector3(-worldHalf + 0.6f, 2.5f, 0); // change the start point //-5x default need 3.21 for accurate
        scaleFactor = (2.66f / 0.78f) * scaleValue;
        GeneratePuzzle();
    }

    void GeneratePuzzle()
    {
        int horizontalIterations = Mathf.RoundToInt((horizontalPieces - 2)/2);
        int verticalIterations = Mathf.RoundToInt((verticlPieces - 2)/2);
        float horizontalDistance = horizontalIterations * scaleFactor;
        float verticleDistance = verticalIterations * scaleFactor;
        
        
        //Corner Pieces
            pieces.Add(Instantiate(jigsawPieces[0], startPoint , Quaternion.identity));
            pieces.Add(Instantiate(jigsawPieces[0], startPoint + new Vector3( 1.43f * scaleValue + horizontalDistance,0.25f * scaleValue,0), Quaternion.Euler(0,0,-90)));
            pieces.Add(Instantiate(jigsawPieces[0], startPoint + new Vector3(0.25f * scaleValue,-1.42f * scaleValue - verticleDistance,0), Quaternion.Euler(0,0,90)));
            pieces.Add(Instantiate(jigsawPieces[0], startPoint + new Vector3(1.68f * scaleValue + horizontalDistance,-1.18f * scaleValue - verticleDistance,0), Quaternion.Euler(0,0,180)));
            
        //Top and Bottom Pieces
        Vector3 pieceTwoStart = startPoint + new Vector3(1.7f * scaleValue, 0.26f * scaleValue, 0);
        Vector3 pieceThreeStart = startPoint + new Vector3(3.39f * scaleValue,0,0);
        for (int i = 0; i < horizontalIterations; i++)
        {
            pieces.Add(Instantiate(jigsawPieces[1], pieceTwoStart + new Vector3(scaleFactor * i,0,0), Quaternion.identity));
            pieces.Add(Instantiate(jigsawPieces[2], pieceThreeStart + new Vector3(scaleFactor * i,0,0), Quaternion.Euler(0,0,-90)));
            pieces.Add(Instantiate(jigsawPieces[1], pieceThreeStart + new Vector3(scaleFactor * i + 0.01f,-1.44f * scaleValue - verticleDistance,0), Quaternion.Euler(0,0,180)));
            pieces.Add(Instantiate(jigsawPieces[2], pieceTwoStart + new Vector3(scaleFactor * i,-1.44f * scaleValue - verticleDistance,0), Quaternion.Euler(0,0,90)));
        }

        //Left and Right Pieces
        Vector3 leftPieceTwo = startPoint + new Vector3(-0.01f * scaleValue, -3.14f * scaleValue,0);
        Vector3 leftPieceThree = startPoint + new Vector3(0.24f * scaleValue, -1.45f * scaleValue,0);
        for (int i = 0; i < verticalIterations; i++)
        {
            pieces.Add(Instantiate(jigsawPieces[2], leftPieceThree + new Vector3(-0, -scaleFactor * i,0), Quaternion.identity));
            pieces.Add(Instantiate(jigsawPieces[1], leftPieceTwo + new Vector3(-0, -scaleFactor * i,0), Quaternion.Euler(0,0,90)));
            pieces.Add(Instantiate(jigsawPieces[2], leftPieceTwo + new Vector3(1.44f *  scaleValue + horizontalDistance , -scaleFactor * i ,0), Quaternion.Euler(0,0,180))); //6.45
            pieces.Add(Instantiate(jigsawPieces[1], leftPieceThree + new Vector3(1.44f * scaleValue + horizontalDistance , -scaleFactor * i ,0), Quaternion.Euler(0,0,-90))); //6.45  need to fix the divisiion factor
        }
        
        //Middle Pieces
        Vector3 midStart = startPoint + new Vector3(1.69f * scaleValue,-1.45f * scaleValue,0);
        Vector3 oddmidStart = startPoint + new Vector3(3.4f * scaleValue,-1.45f * scaleValue,0);
        for (int i = 0; i < verticalIterations *2; i++)
        {
            float yChange = -scaleFactor/2 *i;
            if (i % 2 == 0)
            {
                for (int j = 0; j < horizontalIterations; j++)
                {
                    pieces.Add(Instantiate(jigsawPieces[3], oddmidStart + new Vector3(scaleFactor *j, yChange,0), Quaternion.Euler(0,0,90)));
                    pieces.Add(Instantiate(jigsawPieces[3], midStart + new Vector3(scaleFactor *j, yChange,0), Quaternion.identity));
                }
            }
            else
            {
                for (int j = 0; j < horizontalIterations; j++)
                {
                    pieces.Add(Instantiate(jigsawPieces[3], oddmidStart + new Vector3(scaleFactor * j, yChange, 0), Quaternion.identity));
                    pieces.Add(Instantiate(jigsawPieces[3], midStart + new Vector3(scaleFactor * j, yChange, 0), Quaternion.Euler(0, 0, 90)));
                }
            }
        }

        InsertPicture();
    }

    void InsertPicture()
    {
        //GameObject piGO = Instantiate(pi, Vector3.zero, Quaternion.identity);
        pi.GetComponent<SpriteRenderer>().sprite = puzzleImage;
        
        foreach (var piece in pieces)
        {
            GameObject piGO = Instantiate(pi, Vector3.zero, Quaternion.identity);
            piGO.transform.parent = piece.transform;
        }
    }
}
