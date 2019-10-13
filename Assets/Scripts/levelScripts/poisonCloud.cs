using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poisonCloud : MonoBehaviour
{
    private float speed;
    private float direction;
    private bool hasSpawned;
    private bool up;
    private UnityEngine.Object cloudImpactRef;

    void Start()
    {
        cloudImpactRef = Resources.Load("CloudImpact");
        speed = 3f;
        hasSpawned = false;
        up = false;
        ApplyForce();
        StartCoroutine(WaitTimeBtwMovement());
        StartCoroutine(DeSpawn());
    }

    IEnumerator WaitTimeBtwMovement()
    {
        yield return new WaitForSeconds(1.3f);
        hasSpawned = true;
        InvokeRepeating("MoveUpAndDown", 0.6f, 0.6f);
        ApplyForce();
    }

    IEnumerator DeSpawn()
    {
        yield return new WaitForSeconds(12f);
        Destroy(gameObject);
    }

    void MoveUpAndDown()
    {
        up = !up;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject cloudImpact = (GameObject)Instantiate(cloudImpactRef);
            cloudImpact.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
            collision.gameObject.GetComponent<poisonManager>().TogglePoisonDamage();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("boundary"))
        {
            direction *= -1;
          
        }
        
    }

    void ApplyForce()
    {
        direction = Random.Range(0, 2) * 2 - 1;

    }


    private void FixedUpdate()
    {
        if (hasSpawned)
        {
            
            if(direction <0)
            {
                transform.Translate(-Vector2.right * Time.deltaTime*speed);

            }
            else
            {
                transform.Translate(Vector2.right * Time.deltaTime*speed);
            }

            if (up)
            {
                transform.Translate(Vector2.up * Time.deltaTime * 0.8f);
            }
            else
            {
                transform.Translate(Vector2.down * Time.deltaTime * 0.8f);
            }
            
        }
        
    }

}
