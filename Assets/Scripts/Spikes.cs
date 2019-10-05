using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    Player player;

    private void Start()
    {
        StartCoroutine("EraseSpikes");
    }

    void KnockBack(Rigidbody2D pgb)
    {
        Vector3 moveDirection = transform.position - pgb.transform.position;
        //pgb.AddForce(moveDirection.normalized * -90f);
        pgb.AddForce( new Vector3(0f, moveDirection.normalized.y * -90f,0f));
    }

    IEnumerator EraseSpikes()
    {
        yield return new WaitForSeconds(8);
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Player>();
            if (!player.canForceField)
            {
                player.TakeDamage(1);
                KnockBack(collision.gameObject.GetComponent<Rigidbody2D>());
            }
        }
    }


}
