using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuashPower : MonoBehaviour
{
    public GameObject gun;
    private Player player;
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float disappearTimer;
    private float countDownInterval;
    private float shootInterval;

    private bool power;


    private void Start()
    {
        player = GetComponent<Player>();
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
                shootInterval = 0.6f;
                break;
            case 1:
                countDownInterval = 0.8f;
                shootInterval = 0.5f;
                break;
            case 2:
                countDownInterval = 1.0f;
                shootInterval = 0.4f;
                break;
            case 3:
                countDownInterval = 1.2f;
                shootInterval = 0.3f;
                break;
            case 4:
                countDownInterval = 1.5f;
                shootInterval = 0.2f;
                break;
        }
    }
    private void Update()
    {
        if (player.powered)
        {
            if (!power)
            {
                power = true;
                gun.SetActive(true);
                InvokeRepeating("StartShootPower", shootInterval, shootInterval);
            }
            

            if (Time.time >= disappearTimer)
            {
                disappearTimer = Time.time + countDownInterval; //Random.Range(0.5f, 1f);
                player.currentxp -= 10;
                if (player.currentxp < 0f)
                {
                    StopShootPower();
                    player.currentxp = 0;
                    player.powered = false;
                    power = false;
                }
                player.UpdatePowerupBar(player.currentxp / player.Powerxp);
            }
        }

    }


    void StartShootPower()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void StopShootPower()
    {
        CancelInvoke("StartShootPower");
        gun.SetActive(false);
    }
}
