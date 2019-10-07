using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class Player : MonoBehaviour
{
    //used for health bar
    private float StartingHealth;
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
    public bool canForceField;

	void Start()
    {
        canForceField = false;
        StartingHealth = health;
		currentxp = 0;
        bar = HealthBar.transform.Find("Bar");
		bar2 = PowerBar.transform.Find("Bar");
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
        Time.timeScale = 0.5f;
        levelMan.isGameOver = true;
        Destroy(gameObject);
    }


    //detects collision with coins
    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if (hitInfo.gameObject.CompareTag("yellowCoin"))
        {
            levelMan.AddCoinsScore(10);
			currentxp += 8;
			UpdatePowerupBar(currentxp / Powerxp);

            PointsPopup.Create(gameObject.transform.position, "+10");
            Destroy(hitInfo.gameObject);
        }
        else if (hitInfo.gameObject.CompareTag("redCoin"))
        {
            levelMan.AddCoinsScore(30);
			currentxp += 15;
			UpdatePowerupBar(currentxp/Powerxp);

            PointsPopup.Create(gameObject.transform.position, "+30");
            Destroy(hitInfo.gameObject);
        }
        
        else if (hitInfo.gameObject.CompareTag("blueCoin"))
		{
			levelMan.AddCoinsScore(50);
            currentxp = 100;
            if (currentxp >Powerxp)
            {
                currentxp = 100;
            }
			UpdatePowerupBar(currentxp/Powerxp);

            PointsPopup.Create(gameObject.transform.position, "+50");
            Destroy(hitInfo.gameObject);
        }

        else if (hitInfo.gameObject.CompareTag("healthCoin"))
        {
            health += 25;
            if (health > StartingHealth)
            {
                health = StartingHealth;
            }
            SetHealthBarSize(health / StartingHealth);
            PointsPopup.Create(gameObject.transform.position, "+25");
            Destroy(hitInfo.gameObject);
        }
       
    }

    public void DamageAnimTriggerFalse()
    {
        GetComponent<Animator>().SetBool("damage",false);
    }

}
