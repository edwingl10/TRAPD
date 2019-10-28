using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
 

public class Player : MonoBehaviour
{
    //used for health bar
    public float StartingHealth;
    public float health;
    public GameObject HealthBar;
    private Transform bar;

	//references the power bar
	public float Powerxp; //xp needed to get your power up
	public float currentxp;
	public GameObject PowerBar;
	private Transform bar2;

    public levelManager levelMan;

    public GameObject powerButton;
    public bool powered;
    public bool canForceField;

    public int id;
    public Hashtable[] playerInfo;

    public SoundManager soundMan;

    public GameObject secondLifePanel;
    public int deaths;

    private UnityEngine.Object ReviveEffectRef;
    private UnityEngine.Object DieEffectRef;
    public GameObject canvas;

    private void Awake()
    {
        //GetComponent<DotzPower>().enabled = true;
        LoadPLayerData();
        GetComponent<SpriteRenderer>().sprite = GameAssets.i.GetCharacterSprite(id);
        GetComponent<Animator>().runtimeAnimatorController = GameAssets.i.controllers[id];
        LoadPower();
    }

    IEnumerator PlayerIntro()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<Rigidbody2D>().gravityScale = 3;
    }

    private void LoadPLayerData()
    {
        try
        {
            playerData data = saveSystem.LoadCharacterInfo();
            id = data.playerID;
            playerInfo = data.playerInfo;
        }
        catch (Exception e)
        {
            id = 0;
            playerInfo = GameAssets.i.playerInfo;
        }
    }

    private void LoadPower()
    {
        switch (id)
        {
            case 0:
                GetComponent<DotzPower>().enabled = true;
                break;
            case 1:
                GetComponent<MenderPower>().enabled = true;
                break;
            case 2:
                GetComponent<CocoPower>().enabled = true;
                break;
            case 3:
                GetComponent<VerglasPower>().enabled = true;
                break;
            case 4:
                GetComponent<DartPower>().enabled = true;
                break;
            case 5:
                GetComponent<QuashPower>().enabled = true;
                break;
            
        }
    }

    void Start()
    {
        StartCoroutine(PlayerIntro());
        deaths = 0;
        canForceField = false;
        powered = false;
        SetUpHealth();
        StartingHealth = health;
		currentxp = 0;
        bar = HealthBar.transform.Find("Bar");
		bar2 = PowerBar.transform.Find("Bar");
        PlayerPrefs.SetInt("adHp", 0);

        ReviveEffectRef = Resources.Load("ReviveEffect");
        DieEffectRef = Resources.Load("bulletExplosion");
    }

    public void TakeDamage(int Damage)
    {
        if (!canForceField)
        {
            GetComponent<Animator>().SetBool("damage",true);
            health -= Damage;
            if (health <= 0)
            {
                Die();
                health = 0;
            }
            SetHealthBarSize(health / StartingHealth);
        }
    }

    public void SetHealthBarSize(float percent)
    {
        bar.localScale = new Vector3(percent, 1f);
    }

    public void UpdatePowerupBar(float percent)
	{
		if (percent >= 1.0)
		{
			if (!powerButton.activeInHierarchy)
				soundMan.Play("PowerReady");

			currentxp = Powerxp;
			bar2.localScale = new Vector3(1f, 1f);
			powerButton.SetActive(true);
		}
        else if (percent <= 0)
        {
            bar2.localScale = new Vector3(0f, 1f);
            powerButton.SetActive(false);
        }
		else
		{
			bar2.localScale = new Vector3(percent, 1f);
		}
		
	}

    void Die()
    {
        GameObject dieEffect = (GameObject)Instantiate(DieEffectRef);
        dieEffect.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        Time.timeScale = 0.5f;
        if(deaths == 0)
        {
            levelMan.StopCounter();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY|RigidbodyConstraints2D.FreezeRotation;
            GetComponent<CircleCollider2D>().enabled = false;

            StartCoroutine(ShowSecondLifePanel());
        }
        else
        {
            levelMan.isGameOver = true;
            SaveInfo();
            Destroy(gameObject);
        }
    }

    IEnumerator ShowSecondLifePanel()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.3f);
        deaths++;
        secondLifePanel.SetActive(true);
    }

    public void RestoreLifeWrapper()
    {
        StartCoroutine(RestoreLife());
    }

    private IEnumerator RestoreLife()
    {
        health = 150;
        StartingHealth = 150;
        SetHealthBarSize(1f);

        secondLifePanel.SetActive(false);
        canvas.SetActive(false);

        GameObject ReviveEffect = (GameObject)Instantiate(ReviveEffectRef);
        ReviveEffect.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(3.4f);
        soundMan.Play("Heal");

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        GetComponent<CircleCollider2D>().enabled = true;
        canvas.SetActive(true);

        yield return new WaitForSeconds(1f);
        levelMan.ResumeCounter();
    }

    void SaveInfo()
    {
        int score = levelMan.scoreValue;
        if(score > (int)playerInfo[id]["highscore"])
        {
            levelMan.BestScoreWrapper();
            playerInfo[id]["highscore"] = score;
            saveSystem.saveCharacterInfo(this);
        }
        
    }

    //detects collision with coins
    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if (hitInfo.gameObject.CompareTag("yellowCoin"))
        {
            soundMan.Play("CoinCollect");
            levelMan.AddCoinsScore(10);
			currentxp += 8;
			UpdatePowerupBar(currentxp / Powerxp);

            PointsPopup.Create(gameObject.transform.position, "+10¢", Color.yellow);
            Destroy(hitInfo.gameObject);
        }
        else if (hitInfo.gameObject.CompareTag("redCoin"))
        {
            soundMan.Play("CoinCollect");
            levelMan.AddCoinsScore(30);
			currentxp += 15;
			UpdatePowerupBar(currentxp/Powerxp);

            PointsPopup.Create(gameObject.transform.position, "+30¢", Color.yellow);
            Destroy(hitInfo.gameObject);
        }
        
        else if (hitInfo.gameObject.CompareTag("blueCoin"))
		{
            soundMan.Play("PowerReady");
            levelMan.AddCoinsScore(50);
            currentxp = 100;
            if (currentxp >Powerxp)
            {
                currentxp = 100;
            }
			UpdatePowerupBar(currentxp/Powerxp);

            PointsPopup.Create(gameObject.transform.position, "+50¢", Color.yellow);
            Destroy(hitInfo.gameObject);
        }

        else if (hitInfo.gameObject.CompareTag("healthCoin"))
        {
            soundMan.Play("Heal");
            health += 25;
            if (health > StartingHealth)
            {
                health = StartingHealth;
            }
            SetHealthBarSize(health / StartingHealth);
            PointsPopup.Create(gameObject.transform.position, "+25hp", Color.green);
            Destroy(hitInfo.gameObject);
        }
       
    }

    public void DamageAnimTriggerFalse()
    {
        GetComponent<Animator>().SetBool("damage",false);
    }

    public void ActivatePower()
    {
        powered = true;
    }

    void SetUpHealth()
    {
        int adReward = PlayerPrefs.GetInt("adHp");
        if(adReward == 1)
        {
            health = 200;
        }
        else
        {
            health = 150;
        }
    }

    
}
