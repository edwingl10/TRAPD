using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed; //set in level manager
    public Rigidbody2D rb;
    //references bullet particle 
    private UnityEngine.Object bulletExplosionRef;
    public int damage;
    
    void Start()
    { 
        //rb.velocity = transform.right * speed;
        bulletExplosionRef = Resources.Load("bulletExplosion");
    }

    void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo){

        if(hitInfo.gameObject.CompareTag("Player")){
            hitInfo.GetComponent<Player>().TakeDamage(damage);
        }
        //if (tagName != "yellowCoin" && tagName !="redCoin" && tagName!= "blueCoin" && tagName!= "healthCoin")
        if(hitInfo.gameObject.CompareTag("Player") || hitInfo.gameObject.CompareTag("block") || hitInfo.gameObject.CompareTag("boundary"))
        {
            GameObject bulletExplosion = (GameObject)Instantiate(bulletExplosionRef);
            bulletExplosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
            Destroy(gameObject);
        }
        
    }
}
