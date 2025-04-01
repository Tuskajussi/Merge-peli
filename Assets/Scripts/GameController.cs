using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] balls;
    public int score = 0;

    public static GameController instance;
    private void Awake()
    {
        instance = this;
    }
    
  public void RegisterCollision(GameObject a, GameObject b)
    {
        Vector3 spawnPosition = (a.transform.position + b.transform.position) / 2;
        int size = a.GetComponent<FallingObject>().size;
        score += (size * 100) * 2;
        //Debug.Log(score);
        Instantiate(instance.balls[size], spawnPosition, Quaternion.identity, this.gameObject.transform);
    }
}
