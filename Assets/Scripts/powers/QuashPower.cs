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

    private bool power;


    private void Start()
    {
        player = GetComponent<Player>();
        disappearTimer = Random.Range(0.5f, 1f);
    }

    private void Update()
    {
        if (player.powered)
        {
            if (!power)
            {
                power = true;
                gun.SetActive(true);
                InvokeRepeating("StartShootPower", 0.5f, 0.5f);
            }
            

            if (Time.time >= disappearTimer)
            {
                disappearTimer = Time.time + Random.Range(0.5f, 1f);
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
