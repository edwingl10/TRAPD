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
        CountdownInterval = 0.5f;
        disappearTimer = CountdownInterval; //Random.Range(0.8f,1f);
    }

    public void ActivatePowerup()
    {
        player.canForceField = true;
        forceField.SetActive(true);
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
