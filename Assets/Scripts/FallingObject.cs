using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public int size;
    
    public bool hasMerged = false;

    public void Update()
    {
        if(hasMerged)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(gameObject.tag) && !hasMerged)
        {
            hasMerged = true;
            collision.gameObject.GetComponent<FallingObject>().hasMerged = true;
            GameController.instance.RegisterCollision(gameObject, collision.gameObject);
        }
    }
}
