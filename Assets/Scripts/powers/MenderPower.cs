using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenderPower : MonoBehaviour
{
    private Player player;
    private UnityEngine.Object healthEffectRef;
    private int healthAmount;
    private int usage;

    private void Start()
    {
        player = GetComponent<Player>();
        healthEffectRef = Resources.Load("HealthEffect");
        healthAmount = 25;
        usage = 1;
    }

    private void Update()
    {
        if(player.currentxp >= player.Powerxp / usage)
        {
            player.powerButton.SetActive(true);
        }
        else
        {
            player.powerButton.SetActive(false);
        }

        if (player.powered)
        {
            GiveHealth();
        }
    }

    public void GiveHealth()
    {
        player.health += healthAmount;
        PointsPopup.Create(gameObject.transform.position, "+"+healthAmount.ToString()+"hp", Color.green);
        GameObject healthEffect= (GameObject)Instantiate(healthEffectRef);
        healthEffect.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
        if (player.health >= player.StartingHealth)
        {
            player.health = player.StartingHealth;
        }
        player.SetHealthBarSize(player.health/player.StartingHealth);
        player.currentxp -= player.Powerxp/usage;
        if (player.currentxp < 0f)
        {
            player.currentxp = 0;
        }
        player.UpdatePowerupBar(player.currentxp/player.Powerxp);
        player.powered = false;
    }
}
