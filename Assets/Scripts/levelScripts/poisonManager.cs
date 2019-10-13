using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poisonManager : MonoBehaviour
{
    public ParticleSystem poisonedEffect;
    Player player;
    private int poisoncounter;
    

    private void Start()
    {
        player = GetComponent<Player>();
        poisoncounter = 0;
    }

    public void TogglePoisonDamage()
    {
        poisoncounter = 0;
        poisonedEffect.Play();
        InvokeRepeating("PoisonDamage", 1f, 2f);
    }

    void PoisonDamage()
    {
        if (poisoncounter == 2)
        {
            poisonedEffect.Stop();
            CancelInvoke("PoisonDamage");
            poisoncounter = 0;
        }
        poisoncounter++;
        player.TakeDamage(20);
    }

    private void Update()
    {
        if (player.canForceField)
        {
            poisonedEffect.Stop();
            CancelInvoke("PoisonDamage");
        }
    }
    /**
    public void TogglePoisonDamage(int Damage)
    {
        if (!player.canForceField)
        {
            //poisoned = true;
            poisonedEffect.Play();
            StartCoroutine(PoisonDamage(Damage));
        }

    }
    public IEnumerator PoisonDamage(int Damage)
    {
        Debug.Log("poison damage 1");
        player.TakeDamage(Damage);
        yield return new WaitForSeconds(2f);

        Debug.Log("poison damage 2");
        player.TakeDamage(Damage);
        yield return new WaitForSeconds(2f);

        Debug.Log("poison damage 3");
        player.TakeDamage(Damage);
        //poisoned = false;
        //poisonedEffect.Stop();
    }**/


}
