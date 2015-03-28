using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviourEx, IHandle<CameraShakeMessage>
{

    private float magnitude;
    private float duration;


    IEnumerator Shake()
    {
        float elapsed = 0.0f;

        Vector3 originalCamPos = transform.position;

        while (elapsed < duration)
        {
            originalCamPos = transform.position;
            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            y *= magnitude * damper;

            transform.position = new Vector3(x + originalCamPos.x, y + originalCamPos.y, originalCamPos.z);

            yield return null;
        }

        transform.position = originalCamPos;
    }


    public void Handle(CameraShakeMessage message)
    {
        magnitude = message.Magnitude;
        duration = message.Duration;
        StartCoroutine(Shake());
    }
}