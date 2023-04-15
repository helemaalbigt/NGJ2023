using System;
using System.Collections;
using System.Collections.Generic;
using Rowhouse;
using UnityEngine;
using UnityEngine.Serialization;

public class BootCalibration : MonoBehaviour
{
    [Header("Anchors")]
    public Transform _shoeLeft;
    public Transform _shoeRight;
    public Transform _shoeLeftAnchor;
    public Transform _shoeRightAnchor;
    
    [Header("Passthrough")]
    public OVRPassthroughLayer _passthroughLayer;
    public Camera _mainCamera;
    public GameObject _calibrationWrapper;
    public GameObject _gameWrapper;

    private bool _calibrating;
    private bool _triggersHeld;

    private void Start() {
#if UNITY_ANDROID
        _calibrating = true;
        PlaceShoeOnAnchor();
        SetCalibrationView();
#else
        _shoeLeft.gameObject.SetActive(true);
        _shoeRight.gameObject.SetActive(true);
#endif
    }

    void Update()
    {
#if UNITY_ANDROID
        if (InputManager.I.Trigger(Hand.right) && InputManager.I.Trigger(Hand.left) && !_calibrating && !_triggersHeld) {
            _calibrating = true;
            _triggersHeld = true;
            
            SetCalibrationView();
        }
        
        if (InputManager.I.Trigger(Hand.right) && InputManager.I.Trigger(Hand.left) && _calibrating && !_triggersHeld) {
            _calibrating = false;
            _triggersHeld = true;
            
            SetGameView();
        }

        if (_calibrating) {
            PlaceShoeOnAnchor();
        }
        
        if(_triggersHeld && !InputManager.I.Trigger(Hand.right) && !InputManager.I.Trigger(Hand.left)) {
            _triggersHeld = false;
        }
#endif
    }

    private void PlaceShoeOnAnchor() {
        _shoeLeft.position = _shoeLeftAnchor.position;
        _shoeLeft.rotation = _shoeLeftAnchor.rotation;
        _shoeRight.position = _shoeRightAnchor.position;
        _shoeRight.rotation = _shoeRightAnchor.rotation;
    }

    private void SetCalibrationView() {
        _passthroughLayer.enabled = true;
        _mainCamera.clearFlags = CameraClearFlags.SolidColor;
        _calibrationWrapper.SetActive(true);
        _gameWrapper.SetActive(false);
    }
    
    private void SetGameView() {
        _passthroughLayer.enabled = false;
        _mainCamera.clearFlags = CameraClearFlags.Skybox;
        _calibrationWrapper.SetActive(false);
        _gameWrapper.SetActive(true);
    }
}
