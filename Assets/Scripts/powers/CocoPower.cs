using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocoPower : MonoBehaviour
{
    private Player player;
    private bool active;
    private float disappearTimer;

    void Start()
    {
        player = GetComponent<Player>();
        active = false;
        disappearTimer = Random.Range(0.5f, 1f);
    }

    void Update()
    {
        if (player.powered)
        {
            ShrinkPower();
            if (Time.time >= disappearTimer)
            {
                disappearTimer = Time.time + Random.Range(0.5f, 1f);
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

    private void ShrinkPower()
    {
        if (!active)
        {
            transform.localScale += new Vector3(-0.1f, -0.1f, 0);
            active = true;
        }
        
    }

    private void EndPower()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0);
    }
}
