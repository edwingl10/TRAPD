using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject laserBeam;
    public SoundManager soundMan;

    public IEnumerator Shoot()
    {
        Instantiate(laserBeam, firePoint.position, firePoint.rotation);
        soundMan.Play("LaserShoot");
        yield return new WaitForSeconds(0.2f);
        soundMan.Play("LaserShoot");
        Instantiate(laserBeam, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(0.2f);
        soundMan.Play("LaserShoot");
        Instantiate(laserBeam, firePoint.position, firePoint.rotation);
    }
}
