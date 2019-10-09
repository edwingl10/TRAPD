using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenderPower : MonoBehaviour
{
    private Player player;
    private UnityEngine.Object healthEffectRef;

    private void Start()
    {
        player = GetComponent<Player>();
        healthEffectRef = Resources.Load("HealthEffect");
    }

    private void Update()
    {
        if(player.currentxp >= player.Powerxp / 2)
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
        player.health += 50;
        PointsPopup.Create(gameObject.transform.position, "+50hp", Color.green);
        GameObject healthEffect= (GameObject)Instantiate(healthEffectRef);
        healthEffect.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
        if (player.health >= player.StartingHealth)
        {
            player.health = player.StartingHealth;
        }
        player.SetHealthBarSize(player.health/player.StartingHealth);
        player.currentxp -= 50;
        if (player.currentxp < 0f)
        {
            player.currentxp = 0;
        }
        player.UpdatePowerupBar(player.currentxp/player.Powerxp);
        player.powered = false;
    }
}
