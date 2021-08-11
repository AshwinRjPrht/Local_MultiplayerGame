using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthController : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public SpriteRenderer[] heartDisplay;
    public Sprite heartFull, heartEmpty;

    public Transform heartHolder;

    public float invincibilityTime, HealthFlashTime =.2f;
    private float invincCounter, flashCounter;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if(flashCounter < 0)
            {
                flashCounter = HealthFlashTime;

                heartHolder.gameObject.SetActive(!heartHolder.gameObject.activeInHierarchy);
            }
            if(invincCounter <= 0)
            {
                heartHolder.gameObject.SetActive(true);
            }
        }
    }

    private void LateUpdate()
    {
        heartHolder.localScale = transform.localScale;
    }



    public void UpdateHealthDisplay()
    {
        switch (currentHealth)
        {
            case 3:
                heartDisplay[0].sprite = heartFull;
                heartDisplay[1].sprite = heartFull;
                heartDisplay[2].sprite = heartFull;
                break;

            case 2:
                heartDisplay[0].sprite = heartFull;
                heartDisplay[1].sprite = heartFull;
                heartDisplay[2].sprite = heartEmpty;
                break;

            case 1:
                heartDisplay[0].sprite = heartFull;
                heartDisplay[1].sprite = heartEmpty;
                heartDisplay[2].sprite = heartEmpty;
                break;

            case 0:
                heartDisplay[0].sprite = heartEmpty;
                heartDisplay[1].sprite = heartEmpty;
                heartDisplay[2].sprite = heartEmpty;
                break;
        }
    }
    public void DamagePlayer(int DamageToTake)
    {
        if (invincCounter <= 0)
        {
            currentHealth -= DamageToTake;

            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
            UpdateHealthDisplay();

            if (currentHealth == 0)
            {
                gameObject.SetActive(false);
            }

            invincCounter = invincibilityTime;
        }
    }
    public void FillHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthDisplay();

        flashCounter = 0;
        invincCounter = 0;

        heartHolder.gameObject.SetActive(true);
    }
}
