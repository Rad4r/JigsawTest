using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawGenSimpleTwo : MonoBehaviour
{
    public Transform parentPiece;
    public GameObject[] jigsawPieces;
    public int horizontalPieces;
    public int verticalPieces;
    public GameObject puzzleImage;
    //public Sprite
    private List<GameObject> pieces;
    private Vector3 startPoint;
    
    //private float scaleDistance;
    private float scaleDistanceX;
    private float scaleDistanceY;
    

    private float scaleValueX;
    private float scaleValueY;
    void Start()
    {
        Sprite puzzleSprite = puzzleImage.GetComponent<SpriteRenderer>().sprite;
        Vector3 puzzleScale = puzzleImage.transform.localScale;
        float scaleX = puzzleSprite.rect.width/ 160 * puzzleScale.x ;
        scaleValueX = scaleX / horizontalPieces; //magic number change
        float scaleY = (puzzleSprite.rect.height * puzzleScale.y) / verticalPieces;
        scaleValueY = scaleY / 152;


        // for (int i = 0; i < jigsawPieces.Length; i++)
        // {
        //     jigsawPieces[i].transform.localScale = new Vector3(defaultScale, defaultScale, 1);
        //     //jigsawPieces[i].transform.GetChild(0).localScale = new Vector3(1, 1, 1);
        // }
        jigsawPieces[0].transform.localScale = new Vector3(scaleValueX, scaleValueY, 1);
        jigsawPieces[1].transform.localScale = new Vector3(scaleValueX, scaleValueY, 1);
        jigsawPieces[2].transform.localScale = new Vector3(scaleValueY, scaleValueX, 1);
            
            
        
        //scaleDistance = defaultScale*1.7f; //was 1.7 default
        scaleDistanceX = scaleValueX * 1.7f;
        scaleDistanceY = scaleValueY * 1.7f;
        pieces = new List<GameObject>();

        float halfPointX = (scaleValueX*0.87f) + (scaleValueX*0.84f*(horizontalPieces - 2)); // still needs the fix
        float halfPointY = (scaleValueY*0.87f) + (scaleValueY*0.84f*(verticalPieces - 2));
        startPoint = puzzleImage.transform.position + new Vector3(-halfPointX,halfPointY,0);
        GeneratePuzzle();
    }
    
    void GeneratePuzzle()
    {
        
        //float doubleDistance = scaleDistance * 2;
        float doubleDistanceX = scaleDistanceX * 2;
        float doubleDistanceY = scaleDistanceY * 2;
        int horizontalIterations = Mathf.RoundToInt((horizontalPieces - 2)/2);
        int verticalIterations = Mathf.RoundToInt((verticalPieces - 2)/2);
        float horizontalDistance = scaleDistanceX * (horizontalPieces -1);
        float verticleDistance = scaleDistanceY * (verticalPieces -1);
        
        //Corner Pieces
            pieces.Add(Instantiate(jigsawPieces[0], startPoint , Quaternion.identity, parentPiece));
            pieces.Add(Instantiate(jigsawPieces[0], startPoint + new Vector3( horizontalDistance,-verticleDistance,0), Quaternion.Euler(0,0,180),parentPiece));
            jigsawPieces[0].transform.localScale = new Vector3(scaleValueY, scaleValueX, 1);
            pieces.Add(Instantiate(jigsawPieces[0], startPoint + new Vector3( horizontalDistance,0,0), Quaternion.Euler(0,0,-90),parentPiece));
            pieces.Add(Instantiate(jigsawPieces[0], startPoint + new Vector3(0,-verticleDistance,0), Quaternion.Euler(0,0,90),parentPiece));
            
            
        //Top and Bottom Pieces
        
        
        Vector3 pieceTwoStart = startPoint + new Vector3(scaleDistanceX, 0 , 0);
        Vector3 pieceThreeStart = startPoint + new Vector3(doubleDistanceX,0,0); //horizonal 3
        for (int i = 0; i < horizontalIterations; i++)
        {
            pieces.Add(Instantiate(jigsawPieces[1], pieceTwoStart + new Vector3(doubleDistanceX * i,0,0), Quaternion.identity,parentPiece));
            pieces.Add(Instantiate(jigsawPieces[2], pieceThreeStart + new Vector3(doubleDistanceX *  i,0,0), Quaternion.Euler(0,0,-90),parentPiece));
            pieces.Add(Instantiate(jigsawPieces[1], pieceThreeStart + new Vector3(doubleDistanceX * i ,-verticleDistance,0), Quaternion.Euler(0,0,180),parentPiece));
            pieces.Add(Instantiate(jigsawPieces[2], pieceTwoStart + new Vector3(doubleDistanceX * i,-verticleDistance,0), Quaternion.Euler(0,0,90),parentPiece));
        }
        
        //Left and Right Pieces
        
        jigsawPieces[1].transform.localScale = new Vector3(scaleValueY, scaleValueX, 1);
        jigsawPieces[2].transform.localScale = new Vector3(scaleValueX, scaleValueY, 1);
        
         Vector3 leftPieceTwo = startPoint + new Vector3(0, -doubleDistanceY,0);
         Vector3 leftPieceThree = startPoint + new Vector3(0, -scaleDistanceY,0);
         for (int i = 0; i < verticalIterations; i++)
         {
             pieces.Add(Instantiate(jigsawPieces[2], leftPieceThree + new Vector3(0, -doubleDistanceY * i,0), Quaternion.identity,parentPiece));
             pieces.Add(Instantiate(jigsawPieces[1], leftPieceTwo + new Vector3(0, -doubleDistanceY * i,0), Quaternion.Euler(0,0,90),parentPiece));
             pieces.Add(Instantiate(jigsawPieces[2], leftPieceTwo + new Vector3(horizontalDistance , -doubleDistanceY * i ,0), Quaternion.Euler(0,0,180),parentPiece)); //6.45
             pieces.Add(Instantiate(jigsawPieces[1], leftPieceThree + new Vector3(horizontalDistance , -doubleDistanceY * i ,0), Quaternion.Euler(0,0,-90),parentPiece)); //6.45  need to fix the divisiion factor
         }
        
        //Middle Pieces
        Vector3 midStart = startPoint + new Vector3(scaleDistanceX,-scaleDistanceY,0);
        Vector3 oddmidStart = startPoint + new Vector3(doubleDistanceX,-scaleDistanceY,0);
        for (int i = 0; i < verticalIterations *2; i++)
        {
            //float yChange = -scaleFactor/2 *i;
            if (i % 2 == 0)
            {
                
                for (int j = 0; j < horizontalIterations; j++)
                {
                    jigsawPieces[3].transform.localScale = new Vector3(scaleValueY, scaleValueX, 1);
                    pieces.Add(Instantiate(jigsawPieces[3], oddmidStart + new Vector3(doubleDistanceX *j, -scaleDistanceY*i,0), Quaternion.Euler(0,0,90),parentPiece));
                    jigsawPieces[3].transform.localScale = new Vector3(scaleValueX, scaleValueY, 1);
                    pieces.Add(Instantiate(jigsawPieces[3], midStart + new Vector3(doubleDistanceX *j, -scaleDistanceY*i,0), Quaternion.identity,parentPiece));
                }
            }
            else
            {
                
                for (int j = 0; j < horizontalIterations; j++)
                {
                    jigsawPieces[3].transform.localScale = new Vector3(scaleValueX, scaleValueY, 1);
                    pieces.Add(Instantiate(jigsawPieces[3], oddmidStart + new Vector3(doubleDistanceX * j, -scaleDistanceY*i, 0), Quaternion.identity,parentPiece));
                    jigsawPieces[3].transform.localScale = new Vector3(scaleValueY, scaleValueX, 1);
                    pieces.Add(Instantiate(jigsawPieces[3], midStart + new Vector3(doubleDistanceX * j, -scaleDistanceY*i, 0), Quaternion.Euler(0, 0, 90),parentPiece));
                }
            }
        }

        InsertPicture();
    }
    
    void InsertPicture()
    {
        foreach (var piece in pieces)
        {
            GameObject imgPuz = Instantiate(puzzleImage, puzzleImage.transform.position, Quaternion.identity);
            imgPuz.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            imgPuz.transform.parent = piece.transform.GetChild(0);
        }

        puzzleImage.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 80);
    }
}
