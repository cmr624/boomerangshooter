using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour {

    Transform target;
    Vector3 initialPos;
    public float magnitudeX;
    public float magnitudeY;
	// Use this for initialization
	void Start () {
        target = GetComponent<Transform>();
        initialPos = target.localPosition;
	}

    float pendingShakeDuration = 0f;

    public void Shake(float duration)
    {
        if (duration > 0)
        {
            pendingShakeDuration += duration;
        }
    }

    bool isShaking = false;
	// Update is called once per frame
	void Update () {
		if (pendingShakeDuration > 0 && !isShaking)
        {
            //initialPos = target.position;
            StartCoroutine(DoShake(magnitudeX, magnitudeY));
        }
	}

    IEnumerator DoShake(float magnitudeX, float magnitudeY)
    {
        isShaking = true;
        var startTime = Time.realtimeSinceStartup;
        while(Time.realtimeSinceStartup < startTime + pendingShakeDuration)
        {
            float x = Random.Range(-1f, 1f) * magnitudeX;
            float y = Random.Range(-1f, 1f) * magnitudeY;
            var randomPoint = new Vector3(x, y);
            target.localPosition = randomPoint;
            yield return null;
        }

        pendingShakeDuration = 0f;
        target.localPosition = initialPos;
        isShaking = false;
    }
}
