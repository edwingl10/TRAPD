using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector3 m_Velocity = Vector3.zero;
    public float speed;
    private Animator anim;
    private bool m_FacingRight = true;

    private UnityEngine.Object ExplosionRef;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 0;
        anim = GetComponent<Animator>();
        StartCoroutine(Despawn());
        ExplosionRef = Resources.Load("Explosion");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(30);
            Destroy(gameObject);

            GameObject bulletExplosion = (GameObject)Instantiate(ExplosionRef);
            bulletExplosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
        }
        else if (collision.gameObject.CompareTag("boundary"))
        {
            speed *= -1;
        }
        else if(collision.gameObject.CompareTag("block") || collision.gameObject.CompareTag("ground"))
        {
            ApplyForce();
        }


        anim.SetFloat("speed", Mathf.Abs(speed));

        // If the input is moving the player right and the player is facing left...
        if (speed > 0 && !m_FacingRight)
        {
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (speed < 0 && m_FacingRight)
        {
            Flip();
        }
   
    }

    void ApplyForce()
    {
        speed = Random.Range(0,2) *2-1;
        
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector2(speed *3f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, .05f);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
        GameObject bulletExplosion = (GameObject)Instantiate(ExplosionRef);
        bulletExplosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
    }
}
