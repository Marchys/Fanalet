using UnityEngine;

[RequireComponent(typeof(Light))]
public class CandleLight : MonoBehaviour
{
    public float minIntensity = 0.25f; public float maxIntensity = 0.5f;
    private Light ownLight;

    float random;

    void Start()
    {
        random = Random.Range(0.0f, 65535.0f);
        ownLight = GetComponent<Light>();
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(random, Time.time);
        ownLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}