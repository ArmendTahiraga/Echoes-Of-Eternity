using UnityEngine;
using System.Collections;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float timeDelay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlickeringLights());
    }

    IEnumerator FlickeringLights()
    {
        while (true) // Infinite loop to continuously flicker the light
        {
            this.gameObject.GetComponent<Light>().enabled = false;
            timeDelay = Random.Range(0.01f, 0.2f);
            yield return new WaitForSeconds(timeDelay);
            this.gameObject.GetComponent<Light>().enabled = true;
            timeDelay = Random.Range(0.01f, 0.2f);
            yield return new WaitForSeconds(timeDelay);
        }
    }
}