using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserManager : MonoBehaviour
{
    public laserWeapon laserwep;
    public levelManager levelman;
    float nextFire;
    public float min = 7f;
    public float max = 10f;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        nextFire = Random.Range(min, max);
        InvokeRepeating("PlayLaserBeam", nextFire, nextFire);
    }

    void PlayLaserBeam()
    {
        anim.SetBool("shoot", true);
        nextFire = Random.Range(min, max);
        CancelInvoke("PlayLaserBeam");
        if (!levelman.isGameOver)
        {
            InvokeRepeating("PlayLaserBeam", nextFire, nextFire);
        }
    }

    void CallShoot()
    {
        anim.SetBool("shoot", false);
        StartCoroutine(laserwep.Shoot());
    }

}

