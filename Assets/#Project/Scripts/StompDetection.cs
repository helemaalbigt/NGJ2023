using System;
using UnityEngine;

public class StompDetection : MonoBehaviour {
    public event Action OnGotStomped;
    private AntController ant;

    private void Awake()
    {
        ant = GetComponent<AntController>();
    }
    public void Stomp() {
        ant.Spawn();
    }
}
