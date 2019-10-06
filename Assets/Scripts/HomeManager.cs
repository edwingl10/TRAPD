using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public Animator llight;
    public Animator mlight;
    public Animator rlight;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startSunlightAnimation());
    }

    IEnumerator startSunlightAnimation()
    {
        yield return new WaitForSeconds(5);
        llight.Play("sunlight");
        yield return new WaitForSeconds(0.3f);
        mlight.Play("sunlight");
        yield return new WaitForSeconds(0.3f);
        rlight.Play("sunlight");
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("BoxScene");
    }
}
