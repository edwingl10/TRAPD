using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    Player player;
    private bool spikeDamage;

    private void Start()
    {
        StartCoroutine("EraseSpikes");
        spikeDamage = false;
    }

    public void PlaySpikeSound()
    {
        FindObjectOfType<SoundManager>().Play("SpikeSpawn");
    }

    void KnockBack(Rigidbody2D pgb)
    {
        Vector3 moveDirection = transform.position - pgb.transform.position;
        pgb.AddForce( new Vector3(moveDirection.normalized.x * -90f,moveDirection.normalized.y * -90f,0f));
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

            if(!player.canForceField)
                KnockBack(collision.gameObject.GetComponent<Rigidbody2D>());

            if (!spikeDamage)
            {
                if (!player.canForceField)
                {
                    StartCoroutine(SpikeDamage());
                    player.TakeDamage(15);
                }
            }

        }
    }

    public IEnumerator SpikeDamage()
    {
        spikeDamage = true;
        FindObjectOfType<SoundManager>().Play("SpikePierce");
        yield return new WaitForSeconds(0.2f);
        spikeDamage = false;
    }

}
