using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinDestroy : MonoBehaviour
{
   
    private void Awake()
    {
        //StartCoroutine(StartDestroy());
        Destroy(gameObject, 6f);
        
    }

    IEnumerator StartDestroy()
    {
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }
}
