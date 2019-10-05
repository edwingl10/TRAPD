using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used to check if player is touching the ground
// used character controller to check if player is touching the real ground
public class GroundisLava : MonoBehaviour
{
    public Player player;
    public bool isLava;
    private Animator lavaAnim;
    

    void Start()
    {
        isLava = false;
        lavaAnim = GetComponent<Animator>();
    }

    public void StartLavaAnim()
    {
        lavaAnim.SetBool("lava", true);
      
    }

    public void changeLava()
    {
        isLava = !isLava;
        if (isLava)
        {
            StartCoroutine("StopLava"); 
        }
    }

    IEnumerator StopLava()
    {
        yield return new WaitForSeconds(8);
        changeLava();
        lavaAnim.SetBool("lava", false);
    }

    public void checkIfDamage()
    {
        if (isLava)
        {
            player.TakeDamage(1);
        }
    }

}
