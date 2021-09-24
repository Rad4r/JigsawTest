using UnityEngine;
using Rewired;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PointerRemote : MonoBehaviour
{
    private GameManager GM;
    private Player player;
    private GameObject currentObject;
    public bool holdingObject;
    private bool menuActive;
    private bool movable;
    private Vector3 pointerOffset;
    private bool buttonClickCheckable;
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        player = ReInput.players.GetPlayer(0);
        Invoke("ButtonToCheckable", 2f);
    }

    private void Update()
    {
        Collider2D[] nearbyObjects = Physics2D.OverlapPointAll(transform.position);

        if (GM.winPanel.activeSelf || GM.UIpanel.activeSelf)
            menuActive = true;
        else
            menuActive = false;

        if(!menuActive)
            TouchMove();

        if (buttonClickCheckable && nearbyObjects.Length >= 1 && !holdingObject && player.GetButtonDown("Touch Click"))
        {
            if (ClosestObject(nearbyObjects).CompareTag("OpenPiece"))
            {
                GetComponent<SpriteRenderer>().enabled = false;
                currentObject = ClosestObject(nearbyObjects).gameObject;
                currentObject.GetComponent<SpriteRenderer>().color = Color.white;
                currentObject.GetComponent<SortingGroup>().sortingOrder = GM.zIndex++;
                GM.PickUpSoundPlay();
                holdingObject = true;
            }
            else if (ClosestObject(nearbyObjects).CompareTag("Pause"))
                ClosestObject(nearbyObjects).GetComponent<Button>().onClick.Invoke();
            
        }

        if (holdingObject)
        {
            if (player.GetButtonDown("Back Button") || player.GetButtonDown("Menu Button"))
            {
                GetComponent<SpriteRenderer>().enabled = true;
                currentObject.GetComponent<SpriteRenderer>().color = Color.gray;
                holdingObject = false;
            }
            else if(currentObject.CompareTag("OpenPiece"))
                currentObject.transform.position = transform.position;
            else
            {
                holdingObject = false;
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else
        {
            if (player.GetButtonDown("Back Button") || player.GetButtonDown("Menu Button"))
            {
                if(GM.UIpanel.activeSelf)
                    GM.UIpanel.SetActive(false);
                else
                    GM.UIpanel.SetActive(true);
            }
        }
        
    }

    private void TouchMove()
    {
        PointerClamp();
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                pointerOffset = transform.position;
                GM.PickUpSoundPlay();
            }
            if (touch.phase == TouchPhase.Moved)
                transform.position = new Vector3(touchPosition.x, touchPosition.y,0) + pointerOffset;
        }
    }

    private void PointerClamp()
    {
        Vector2 WorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        Vector2 clampedPosition = new Vector2(Mathf.Clamp(transform.position.x, -WorldPosition.x, WorldPosition.x),
            Mathf.Clamp(transform.position.y, -WorldPosition.y, WorldPosition.y));
        transform.position = clampedPosition ;
    }

    private Collider2D ClosestObject(Collider2D[] collisionList)
    {
        float highestValue = 0;
        Collider2D closestObj = collisionList[0];
        
        for (int i = 0; i < collisionList.Length; i++)
        {
            if (collisionList[i].transform.position.z >= highestValue)
            {
                highestValue = collisionList[i].transform.position.z;
                closestObj = collisionList[i];
            }
                
        }

        return closestObj;
    }

    private void ButtonToCheckable()
    {
        buttonClickCheckable = true;
    }

    // private void RespawnObject(GameObject obj)
    // {
    //     if (Random.Range(1, 101) < 50)
    //         obj.transform.position = new Vector3(Random.Range(5f, 8f), Random.Range(-3.0f, 3.0f), GM.zIndex);
    //     
    //     else
    //         obj.transform.position = new Vector3(Random.Range(-5f, -8f), Random.Range(-3.0f, 2.0f), GM.zIndex);
    // }
}
