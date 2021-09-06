using UnityEngine;

public class JigsawGenTwo : MonoBehaviour
{
    public GameObject[] jigsawPieces;
    public float horizontalPieces;
    public float verticlPieces;
    private Vector3 startPoint;
    private float pieceIncrement;
    private float scaleFactor;
    private float scaleValue;

    private void Start()
    {
        scaleValue = jigsawPieces[0].transform.localScale.x;
        scaleFactor = (2.66f / 0.78f) * scaleValue; // 2.0748 divide by scale
        Debug.Log(scaleFactor);
        Debug.Log(jigsawPieces[0].transform.localScale.x);
        //pieceIncrement = 1.12f;
        startPoint = new Vector3(-5, 2.5f, 0);
        GeneratePuzzle();
    }

    void GeneratePuzzle()
    {
        // need adjustments for 10+ blocks
        //int horizontalIterations = Mathf.RoundToInt(horizontalPieces - 2/(0.5f*horizontalPieces)); // important
        int horizontalIterations = Mathf.RoundToInt((horizontalPieces - 2)/2); // important
        int verticalIterations = Mathf.RoundToInt((verticlPieces - 2)/2);
        float horizontalDistance = horizontalIterations * scaleFactor;
        float verticleDistance = verticalIterations * scaleFactor;
        
        //need to find constant factor to distance the let and right
        
        //piece one
            Instantiate(jigsawPieces[0], startPoint , Quaternion.identity);
            Instantiate(jigsawPieces[0], startPoint + new Vector3( 1.43f * scaleValue + horizontalDistance,0.25f * scaleValue,0), Quaternion.Euler(0,0,-90)); // -90z  2.19y       2.2x
            Instantiate(jigsawPieces[0], startPoint + new Vector3(0.25f * scaleValue,-1.42f * scaleValue - verticleDistance,0), Quaternion.Euler(0,0,90)); // 90z
            Instantiate(jigsawPieces[0], startPoint + new Vector3(1.68f * scaleValue + horizontalDistance,-1.18f * scaleValue - verticleDistance,0), Quaternion.Euler(0,0,180)); // 1800z
            
        //top n Bottom
        Vector3 pieceTwoStart = startPoint + new Vector3(1.7f * scaleValue, 0.26f * scaleValue, 0);
        Vector3 pieceThreeStart = startPoint + new Vector3(3.39f * scaleValue,0,0);
        Vector3 pieceThreeStartDown = startPoint + new Vector3(1.7f * scaleValue,0,0); //check  8f
        

        for (int i = 0; i < horizontalIterations; i++) // need adjustments for 10+ blocks
        {
            Instantiate(jigsawPieces[1], pieceTwoStart + new Vector3(scaleFactor * i,0,0), Quaternion.identity);
            Instantiate(jigsawPieces[2], pieceThreeStart + new Vector3(scaleFactor * i,0,0), Quaternion.Euler(0,0,-90));
            Instantiate(jigsawPieces[1], pieceThreeStartDown + new Vector3((scaleFactor + 0.005f) * i,-1.13f - verticleDistance,0), Quaternion.identity).GetComponent<SpriteRenderer>().flipY = true;
            Instantiate(jigsawPieces[2], pieceTwoStart + new Vector3(scaleFactor * i,-1.13f - verticleDistance,0), Quaternion.Euler(0,0,90)).GetComponent<SpriteRenderer>();
        }

        //Left and Right Side
        Vector3 leftPieceTwo = startPoint + new Vector3(-0.01f, -2.45f,0);
        Vector3 leftPieceThree = startPoint + new Vector3(0.19f, -1.13f,0);
        Debug.Log(horizontalDistance);
        
        
        for (int i = 0; i < verticalIterations; i++) //fix horizontal and vertical calc  // need adjustments for 10+ blocks
        {
            Instantiate(jigsawPieces[2], leftPieceThree + new Vector3(-0, -scaleFactor * i,0), Quaternion.identity);
            Instantiate(jigsawPieces[1], leftPieceTwo + new Vector3(-0, -scaleFactor * i,0), Quaternion.Euler(0,0,90));
            Instantiate(jigsawPieces[2], leftPieceTwo + new Vector3(1.13f + horizontalDistance , -scaleFactor * i ,0), Quaternion.Euler(0,0,180)); //6.45
            Instantiate(jigsawPieces[1], leftPieceThree + new Vector3(1.13f + horizontalDistance , -scaleFactor * i ,0), Quaternion.Euler(0,0,-90)); //6.45  need to fix the divisiion factor
        } // could be part of the other grid
        

        Vector3 midStart = startPoint + new Vector3(1.32f,-1.13f,0);
        Vector3 oddmidStart = startPoint + new Vector3(2.65f,-1.13f,0); //might need to adjust x

        for (int i = 0; i < verticalIterations *2; i++) // need adjustments for 10+ blocks
        {
            float yChange = -1.33f*i;

            if (i % 2 == 0)
            {
                for (int j = 0; j < horizontalIterations; j++)
                {
                    Instantiate(jigsawPieces[3], oddmidStart + new Vector3(scaleFactor *j, yChange,0), Quaternion.Euler(0,0,90));
                    Instantiate(jigsawPieces[3], midStart + new Vector3(scaleFactor *j, yChange,0), Quaternion.identity);
                }
                    
            }
            else if (i == verticalIterations-1)
            {
                for (int j = 0; j < horizontalIterations; j++)
                {
                    Instantiate(jigsawPieces[3], oddmidStart + new Vector3(scaleFactor * j - 0.005f, yChange, 0), Quaternion.identity);
                    Instantiate(jigsawPieces[3], midStart + new Vector3(scaleFactor * j - 0.005f, yChange, 0), Quaternion.Euler(0, 0, 90));
                }
            }
            else
            {
                for (int j = 0; j < horizontalIterations; j++)
                {
                    Instantiate(jigsawPieces[3], oddmidStart + new Vector3(scaleFactor * j, yChange, 0), Quaternion.identity);
                    Instantiate(jigsawPieces[3], midStart + new Vector3(scaleFactor * j, yChange, 0), Quaternion.Euler(0, 0, 90));
                }
            }
        }
        
        
    }
    
}
