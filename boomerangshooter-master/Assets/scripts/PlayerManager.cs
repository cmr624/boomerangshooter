using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager Instance { get; private set; }

    public GameObject player;

    private void Start()
    {
        Instance = this;
    }



}
