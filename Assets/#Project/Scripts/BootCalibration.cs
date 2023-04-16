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

    [Header("LayerMasks")] 
    public LayerMask _calibMask;
    public LayerMask _gameMask;
    
    private bool _calibrating;
    private bool _triggersHeld;

    private void Start() {
        _calibrating = true;
        PlaceShoeOnAnchor();
        SetCalibrationView();
    }

    void Update()
    {
        if (InputManager.I.Trigger(Hand.right) && InputManager.I.Trigger(Hand.left) && !_calibrating && !_triggersHeld) {
            _calibrating = true;
            _triggersHeld = true;
            
            SetCalibrationView();
        }
        
        if (InputManager.I.Trigger(Hand.right) && InputManager.I.Trigger(Hand.left) && _calibrating && !_triggersHeld) {
            _calibrating = false;
            _triggersHeld = true;

            if (GameStateManager.Instance.currentState == GameStateManager.GameState.Waiting)
            {
                GameStateManager.Instance.SetGameState(GameStateManager.GameState.GameStarting);
            }

            SetGameView();
        }

        if (_calibrating) {
            PlaceShoeOnAnchor();
        }
        
        if(_triggersHeld && !InputManager.I.Trigger(Hand.right) && !InputManager.I.Trigger(Hand.left)) {
            _triggersHeld = false;
        }
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
        _mainCamera.cullingMask = _calibMask;
    }
    
    private void SetGameView() {
        _passthroughLayer.enabled = false;
        _mainCamera.clearFlags = CameraClearFlags.Skybox;
        _mainCamera.cullingMask = _gameMask;
    }
}
