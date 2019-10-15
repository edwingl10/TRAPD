using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocoPower : MonoBehaviour
{
    private Player player;
    private bool active;
    private float disappearTimer;
    private float CountdownInterval;

    public GameObject echoEffect;


    void Start()
    {
        player = GetComponent<Player>();
        active = false;
        CountdownInterval = 0.5f;
        disappearTimer = CountdownInterval; //Random.Range(0.5f, 1f);
    }

    void Update()
    {
        if (player.powered)
        {
            ShrinkPower();
            if (Time.time >= disappearTimer)
            {
                disappearTimer = Time.time + CountdownInterval; //Random.Range(0.5f, 1f);
                player.currentxp -= 10;
                if (player.currentxp < 0f)
                {
                    player.currentxp = 0;
                    player.powered = false;
                    EndPower();
                }
                player.UpdatePowerupBar(player.currentxp / player.Powerxp);
            }
        }
        else
        {
            active = false;
        }
    }

    private IEnumerator ShrinkEffect()
    {
        GameObject e1 = Instantiate(echoEffect, transform.position, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(0.1f);
        GameObject e2 = Instantiate(echoEffect, transform.position, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(0.1f);
        GameObject e3 = Instantiate(echoEffect, transform.position, Quaternion.identity) as GameObject;

        yield return new WaitForSeconds(1f);
        Destroy(e1);
        Destroy(e2);
        Destroy(e3);
    }

    private void ShrinkPower()
    {
        if (!active)
        {
            StartCoroutine(ShrinkEffect());

            transform.localScale += new Vector3(-0.1f, -0.1f, 0);
            active = true;
        }
        
    }

    private void EndPower()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0);
    }
}
