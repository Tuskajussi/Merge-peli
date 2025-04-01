using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // haetaan hiiren sijainti
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane; // Aseta z-arvo, jotta ScreenToWorldPoint toimii oikein
            // muutetaan koordinaatti pelimaailman koordinaateiksi
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.y = 5;
                
            int randomBallIndex = Random.Range(0, 4);
            GameObject uusiObjekti = GameController.instance.balls[randomBallIndex];
            Instantiate(uusiObjekti, worldPos, Quaternion.identity, GameController.instance.gameObject.transform);
            GameController.instance.score += 10;
            //Debug.Log(GameController.instance.score);
        }
    }
}
