using UnityEngine;

public class JigsawGenerator : MonoBehaviour
{
    public GameObject[] jigsawPieces;
    public float horizontalPieces;
    public float verticlPieces;
    private Vector3 startPoint;
    private float pieceIncrement;
    private float scaleFactor;

    private void Start()
    {
        scaleFactor = (2.66f / 0.78f) * jigsawPieces[0].transform.localScale.x; // 2.0748 divide by scale
        pieceIncrement = 1.12f;
        startPoint = new Vector3(-5, 2.5f, 0);
       //verticlPieces = 1.077f;
        GeneratePuzzle();
    }

    void GeneratePuzzle()
    {
        int horizontalIterations = Mathf.FloorToInt(horizontalPieces / 3); // important
        int verticalIterations = Mathf.FloorToInt(verticlPieces / 1.5f);
        //Need to figure out this calc
        //need to find constant factor to distance the let and right
        //maybe if statement for puzzles like 2x2
        
        //piece one
            Instantiate(jigsawPieces[0], startPoint , Quaternion.identity);
            Instantiate(jigsawPieces[0], startPoint + new Vector3( 1.072f * horizontalPieces,0.19f,0), Quaternion.Euler(0,0,-90)); // -90z  2.19y       2.2x
            Instantiate(jigsawPieces[0], startPoint + new Vector3(0.195f,(-6.43f/verticlPieces) * verticlPieces,0), Quaternion.Euler(0,0,90)); // 90z
            Instantiate(jigsawPieces[0], startPoint + new Vector3((6.62f/horizontalPieces) * horizontalPieces,(-6.24f/verticlPieces) * verticlPieces,0), Quaternion.Euler(0,0,180)); // 1800z
            
        //top n Bottom
        Vector3 pieceTwoStart = startPoint + new Vector3(1.32f, 0.2f, 0);
        Vector3 pieceThreeStart = startPoint + new Vector3(2.64f,0,0);
        Vector3 pieceThreeStartDown = startPoint + new Vector3(2.645f,0,0);
        

        for (int i = 0; i < horizontalIterations; i++)
        {
            Instantiate(jigsawPieces[1], pieceTwoStart + new Vector3(2.66f * i,0,0), Quaternion.identity);
            Instantiate(jigsawPieces[2], pieceThreeStart + new Vector3(2.66f * i,0,0), Quaternion.Euler(0,0,-90));
            Instantiate(jigsawPieces[1], pieceThreeStartDown + new Vector3(2.665f * i,(-6.44f/verticlPieces) * verticlPieces,0), Quaternion.identity).GetComponent<SpriteRenderer>().flipY = true;
            Instantiate(jigsawPieces[2], pieceTwoStart + new Vector3(2.66f * i,(-6.44f/verticlPieces) * verticlPieces,0), Quaternion.Euler(0,0,90)).GetComponent<SpriteRenderer>();
        }
        
        

        //Left and Right Side
        Vector3 leftPieceTwo = startPoint + new Vector3(-0.01f, -2.45f,0);
        Vector3 leftPieceThree = startPoint + new Vector3(0.19f, -1.13f,0);
        //Vector3 invLeftPieceTwo = startPoint + new Vector3(0, -1.13f,0);
        //Vector3 invLeftPieceThree = startPoint + new Vector3(0, -2.45f,0);
        
        
        for (int i = 0; i < horizontalIterations; i++) //fix horizontal and vertical calc
        {
            Instantiate(jigsawPieces[2], leftPieceThree + new Vector3(-0, -2.66f * i,0), Quaternion.identity);
            Instantiate(jigsawPieces[1], leftPieceTwo + new Vector3(-0, -2.66f * i,0), Quaternion.Euler(0,0,90));
            Instantiate(jigsawPieces[2], leftPieceTwo + new Vector3(1.075f * horizontalPieces, -2.66f * i ,0), Quaternion.Euler(0,0,180)); //6.45
            Instantiate(jigsawPieces[1], leftPieceThree + new Vector3(1.075f * horizontalPieces, -2.66f * i ,0), Quaternion.Euler(0,0,-90)); //6.45  need to fix the divisiion factor
        }
        
        
        //Middle pieces

        Vector3 midStart = startPoint + new Vector3(1.32f,-1.13f,0);
        Vector3 oddmidStart = startPoint + new Vector3(2.65f,-1.13f,0); //might need to adjust x
        Vector3 rotMidStart = startPoint + new Vector3(2.65f,-1.13f,0);
        Vector3 oddRotMidStart = startPoint + new Vector3(1.32f,-1.13f,0);

        for (int i = 0; i < verticalIterations; i++)
        {
            float yChange = -1.33f*i;

            if (i % 2 == 0)
            {
                for (int j = 0; j < horizontalIterations; j++)
                {
                    Instantiate(jigsawPieces[3], rotMidStart + new Vector3(2.66f *j, yChange,0), Quaternion.Euler(0,0,90));
                    Instantiate(jigsawPieces[3], midStart + new Vector3(2.66f *j, yChange,0), Quaternion.identity);
                }
                    
            }
            else if (i == verticalIterations-1)
            {
                for (int j = 0; j < horizontalIterations; j++)
                {
                    Instantiate(jigsawPieces[3], oddmidStart + new Vector3(2.66f * j - 0.005f, yChange, 0), Quaternion.identity);
                    Instantiate(jigsawPieces[3], oddRotMidStart + new Vector3(2.66f * j - 0.005f, yChange, 0), Quaternion.Euler(0, 0, 90));
                }
            } //could remove the whole else if no need for perfection
            else
            {
                for (int j = 0; j < horizontalIterations; j++)
                {
                    Instantiate(jigsawPieces[3], oddmidStart + new Vector3(2.66f * j, yChange, 0), Quaternion.identity);
                    Instantiate(jigsawPieces[3], oddRotMidStart + new Vector3(2.66f * j, yChange, 0), Quaternion.Euler(0, 0, 90));
                }
            }
        }
        
        
    }
    
}
