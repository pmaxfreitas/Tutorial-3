using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendorController : MonoBehaviour
{
    public float displayTime = 4;
    public GameObject dialogBox;

    float timerDisplay;

    public Button itemOne;

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
        if (itemOne.tag == "Cogs")
        {
        player.cogsValue += 10;
        player.cogs.text = "Cogs: " + player.cogsValue.ToString();
        }
    }
}
