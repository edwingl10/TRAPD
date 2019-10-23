using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBeam : MonoBehaviour
{
    public float speed; //set in level manager
    public Rigidbody2D rb;
    //references laser impact particle 
    private UnityEngine.Object laserExplosionRef;
    public int damage;

    void Start()
    {
        //rb.velocity = transform.right * speed;
        laserExplosionRef = Resources.Load("laserImpact");
    }

    void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if (hitInfo.gameObject.CompareTag("Player"))
        {
			if (!hitInfo.GetComponent<Player>().canForceField)
				FindObjectOfType<SoundManager>().Play("BulletHit");

			hitInfo.GetComponent<Player>().TakeDamage(damage);
        }
        if (hitInfo.gameObject.CompareTag("Player") || hitInfo.gameObject.CompareTag("block") || hitInfo.gameObject.CompareTag("boundary"))
        {
			
			GameObject bulletExplosion = (GameObject)Instantiate(laserExplosionRef);
            bulletExplosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
            Destroy(gameObject);
        }

    }
}
