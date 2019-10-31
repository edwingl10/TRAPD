using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Monetization;


[RequireComponent(typeof(Button))]
public class PlusHpButton : MonoBehaviour
{

    public string placementId = "rewardedVideo";
    private Button adButton;

#if UNITY_IOS
   private string gameId = "3337101";
#elif UNITY_ANDROID
    private string gameId = "3337100";
#endif

    public GameObject errorPanel;
    private bool on;
    private UnityEngine.Object healthEffectRef;
    public GameObject player;
    public SoundManager soundMan;

    void Start()
    {
        healthEffectRef = Resources.Load("HealthEffect");
        adButton = GetComponent<Button>();
        if (adButton)
        {
            adButton.onClick.AddListener(ShowAd);
        }

        if (Monetization.isSupported)
        {
            Monetization.Initialize(gameId, false);
        }
        on = true;
    }

    void Update()
    {
        if (adButton && on)
        {
            adButton.interactable = Monetization.IsReady(placementId);
        }
        else
        {
            adButton.interactable = false;
        }
    }

    void ShowAd()
    {
        ShowAdCallbacks options = new ShowAdCallbacks();
        options.finishCallback = HandleShowResult;
        ShowAdPlacementContent ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;
        ad.Show(options);
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            GiveHealthEffect();
            PlayerPrefs.SetInt("adHp",1);
            on = false;
        }
        else if (result == ShowResult.Failed)
        {
            errorPanel.SetActive(false);
        }
    }

    private void GiveHealthEffect()
    {
        soundMan.Play("Heal");
        GameObject healthEffect = (GameObject)Instantiate(healthEffectRef);
        healthEffect.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + .3f, player.transform.position.z);
    }
}