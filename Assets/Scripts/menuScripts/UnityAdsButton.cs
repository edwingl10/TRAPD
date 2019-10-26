using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Monetization;

[RequireComponent(typeof(Button))]
public class UnityAdsButton : MonoBehaviour{
    public GameObject successPanel;
    public GameObject errorPanel;

    public string placementId = "rewardedVideo";
    private Button adButton;

 
#if UNITY_IOS
   private string gameId = "3337101";
#elif UNITY_ANDROID
    private string gameId = "3337100";
#endif

    void Start()
    {
        adButton = GetComponent<Button>();
        if (adButton)
        {
            adButton.onClick.AddListener(ShowAd);
        }

        if (Monetization.isSupported)
        {
            Monetization.Initialize(gameId, true);
        }
    }

    void Update()
    {
        if (adButton)
        {
            adButton.interactable = Monetization.IsReady(placementId);
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
            successPanel.SetActive(true);
        }
        else if (result == ShowResult.Failed)
        {
            errorPanel.SetActive(true);
            Debug.LogError("Video failed to show");
        }
    }
}