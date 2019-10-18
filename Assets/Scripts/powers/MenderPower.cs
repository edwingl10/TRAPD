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
        FetchAbility();
    }

    private void FetchAbility()
    {
        int lvl = (int)player.playerInfo[player.id]["upgradelvl"];
        switch (lvl)
        {
            case 0:
                healthAmount = 25;
                usage = 1;
                break;
            case 1:
                healthAmount = 35;
                usage = 1;
                break;
            case 2:
                healthAmount = 60;
                usage = 1;
                break;
            case 3:
                healthAmount = 40;
                usage = 2;
                break;
            case 4:
                healthAmount = 50;
                usage = 2;
                break;
        }
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
