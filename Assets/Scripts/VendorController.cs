using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VendorController : MonoBehaviour
{
    public float displayTime = 4;
    public GameObject dialogBox;

    float timerDisplay;

    public GameObject itemOne;
    public GameObject itemTwo;

    public AudioClip powerUpSound;

    public float powerDuration = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
            }
        }
    }

    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }

    public void SellItem()
    {
        RubyController player = GameObject.Find("Ruby").GetComponent<RubyController>();
        if (EventSystem.current.currentSelectedGameObject == itemOne)
        {
            player.powerUp = 2.0f;
            player.powerUpTimer = powerDuration;

            player.force = 600;

            player.invincibleTimer = 10.0f;
            player.isInvincible = true;

            player.GetComponent<SpriteRenderer>().color = Color.blue;
            player.PlaySound(powerUpSound);
        }
        if (EventSystem.current.currentSelectedGameObject == itemTwo)
        {
            player.maxHealth++;
        }

        GameObject healthUpObject = Instantiate(player.healthUpPrefab, player.GetComponent<Rigidbody2D>().position + Vector2.up * 0.5f, Quaternion.identity);
    }
}
