using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotzPower : MonoBehaviour
{
    private Player player;
    private float disappearTimer;
    public GameObject forceField;

    private void Start()
    {
        player = GetComponent<Player>();
        disappearTimer = Random.Range(0.5f,1f);
    }

    public void ActivatePowerup()
    {
        player.canForceField = true;
        forceField.SetActive(true);
    }

    private void Update()
    {
        if (player.canForceField)
        {
            if(Time.time >= disappearTimer)
            {
                disappearTimer = Time.time + Random.Range(0.5f,1f);
                player.currentxp -= 10;
                if (player.currentxp < 0f)
                {
                    player.currentxp = 0;
                    player.canForceField = false;
                    forceField.SetActive(false);
                }
                player.UpdatePowerupBar(player.currentxp / player.Powerxp);
            }
        }
    }

}
