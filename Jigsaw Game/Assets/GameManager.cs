using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform[] pieces;
    public Transform sideBar;
    void Start()
    {
        foreach (var piece in pieces)
        {
            piece.position = sideBar.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-3.0f, 3.0f), 0);
            //piece.position = Vector3.Lerp(piece.position,sideBar.position + new Vector3(0, Random.Range(-5.0f, 5.0f), 0), 0.125f);
        }
    }

}
