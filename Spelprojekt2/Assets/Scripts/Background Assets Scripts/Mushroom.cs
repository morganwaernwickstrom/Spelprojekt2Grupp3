using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private Animator myAnimator = null;
    [SerializeField] private ParticleSystem mySporeParticles = null;

    private float mySporeTimer = 0f;
    [SerializeField] private float myMaxTime = 15f;

    private void Start()
    {
        //myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!mySporeParticles.isPlaying)
        {
            mySporeTimer += Time.deltaTime;

            if (mySporeTimer >= myMaxTime)
            {
                //myAnimator.SetTrigger("Spores");
                mySporeParticles.Play();
                mySporeTimer = 0;
                Debug.LogError("PLAY");
            }
        }
    }
}
