using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour {

    public Vector3 Offset = new Vector3(0f, 2f, 0f);
    public Vector2 RandomizeIntensity = new Vector3(0f, 0f);
	// Use this for initialization
	void Start () {
        transform.localPosition += Offset;
        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x), Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y));
	}
}
