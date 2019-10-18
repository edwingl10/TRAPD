using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartPower : MonoBehaviour
{
    public playerMovement playermov;
    private Player player;
    private bool fast;
    private float disappearTimer;
    private float countDownInterval;
    public TrailRenderer speedTrail;
    private float fspeed;

    void Start()
    {
        player = GetComponent<Player>();
        fast = false;
        FetchAbilitylvl();
        disappearTimer = countDownInterval; //Random.Range(0.5f, 1f);
    }
    private void FetchAbilitylvl()
    {
        int lvl = (int)player.playerInfo[player.id]["upgradelvl"];
        switch (lvl)
        {
            case 0:
                countDownInterval = 0.5f;
                fspeed = 33f;
                break;
            case 1:
                countDownInterval = 0.8f;
                fspeed = 33f;
                break;
            case 2:
                countDownInterval = 1.0f;
                fspeed = 33f;
                break;
            case 3:
                countDownInterval = 1.2f;
                fspeed = 38f;
                break;
            case 4: countDownInterval = 1.5f;
                fspeed = 45f;
                break;
        }
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
                disappearTimer = Time.time + countDownInterval; //Random.Range(0.5f, 1f);
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
        playermov.runspeed = fspeed;
        speedTrail.enabled = true;
    }

    void RegularSpeed()
    {
        playermov.runspeed =28f;
        speedTrail.enabled = false;
    }
}
