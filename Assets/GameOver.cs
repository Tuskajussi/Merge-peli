using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        FallingObject hit = collision.gameObject.GetComponent<FallingObject>();
        if (hit) // if-lauseke tarkistaa onko hit muuttujan sisällä objektia vai ei eli true / false
        {
            hit.enabled = false;
            hit.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            Time.timeScale = 0;
            Debug.Log("Game over");
        }
    }
}
