using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotzPower : MonoBehaviour
{
    private Player player;
    private float disappearTimer;
    private float CountdownInterval;
    public GameObject forceField;


    private void Start()
    {
        player = GetComponent<Player>();
        FetchAbilitylvl();
        disappearTimer = CountdownInterval; //Random.Range(0.8f,1f);
    }

    public void ActivatePowerup()
    {
        player.canForceField = true;
        forceField.SetActive(true);
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

    private void Update()
    {
        if (player.powered)
        {
            ActivatePowerup();
        }

        if (player.canForceField)
        {
            if(Time.time >= disappearTimer)
            {
                disappearTimer = Time.time + CountdownInterval; //Random.Range(0.5f,1f);
                player.currentxp -= 10;
                if (player.currentxp < 0f)
                {
                    player.currentxp = 0;
                    player.canForceField = false;
                    player.powered = false;
                    forceField.SetActive(false);
                }
                player.UpdatePowerupBar(player.currentxp / player.Powerxp);
            }
        }
    }

}
