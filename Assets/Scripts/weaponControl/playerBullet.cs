using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    public float speed; 
    public Rigidbody2D rb;
    //references bullet particle 
    private UnityEngine.Object bulletExplosionRef;

    void Start()
    {
        rb.velocity = transform.right * speed;
        bulletExplosionRef = Resources.Load("laserImpact");
    }


    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if (hitInfo.gameObject.CompareTag("bullet"))
        {
            Destroy(hitInfo.gameObject);
        }
        
        if (hitInfo.gameObject.CompareTag("bullet") || hitInfo.gameObject.CompareTag("block") || hitInfo.gameObject.CompareTag("boundary"))
        {
            GameObject bulletExplosion = (GameObject)Instantiate(bulletExplosionRef);
            bulletExplosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
            Destroy(gameObject);
        }

    }
}
