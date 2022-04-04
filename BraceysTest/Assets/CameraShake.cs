using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    
    public float range = 1f;

    private Vector3 originalPos;

    public void OnEnable()
    {
        originalPos = new Vector3(0, 0, 0);
    }
    public IEnumerator Shake(float duration, float magnitude)
    {

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-range, range) * magnitude;
            float y = Random.Range(-range, range) * magnitude;
            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }

}
