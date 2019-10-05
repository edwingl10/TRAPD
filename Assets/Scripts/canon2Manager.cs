using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canon2Manager : MonoBehaviour
{
    public weapon2 wep;
    public levelManager levelman;
    float nextFire;
    public float min=10f;
    public float max=15f;
    

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        nextFire = Random.Range(min, max);
        InvokeRepeating("PlayChargedShot", nextFire, nextFire);
    }


    void PlayChargedShot()
    {
        anim.SetBool("shoot", true);
        nextFire = Random.Range(min, max);
        CancelInvoke("PlayChargedShot");
        if (!levelman.isGameOver)
        {
            InvokeRepeating("PlayChargedShot", nextFire, nextFire);
        }
    }

    void CallShoot()
    {
        anim.SetBool("shoot", false);
        wep.Shoot();
    }

}
