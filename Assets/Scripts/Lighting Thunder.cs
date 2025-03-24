using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingThunder : MonoBehaviour
{
    public Light lightningLight;
    public ParticleSystem lightningParticles;
    public float minFlashDelay = 2f;
    public float maxFlashDelay = 5f;
    public float flashDuration = 0.5f;

    private void Start()
    {
        StartCoroutine(FlashLightning());
    }

    private IEnumerator FlashLightning()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minFlashDelay, maxFlashDelay));

            // Play particles
            if (lightningParticles != null)
            {
                lightningParticles.Play();
            }

            // Turn on the light
            lightningLight.intensity = Random.Range(0.5f, 1f);

            // Wait for flash duration
            yield return new WaitForSeconds(flashDuration);

        }
    }

    }
