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
        //for (int i = 1; i <= 2; i++)
        
        //piece one
            Instantiate(jigsawPieces[0], startPoint , Quaternion.identity);
            Instantiate(jigsawPieces[0], startPoint + new Vector3( 1.072f * horizontalPieces,0.19f,0), Quaternion.Euler(0,0,-90)); // -90z  2.19y       2.2x
            Instantiate(jigsawPieces[0], startPoint + new Vector3(0.195f,(-6.43f/verticlPieces) * verticlPieces,0), Quaternion.Euler(0,0,90)); // 90z
            Instantiate(jigsawPieces[0], startPoint + new Vector3((6.62f/horizontalPieces) * horizontalPieces,(-6.24f/verticlPieces) * verticlPieces,0), Quaternion.Euler(0,0,180)); // 1800z
            
            //0.62f
        //piece two top (could sprite flip)
        Vector3 pieceTwoStart = startPoint + new Vector3(1.32f, 0.2f, 0);
        Instantiate(jigsawPieces[1], pieceTwoStart, Quaternion.identity);
        Instantiate(jigsawPieces[1], pieceTwoStart + new Vector3(2.66f,0,0), Quaternion.identity); //2.66 from each
        
        //piece three top
        Vector3 pieceThreeStart = startPoint + new Vector3(2.64f,0,0);
        Instantiate(jigsawPieces[2], pieceThreeStart, Quaternion.Euler(0,0,-90));
        Instantiate(jigsawPieces[2], pieceThreeStart + new Vector3(2.66f,0,0), Quaternion.Euler(0,0,-90));
        
        //piece DownFlip
        Instantiate(jigsawPieces[1], pieceThreeStart + new Vector3(0.005f,(-6.44f/verticlPieces) * verticlPieces,0), Quaternion.identity).GetComponent<SpriteRenderer>().flipY = true;
        Instantiate(jigsawPieces[1], pieceThreeStart + new Vector3(2.665f,(-6.44f/verticlPieces) * verticlPieces,0), Quaternion.identity).GetComponent<SpriteRenderer>().flipY = true; //2.66 from each

        Instantiate(jigsawPieces[2], pieceTwoStart + new Vector3(0.005f,(-6.44f/verticlPieces) * verticlPieces,0), Quaternion.Euler(0,0,90)).GetComponent<SpriteRenderer>();
        Instantiate(jigsawPieces[2], pieceTwoStart + new Vector3(2.66f,(-6.44f/verticlPieces) * verticlPieces ,0), Quaternion.Euler(0,0,90)).GetComponent<SpriteRenderer>();
        
        //LeftSide
        Vector3 leftPieceTwo = startPoint + new Vector3(-0.01f, -2.45f,0);
        Vector3 leftPieceThree = startPoint + new Vector3(0.19f, -1.13f,0);
        Instantiate(jigsawPieces[2], leftPieceThree, Quaternion.identity);
        Instantiate(jigsawPieces[2], leftPieceThree + new Vector3(-0, -2.66f,0), Quaternion.identity);
        
        Instantiate(jigsawPieces[1], leftPieceTwo, Quaternion.Euler(0,0,90));
        Instantiate(jigsawPieces[1], leftPieceTwo + new Vector3(-0, -2.66f,0), Quaternion.Euler(0,0,90));
        
        //rightSide
        Vector3 invLeftPieceTwo = startPoint + new Vector3(0, -1.13f,0);
        Vector3 invLeftPieceThree = startPoint + new Vector3(0, -2.45f,0);
        Instantiate(jigsawPieces[2], invLeftPieceThree + new Vector3((6.43f/horizontalPieces) * horizontalPieces,0,0), Quaternion.Euler(0,0,180));
        Instantiate(jigsawPieces[2], invLeftPieceThree + new Vector3((6.43f/horizontalPieces) * horizontalPieces, -2.66f,0), Quaternion.Euler(0,0,180));
        
        Instantiate(jigsawPieces[1], invLeftPieceTwo + new Vector3((6.63f/horizontalPieces) * horizontalPieces,0,0), Quaternion.Euler(0,0,-90));
        Instantiate(jigsawPieces[1], invLeftPieceTwo + new Vector3((6.63f/horizontalPieces) * horizontalPieces, -2.66f,0), Quaternion.Euler(0,0,-90));
        
        //Middle pieces

        Vector3 midStart = startPoint + new Vector3(1.32f,-1.13f,0);
        Vector3 oddmidStart = startPoint + new Vector3(2.65f,-1.13f,0); //might need to adjust x
        Vector3 rotMidStart = startPoint + new Vector3(2.65f,-1.13f,0);
        Vector3 oddRotMidStart = startPoint + new Vector3(1.32f,-1.13f,0);
        int horizontalIterations = Mathf.FloorToInt(horizontalPieces / 3);
        int verticalIterations = Mathf.FloorToInt(verticlPieces / 1.5f);
        
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
