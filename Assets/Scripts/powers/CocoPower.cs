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
        //CountdownInterval = 0.5f;
        FetchAbilitylvl();
        disappearTimer = CountdownInterval; //Random.Range(0.5f, 1f);
    }
    private void FetchAbilitylvl()
    {
        int lvl = (int)player.playerInfo[player.id]["upgradelvl"];
        switch (lvl)
        {
            case 0:
                CountdownInterval = 0.5f;
                break;
            case 1:
                CountdownInterval = 0.8f;
                break;
            case 2:
                CountdownInterval = 1.0f;
                break;
            case 3:
                CountdownInterval = 1.2f;
                break;
            case 4:
                CountdownInterval = 1.5f;
                break;
        }
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
