using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Monetization;
using TMPro;

[RequireComponent(typeof(Button))]
public class doubleCoinsBttn : MonoBehaviour
{

    public string placementId = "rewardedVideo";
    private Button adButton;

#if UNITY_IOS
   private string gameId = "3337101";
#elif UNITY_ANDROID
    private string gameId = "3337100";
#endif

    public GameObject errorMsg;
    public levelManager levelMan;
    public TextMeshProUGUI coinsText;
    public SoundManager soundMan;
    private bool on;

    void Start()
    {
        adButton = GetComponent<Button>();
        on = true;
        if (adButton)
        {
            adButton.onClick.AddListener(ShowAd);
        }

        if (Monetization.isSupported)
        {
            Monetization.Initialize(gameId, false);
        }
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
            DoubleCoins();
        }
        else if (result == ShowResult.Failed)
        {
            errorMsg.SetActive(true);
            Debug.LogError("Video failed to show");
        }
    }

    void DoubleCoins()
    {
        soundMan.Play("CoinReward");
        coinsText.text = (levelMan.coinsValue * 2).ToString();
        gameData data = saveSystem.LoadGameData();
        saveSystem.SaveCoinData(levelMan.coinsValue + data.totalCoins);
        on = false;
    }

}