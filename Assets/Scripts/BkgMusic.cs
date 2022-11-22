using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BkgMusic : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip victory;
    AudioClip loss;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        RubyController player = GameObject.Find("Ruby").GetComponent<RubyController>();
    }

    void Update()
    {
        if(player.currentHealth <= 0)
        {
            audioSource.clip = loss;
        }
    }
}
