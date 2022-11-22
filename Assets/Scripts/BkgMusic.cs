using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BkgMusic : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip victory;
    public AudioClip loss;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        RubyController player = GameObject.Find("Ruby").GetComponent<RubyController>();

        if(player.gameOverBool == true)
        {
            if(player.currentHealth <= 0)
            {
                audioSource.clip = loss;
            }
            
            if(player.scoreValue >= 1)
            {
                audioSource.clip = victory;
            }

            audioSource.Play();
        }
    }
}
