using UnityEngine;
using Rowhouse;

public class VrDebugCanvasWristVisibility : MonoBehaviour
{
    public float minAngleToView = 22;
    public Hand hand = Hand.right;

    public RectTransform _content;
    public RectTransform _wrapper;

    private bool _visible;
    private float _targetScale = 0;

    void Start() {
        transform.localScale = Vector3.zero;

        //for (int i = 0; i < 500; i++) {
        //    VrDebugPlugin.VrDebug.Log("filler for debugging the debugger " + Time.time);
        //}
    }

    void Update() {
        var inViewingAngle = Vector3.Angle(-transform.up, Camera.main.transform.position - transform.position) < minAngleToView;
        var buttonsHeld = InputManager.I.Grip(hand) && InputManager.I.Trigger(hand);

        if (_visible && !buttonsHeld) {
            _visible = false;
        } else {
            if (InputManager.I.JoystickButtonDown(hand) && inViewingAngle && buttonsHeld) {
                _visible = true;
            }
        }

        _targetScale = _visible ? 1f : 0f;

        var y = InputManager.I.PrimaryAxis2D(hand).y;
        if (Mathf.Abs(y) > 0.15f) {
            var newY = _content.localPosition.y - Time.unscaledDeltaTime * 40f * y;
            // newY = Mathf.Clamp(newY, -_wrapper.sizeDelta.y, 0f);
            _content.localPosition = new Vector3(_content.localPosition.x, newY, _content.localPosition.z);
        }

        transform.localScale = Vector3.one * Mathf.Lerp(transform.localScale.x, _targetScale, Time.unscaledDeltaTime * 15f);
    }
}
