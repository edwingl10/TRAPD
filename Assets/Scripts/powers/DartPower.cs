using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartPower : MonoBehaviour
{
    public playerMovement playermov;
    private Player player;
    private bool fast;
    private float disappearTimer;
    public TrailRenderer speedTrail;

    void Start()
    {
        fast = false;
        disappearTimer = Random.Range(0.5f, 1f);
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (player.powered)
        {
            if (!fast)
            {
                SpeedPower();
            }

            if (Time.time >= disappearTimer)
            {
                disappearTimer = Time.time + Random.Range(0.5f, 1f);
                player.currentxp -= 10;
                if (player.currentxp < 0f)
                {
                    RegularSpeed();
                    player.currentxp = 0;
                    player.powered = false;
                    fast = false;
                }
                player.UpdatePowerupBar(player.currentxp / player.Powerxp);
            }
        }
    }


    void SpeedPower()
    {
        playermov.runspeed = 40f;
        speedTrail.enabled = true;
    }

    void RegularSpeed()
    {
        playermov.runspeed =28f;
        speedTrail.enabled = false;
    }
}
