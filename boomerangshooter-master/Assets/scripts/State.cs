using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour {

	public bool state;

    private void OnDestroy()
    {
        ShootManager.Instance.allShots.Remove(this.gameObject);
    }
}
