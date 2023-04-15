using System;
using UnityEngine;

public class StompDetection : MonoBehaviour {
    public event Action OnGotStomped;

    public void Stomp() {
        Debug.Log("Got stomped!");
        OnGotStomped?.Invoke();
    }
}
