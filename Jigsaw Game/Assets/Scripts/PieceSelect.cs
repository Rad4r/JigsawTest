using UnityEngine;
using Rewired;

public class PieceSelect : MonoBehaviour
{
    public GridScript[] Grids;
    private GameObject currentObject;
    private int currentIndex;
    private int currentGrid;
    private Player player;
    private bool changeReset;
    private bool pieceSelected;
    private Vector3 targetPosition;
    private GameManager GM;
    public Transform arrowMovement;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        player = ReInput.players.GetPlayer(0);
        Invoke("ObjectSet", 2f);
    }

    private void Update()
    {
        PieceChange();
        //Invoke("PieceChange", 2f);
        if (player.GetButtonDown("Menu Button"))
        {
            pieceSelected = false;
            currentObject.transform.position = Grids[currentGrid].GetPiecePosition(currentIndex);
        }

    }
    
    // void DelayedUpdate()
    // {
    //     PieceChange();
    //     //CurrentPieceUIUpdate();
    // }

    void PieceChange()
    {
        //Implement other grid and also add an arrow pointer
        //When click you can rag around an arrow
        
        // remove from list and disable that item
        //maybe a list of banned numbers
        
        if (changeReset && !pieceSelected && !GM.UIpanel.activeSelf)
        {
            if (player.GetButtonDown("Touch Click"))
            {
                pieceSelected = true;
            }

            if (player.GetAxis("Drag Horizontal") > 0 && currentIndex % 2 == 0)
            {
                if(currentObject.CompareTag("OpenPiece"))
                    currentObject.GetComponent<SpriteRenderer>().color = Color.gray;
                currentIndex++;
                currentObject = Grids[currentGrid].GetPiece(currentIndex);
                CurrentPieceUIUpdate();
                changeReset = false;
                Invoke("ButtonChangeReset",0.2f);
            }
            else if (player.GetAxis("Drag Horizontal") < 0 && currentIndex % 2 != 0)
            {
                if(currentObject.CompareTag("OpenPiece"))
                    currentObject.GetComponent<SpriteRenderer>().color = Color.gray;
                currentIndex--;
                currentObject = Grids[currentGrid].GetPiece(currentIndex);
                CurrentPieceUIUpdate();
                changeReset = false;
                Invoke("ButtonChangeReset",0.2f);
            }
            else if (player.GetAxis("Drag Vertical") < 0 && currentIndex < Grids[currentGrid].maxPieces)
            {
                if(currentObject.CompareTag("OpenPiece"))
                    currentObject.GetComponent<SpriteRenderer>().color = Color.gray;
                currentIndex+=2;
                currentObject = Grids[currentGrid].GetPiece(currentIndex);
                CurrentPieceUIUpdate();
                changeReset = false;
                Invoke("ButtonChangeReset",0.2f);
            }
            else if (player.GetAxis("Drag Vertical") > 0 && currentIndex > 1)
            {
                if(currentObject.CompareTag("OpenPiece"))
                    currentObject.GetComponent<SpriteRenderer>().color = Color.gray;
                currentIndex-=2;
                currentObject = Grids[currentGrid].GetPiece(currentIndex);
                CurrentPieceUIUpdate();
                changeReset = false;
                Invoke("ButtonChangeReset",0.2f);
            }
            
            if (player.GetButtonDown("Back Button") || player.GetButtonDown("Menu Button"))
            {
                if (GM.UIpanel.activeSelf)
                    GM.UIpanel.SetActive(false);
                else
                    GM.UIpanel.SetActive(true);
                    
            }
        }
        else if (pieceSelected)
        {
            arrowMovement.transform.position = currentObject.transform.position;
            Vector2 playerInput = new Vector2(player.GetAxis("Drag Horizontal"),player.GetAxis("Drag Vertical"));
            //float zAngle = Vector2.Angle(Vector2.up, playerInput);
            //float zAngle = Mathf.Atan2(playerInput.y, playerInput.x) * Mathf.Rad2Deg;
            //Debug.Log(zAngle);
            //arrowMovement.rotation = quaternion.Euler(0,0,zAngle);

            if (currentObject.CompareTag("OpenPiece"))
            {
                 if (playerInput.x == 0 && playerInput.y == 0) { // this statement allows it to recenter once the inputs are at zero 
                    Vector3 curRot = arrowMovement.transform.localEulerAngles; // the object you are rotating
                    Vector3 homeRot;
                    if (curRot.z > 180f) { // this section determines the direction it returns home 
                        homeRot = new Vector3 (0f,0f, 359.999f); //it doesnt return to perfect zero 
                    } else {                                                                      // otherwise it rotates wrong direction 
                        homeRot = Vector3.zero;
                    }
                    arrowMovement.transform.localEulerAngles = Vector3.Slerp (curRot, homeRot, Time.deltaTime*2);
                } 
                 else {
                    arrowMovement.transform.localEulerAngles = new Vector3 (0f, 0f, -Mathf.Atan2 (playerInput.x, playerInput.y) * 180 / Mathf.PI); // this does the actual rotaion according to inputs
                }
                
                arrowMovement.localScale = new Vector2(30, playerInput.magnitude * 1000) ; //use input to extend the shape
                targetPosition = arrowMovement.up * 0.01f + arrowMovement.up * 0.35f;
                
                if (player.GetButtonDown("Touch Click"))
                {
                    float scaleFactor = arrowMovement.localScale.y * 0.01f;
                    Vector2 offsetfactor = arrowMovement.up * 0.35f;
                    targetPosition = arrowMovement.position + arrowMovement.up * scaleFactor + (Vector3) offsetfactor;
                    //Instantiate(arrowMovement.gameObject, targetPosition, quaternion.identity);
                    //Debug.Log("Target Position is " + targetPosition);
                    currentObject.transform.position = targetPosition;
                    //pieceSelected = false;
                    //currentObject = 0;
                    //Invoke to previous position andshowing that the wrong position and reseting everything
                    //Maybe add lerp animation
                }
                else if (player.GetButtonDown("Back Button") || player.GetButtonDown("Menu Button"))
                {
                    pieceSelected = false;
                    arrowMovement.position = new Vector3(50, 50);
                    currentObject.transform.position = Grids[currentGrid].GetPiecePosition(currentIndex);
                }   
            }
            else
            {
                pieceSelected = false;
                arrowMovement.position = new Vector3(50, 50);
            }
        }
        
        //Debug.Log(pieceSelected);
    }
    
    void ObjectSet()
    {
        currentGrid = 0;
        currentIndex = 0;
        currentObject = Grids[currentGrid].GetPiece(currentIndex);
        currentObject.GetComponent<SpriteRenderer>().color = Color.white;
        changeReset = true;
    }

    void CurrentPieceUIUpdate()
    {
        if(currentObject.CompareTag("OpenPiece"))
            currentObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    void ButtonChangeReset()
    {
        changeReset = true;
    }
    
    // private void TouchMove()
    // {
    //     if(Input.touchCount > 0)
    //     {
    //         Touch touch = Input.GetTouch(0);
    //         Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
    //
    //         if (touch.phase == TouchPhase.Began)
    //         {
    //             Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
    //             if(GetComponent<Collider2D>() == touchedCollider)
    //             {
    //                 movable = true;
    //                 sortGroup.sortingOrder = GM.zIndex++;
    //                 GM.PickUpSoundPlay();
    //             }
    //         }
    //         if (touch.phase == TouchPhase.Moved && movable)
    //             transform.position = new Vector2(touchPosition.x, touchPosition.y);
    //         
    //         if (touch.phase == TouchPhase.Ended)
    //             movable = false;
    //     }
    // }
    
    // void Twist ()
    // {
    //     h1 = CrossPlatformInputManager.GetAxis("Horizontal"); // set as your inputs 
    //     v1 = CrossPlatformInputManager.GetAxis("Vertical");
    //     if (h1 == 0f && v1 == 0f) { // this statement allows it to recenter once the inputs are at zero 
    //         Vector3 curRot = twistPoint.transform.localEulerAngles; // the object you are rotating
    //         Vector3 homeRot;
    //         if (curRot.y > 180f) { // this section determines the direction it returns home 
    //             Debug.Log (curRot.y);
    //             homeRot = new Vector3 (0f,359.999f, 0f); //it doesnt return to perfect zero 
    //         } else {                                                                      // otherwise it rotates wrong direction 
    //             homeRot = Vector3.zero;
    //         }
    //         twistPoint.transform.localEulerAngles = Vector3.Slerp (curRot, homeRot, Time.deltaTime*2);
    //     } else {
    //         twistPoint.transform.localEulerAngles = new Vector3 (0f, Mathf.Atan2 (h1, v1) * 180 / Mathf.PI, 0f); // this does the actual rotaion according to inputs
    //     }
    // }
}
