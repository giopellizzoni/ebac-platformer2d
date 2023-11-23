using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{

    public GameObject endGameCanvas;
    public string tagToCompare = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.CompareTag(tagToCompare)) 
        {
            endGameCanvas.SetActive(true);
        }
        
    }

}
