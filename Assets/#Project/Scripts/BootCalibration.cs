using System.Collections;
using System.Collections.Generic;
using Rowhouse;
using UnityEngine;

public class BootCalibration : MonoBehaviour
{
    public bool _triggerPressed;
    
    void Update()
    {
        _triggerPressed = InputManager.I.Trigger(Hand.right);
    }
}
