using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public SoundManager soundMan;

    public void Shoot(){
        soundMan.Play("BulletShot");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
