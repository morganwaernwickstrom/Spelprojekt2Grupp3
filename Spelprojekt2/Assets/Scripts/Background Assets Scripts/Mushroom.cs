using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] private Animator myAnimator = null;
    [SerializeField] private ParticleSystem mySporeParticles = null;

    private bool myShake = false;
    private float mySporeTimer = 0f;
    [SerializeField] private float myMaxTime = 15f;

    private void Update()
    {
        myShake = false;

        if (!mySporeParticles.isPlaying)
        {
            mySporeTimer += Time.deltaTime;

            if (mySporeTimer >= myMaxTime)
            {
                myShake = true;
                mySporeParticles.Play();
                mySporeTimer = 0;
            }
        }

        myAnimator.SetBool("Shake", myShake);

    }
}
