using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingThunder : MonoBehaviour
{
    public Light lightningLight;
    public ParticleSystem lightningParticles;
    public float minFlashDelay = 2f;
    public float maxFlashDelay = 5f;
    public float flashDuration = 0.1f;

    private void Start()
    {
        StartCoroutine(FlashLightning());
    }

    private IEnumerator FlashLightning()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minFlashDelay, maxFlashDelay));

            // Play the lightning effect
            if (lightningParticles != null)
            {
                lightningParticles.Play();
            }

            // Flash the light
            lightningLight.intensity = 5f; // Adjust intensity
            yield return new WaitForSeconds(flashDuration);
            lightningLight.intensity = 0f;
        }
    }
}
