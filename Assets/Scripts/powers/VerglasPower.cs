using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerglasPower : MonoBehaviour
{
    public bullet bulletRef;
    public bullet bullet2Ref;
    public laserBeam laserBeamRef;
    
    private Player player;
    private float disappearTimer;
    private float countDownInterval;
    private bool freeze;
    private float fspeed;

    public ParticleSystem freezeEffect;
    public Animator freezeAnim;


    private void Start()
    {
        player = GetComponent<Player>();
        FetchAbilitylvl();
        disappearTimer = countDownInterval; //Random.Range(0.5f,1f);
        freeze = false;
    }

    private void FetchAbilitylvl()
    {
        int lvl = (int)player.playerInfo[player.id]["upgradelvl"];
        switch (lvl)
        {
            case 0:
                countDownInterval = 0.5f;
                fspeed = 10f;
                break;
            case 1:
                countDownInterval = 0.8f;
                fspeed = 10f;
                break;
            case 2:
                countDownInterval = 0.8f;
                fspeed = 8f;
                break;
            case 3:
                countDownInterval = 1.0f;
                fspeed = 8f;
                break;
            case 4:
                countDownInterval = 1.5f;
                fspeed = 5f;
                break;
        }
    }
    private void SlowBullets()
    {
        freeze = true;
        freezeAnim.SetBool("freeze", freeze);
        freezeEffect.Play();
        bulletRef.speed = fspeed;
        bullet2Ref.speed = fspeed;
        laserBeamRef.speed = fspeed;
        
    }
    private void StopPower()
    {
        bulletRef.speed = 15f;
        bullet2Ref.speed = 15f;
        laserBeamRef.speed = 20f;
        freezeEffect.Stop();
    }

    private void Update()
    {

        if (player.powered)
        {
            if (!freeze)
            {
                SlowBullets();
            }
            

            if (Time.time >= disappearTimer)
            {
                disappearTimer = Time.time + countDownInterval;
                player.currentxp -= 10;
                if (player.currentxp < 0f)
                {
                    StopPower();
                    player.currentxp = 0;
                    player.powered = false;
                    freeze = false;
                    freezeAnim.SetBool("freeze", freeze);
                }
                player.UpdatePowerupBar(player.currentxp / player.Powerxp);
            }
        }
    }
}
