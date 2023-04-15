using System;
using System.Collections;
using System.Collections.Generic;
using Rowhouse;
using UnityEngine;
using VrDebugPlugin;

public class BootStomper : MonoBehaviour
{
    private VectorQueue _prevDirections = new VectorQueue(5);
    private Vector3 _lastPosition;

    private void OnEnable() {
        _lastPosition = transform.position;
    }

    private void OnDisable() {
        _prevDirections.Clear();
    }

    private void Update() {
        var direction = transform.position - _lastPosition;
        _prevDirections.Enqueue(direction);
        _lastPosition = transform.position;
    }

    public void OnTriggerEnter(Collider collision){
        var isAnt = collision.gameObject.layer == LayerMask.NameToLayer("Ant");
        //var footIsGoingDown = Vector3.Dot(_prevDirections.AverageDirection(), Vector3.up) < 0;
        if(isAnt){
           var stompDetection = collision.gameObject.GetComponent<StompDetection>();
           stompDetection.Stomp();
       }
    }
}
