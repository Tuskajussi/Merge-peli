using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float spawnHeight;
    [SerializeField]
    private float spawnCooldown = 0.79f;
    private bool canSpawn = true;

    private GameObject instantioituObjekti;
    private GameObject vanhaObjekti;

    private Vector3 worldPos;

    public bool gameStarted = false;

    /*
     * Updatessa tarkistetaan onko peli alkanut, peli alkaa kun ensimmäisen kerran painetaan hiiren painiketta.
     * jos peli on alkanut napin painalluksella luodaan uusi objekti randomilla listasta.
     * mikäli pelaaja on jo luonut yhden kappaleen, tiputetaan se, aiempi instantioitu objekti seuraa hiirtä pelissä. (päivitetään joka framella positio)
     */
    void Update() 
    {
        if (gameStarted == true)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane; // Aseta z-arvo, jotta ScreenToWorldPoint toimii oikein
            worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.y = spawnHeight;
            instantioituObjekti.transform.position = worldPos;
        }
        if (Input.GetMouseButtonDown(0) && canSpawn == true)
        {
            StartCoroutine(SpawnCooldown());
            SpawnObject();
        }
    }

    private void SpawnObject() 
    {
        if (gameStarted == true)
        {
            GameController.instance.AddScore(10); //-- tää ei vielä toimi oppilailla
            vanhaObjekti = instantioituObjekti;
            vanhaObjekti.GetComponent<Rigidbody2D>().simulated = true;
            vanhaObjekti.GetComponent<CircleCollider2D>().enabled = true;
        }
        int randomBallIndex = Random.Range(0, 4);
        GameObject uusiObjekti = GameController.instance.balls[randomBallIndex];

        instantioituObjekti = Instantiate(uusiObjekti, worldPos, Quaternion.identity, GameController.instance.gameObject.transform);
        instantioituObjekti.GetComponent<Rigidbody2D>().simulated = false;
        instantioituObjekti.GetComponent<CircleCollider2D>().enabled = false;

        gameStarted = true;
    }

    private IEnumerator SpawnCooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
    }
}
