using UnityEngine;
using UnityEngine.Serialization;

namespace Rowhouse
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager I;

        public bool _initOnAwake;
        public OculusInput _oculusInput;
        public MockInput _mockInput;

        private InputSource _input;

        #region SETUP

        public void Awake() {
            if (_initOnAwake)
                Setup();
        }

        public void Setup() {
            if (I == null)
                I = this;
            
            _input.enabled = true;
        }

        #endregion

        #region INPUT

        public bool MenuPressed(Hand hand) {
            return GetInput().MenuPressed(hand);
        }

        public bool MenuPressed() {
            return GetInput().MenuPressed();
        }

        public bool MenuDown(Hand hand) {
            return GetInput().MenuDown(hand);
        }

        public bool MenuDown() {
            return GetInput().MenuDown();
        }

        public bool PrimaryButton(Hand hand) {
            return GetInput().PrimaryButton(hand);
        }

        public bool PrimaryButtonDown(Hand hand) {
            return GetInput().PrimaryButtonDown(hand);
        }

        public bool PrimaryButtonTouch(Hand hand) {
            return GetInput().PrimaryButtonTouch(hand);
        }

        public bool SecondaryButton(Hand hand) {
            return GetInput().SecondaryButton(hand);
        }

        public bool SecondaryButtonDown(Hand hand) {
            return GetInput().SecondaryButtonDown(hand);
        }

        public bool SecondaryButtonTouch(Hand hand) {
            return GetInput().SecondaryButtonTouch(hand);
        }

        public bool Trigger(Hand hand) {
            return GetInput().Trigger(hand);
        }

        public float TriggerAxis(Hand hand) {
            return GetInput().TriggerAxis(hand);
        }

        public bool TriggerDown(Hand hand) {
            return GetInput().TriggerDown(hand);
        }

        public bool TriggerUp(Hand hand) {
            return GetInput().TriggerUp(hand);
        }

        public bool Grip(Hand hand) {
            return GetInput().Grip(hand);
        }

        public float GripAxis(Hand hand) {
            return GetInput().GripAxis(hand);
        }

        public bool GripDown(Hand hand) {
            return GetInput().GripDown(hand);
        }

        public bool GripUp(Hand hand) {
            return GetInput().GripUp(hand);
        }

        public bool TwoHandGrip() {
            return GetInput().TwoHandGrip();
        }

        public Vector2 PrimaryAxis2D(Hand hand) {
            return GetInput().PrimaryAxis2D(hand);
        }

        public bool DebugHold(Hand hand) {
            return GetInput().DebugHold(hand);
        }

        public bool DebugDown(Hand hand) {
            return GetInput().DebugDown(hand);
        }

        public bool JoystickButton(Hand hand) {
            return GetInput().JoystickButton(hand);
        }

        public bool JoystickButtonDown(Hand hand) {
            return GetInput().JoystickButtonDown(hand);
        }

        private InputSource GetInput() {
            if (I == null)
                I = this;

            _input = _oculusInput;

            _input.enabled = true;
            return _input;
        }

        #endregion
    }
}