using UnityEngine;
using Rewired;
using UnityEngine.Rendering;
using UnityEngine.UI;
//UnityEngine.Apple.TV.Remote.allowExitToHome is false to enable menu button

public class PointerRemote : MonoBehaviour
{
    public float pointerSpeed;
    private GameManager GM;
    private Player player;
    private GameObject currentObject;
    private bool holdingObject;
    private bool menuActive;
    void Start()
    {
        UnityEngine.tvOS.Remote.allowExitToHome = false;
        GM = FindObjectOfType<GameManager>();
        player = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        Collider2D[] nearbyObjects = Physics2D.OverlapPointAll(transform.position);

        if (GM.winPanel.activeSelf || GM.UIpanel.activeSelf)
            menuActive = true;
        else
            menuActive = false;

        if(!menuActive)
            MovePointer();

        if (nearbyObjects.Length >= 1 && !holdingObject && player.GetButtonDown("Touch Click"))
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
                //currentObject.GetComponent<SpriteRenderer>().color = Color.grey;
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

    private void MovePointer()
    {
        Vector2 WorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        Vector3 direction = new Vector3(player.GetAxis("Drag Horizontal"), player.GetAxis("Drag Vertical"), 1).normalized;
        Vector2 clampedPosition = new Vector2(Mathf.Clamp(transform.position.x, -WorldPosition.x, WorldPosition.x),
            Mathf.Clamp(transform.position.y, -WorldPosition.y, WorldPosition.y));
        
        
        transform.position = clampedPosition ;
        transform.position += direction * Time.deltaTime * pointerSpeed;
        //rb.MovePosition(  transform.position + direction * Time.deltaTime * pointerSpeed); //maybe problem fix needed auto move
        
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

    // private void RespawnObject(GameObject obj)
    // {
    //     if (Random.Range(1, 101) < 50)
    //         obj.transform.position = new Vector3(Random.Range(5f, 8f), Random.Range(-3.0f, 3.0f), GM.zIndex);
    //     
    //     else
    //         obj.transform.position = new Vector3(Random.Range(-5f, -8f), Random.Range(-3.0f, 2.0f), GM.zIndex);
    // }
}
