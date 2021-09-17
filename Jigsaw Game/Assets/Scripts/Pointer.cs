using UnityEngine;
using Random = UnityEngine.Random;
using Rewired;
using UnityEngine.Rendering;

public class Pointer : MonoBehaviour
{
    public float pointerSpeed;
    private GameManager GM;
    private Rigidbody2D rb;
    private GameObject currentObject;
    private bool holdingObject;
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(Screen.width);
    }

    private void Update()
    {
        MovePointer();

        Collider2D[] nearbyObjects = Physics2D.OverlapPointAll(transform.position);

        foreach (var obj in nearbyObjects)
        {
            Debug.Log(obj.name);
        }

        if (nearbyObjects.Length >= 2 && !holdingObject && Input.GetMouseButtonDown(0))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            currentObject = ClosestObject(nearbyObjects).gameObject;
            currentObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            currentObject.GetComponent<SortingGroup>().sortingOrder = GM.zIndex++;
            GM.PickUpSoundPlay();
            holdingObject = true;
        }
        
        if (holdingObject)
        {
            if (Input.GetKey(KeyCode.Backspace))
            {
                GetComponent<SpriteRenderer>().enabled = true;
                currentObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 110);
                RespawnObject(currentObject);
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
    }

    private void MovePointer()
    {
        Vector2 WorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 1).normalized;
        Vector2 clampedPosition = new Vector2(Mathf.Clamp(transform.position.x, -WorldPosition.x, WorldPosition.x),
            Mathf.Clamp(transform.position.y, -WorldPosition.y, WorldPosition.y));
        
        rb.MovePosition(  transform.position + direction*Time.deltaTime*pointerSpeed);
        transform.position = clampedPosition ;
    }

    private Collider2D ClosestObject(Collider2D[] collisionList)
    {
        float highestValue = 0;
        Collider2D closestObj = collisionList[1];
        
        for (int i = 1; i < collisionList.Length; i++)
        {
            if (collisionList[i].transform.position.z >= highestValue)
            {
                highestValue = collisionList[i].transform.position.z;
                closestObj = collisionList[i];
            }
                
        }

        return closestObj;
    }

    private void RespawnObject(GameObject obj)
    {
        if (Random.Range(1, 101) < 50)
            obj.transform.position = new Vector3(Random.Range(5f, 8f), Random.Range(-3.0f, 3.0f), GM.zIndex);

        else
            obj.transform.position = new Vector3(Random.Range(-5f, -8f), Random.Range(-3.0f, 2.0f), GM.zIndex);
    }

}
