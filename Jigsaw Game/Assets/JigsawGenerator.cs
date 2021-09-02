using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawGenerator : MonoBehaviour
{
    public GameObject[] jigsawPieces;
    public int horizontalPieces;
    public int verticlPieces;
    private Vector3 startPoint;
    private float pieceIncrement;

    private void Start()
    {
        pieceIncrement = 1.12f;
        startPoint = new Vector3(-6, 2, 0);
        GeneratePuzzle();
    }

    void GeneratePuzzle()
    {
        //for (int i = 1; i <= 2; i++)
        
        //piece one
            Instantiate(jigsawPieces[0], startPoint , Quaternion.identity);
            Instantiate(jigsawPieces[0], startPoint + new Vector3( 1.072f * horizontalPieces,0.19f,0), Quaternion.Euler(0,0,-90)); // -90z  2.19y       2.2x
            Instantiate(jigsawPieces[0], startPoint + new Vector3(0.2f,-0.93f * verticlPieces - 0.19f,0), Quaternion.Euler(0,0,90)); // 90z
            Instantiate(jigsawPieces[0], startPoint + new Vector3((1.12f * horizontalPieces) + 0.2f,-0.93f * verticlPieces,0), Quaternion.Euler(0,0,180)); // 1800z
            
        //piece two top (could sprite flip)
        Vector3 pieceTwoStart = startPoint + new Vector3(1.32f, 0.2f, 0);
        Instantiate(jigsawPieces[1], pieceTwoStart, Quaternion.identity);
        Instantiate(jigsawPieces[1], pieceTwoStart + new Vector3(2.66f,0,0), Quaternion.identity); //2.66 from each
        
        //piece three top
        Vector3 pieceThreeStart = startPoint + new Vector3(2.64f,0,0);
        Instantiate(jigsawPieces[2], pieceThreeStart, Quaternion.Euler(0,0,-90));
        Instantiate(jigsawPieces[2], pieceThreeStart + new Vector3(2.66f,0,0), Quaternion.Euler(0,0,-90));
        
        //piece two down (could sprite flip)
        //Vector3 pieceTwoStart = startPoint + new Vector3(1.32f, 0.2f, 0);
        Instantiate(jigsawPieces[1], pieceThreeStart + new Vector3(0,-0.93f * verticlPieces,0), Quaternion.identity).GetComponent<SpriteRenderer>().flipY = true;
        Instantiate(jigsawPieces[1], pieceThreeStart + new Vector3(2.66f,-0.93f * verticlPieces,0), Quaternion.identity).GetComponent<SpriteRenderer>().flipY = true; //2.66 from each
        
        //piece three down
        //Vector3 pieceThreeStart = startPoint + new Vector3(2.64f,0.01f,0);
        Instantiate(jigsawPieces[2], pieceTwoStart + new Vector3(0,-0.93f * verticlPieces,0), Quaternion.Euler(0,0,90)).GetComponent<SpriteRenderer>();
        Instantiate(jigsawPieces[2], pieceTwoStart + new Vector3(2.65f,-0.93f * verticlPieces ,0), Quaternion.Euler(0,0,90)).GetComponent<SpriteRenderer>();
    }
}
