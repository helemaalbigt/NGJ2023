using UnityEngine;

namespace Rowhouse
{
    public class OculusInput : InputSource
    {
        private const OVRInput.Controller _left = OVRInput.Controller.LTouch;
        private const OVRInput.Controller _right = OVRInput.Controller.RTouch;

        private const OVRInput.Button _grab = OVRInput.Button.PrimaryIndexTrigger;
        private const OVRInput.Axis1D _grabAxis = OVRInput.Axis1D.PrimaryIndexTrigger;
        private const OVRInput.Axis1D _gripAxis = OVRInput.Axis1D.PrimaryHandTrigger;
        private const OVRInput.Button _gripButton = OVRInput.Button.PrimaryHandTrigger;
        private const OVRInput.RawButton _primaryLeft = OVRInput.RawButton.X;
        private const OVRInput.RawButton _primaryRight = OVRInput.RawButton.A;
        private const OVRInput.RawTouch _primaryLeftTouch = OVRInput.RawTouch.X;
        private const OVRInput.RawTouch _primaryRightTouch = OVRInput.RawTouch.A;
        private const OVRInput.RawButton _secondaryLeft = OVRInput.RawButton.Y;
        private const OVRInput.RawButton _secondaryRight = OVRInput.RawButton.B;
        private const OVRInput.RawTouch _secondaryLeftTouch = OVRInput.RawTouch.Y;
        private const OVRInput.RawTouch _secondaryRightTouch = OVRInput.RawTouch.B;
        private const OVRInput.RawButton _openMenuAlt = OVRInput.RawButton.Start;
        private const OVRInput.RawButton _repositionLeft = OVRInput.RawButton.LThumbstick;
        private const OVRInput.RawButton _repositionRight = OVRInput.RawButton.RThumbstick;

        public override bool MenuPressed() {
            return OVRInput.Get(_openMenuAlt);
        }

        public override bool MenuDown() {
            return OVRInput.GetDown(_openMenuAlt);
        }

        public override bool PrimaryButton(Hand hand) {
            OVRInput.RawButton button = hand == Hand.left ? _primaryLeft : _primaryRight;
            return OVRInput.Get(button, GetOculusHand(hand));
        }

        public override bool PrimaryButtonDown(Hand hand) {
            OVRInput.RawButton button = hand == Hand.left ? _primaryLeft : _primaryRight;
            return OVRInput.GetDown(button, GetOculusHand(hand));
        }

        public override bool PrimaryButtonTouch(Hand hand) {
            OVRInput.RawTouch button = hand == Hand.left ? _primaryLeftTouch : _primaryRightTouch;
            return OVRInput.Get(button, GetOculusHand(hand));
        }

        public override bool SecondaryButton(Hand hand) {
            OVRInput.RawButton button = hand == Hand.left ? _secondaryLeft : _secondaryRight;
            return OVRInput.Get(button, GetOculusHand(hand));
        }

        public override bool SecondaryButtonDown(Hand hand) {
            OVRInput.RawButton button = hand == Hand.left ? _secondaryLeft : _secondaryRight;
            return OVRInput.GetDown(button, GetOculusHand(hand));
        }

        public override bool SecondaryButtonTouch(Hand hand) {
            OVRInput.RawTouch button = hand == Hand.left ? _secondaryLeftTouch : _secondaryRightTouch;
            return OVRInput.Get(button, GetOculusHand(hand));
        }

        public override bool Trigger(Hand hand) {
            return OVRInput.Get(_grab, GetOculusHand(hand));
        }

        public override bool TriggerDown(Hand hand) {
            return OVRInput.GetDown(_grab, GetOculusHand(hand));
        }

        public override float TriggerAxis(Hand hand) {
            return OVRInput.Get(_grabAxis, GetOculusHand(hand));
        }

        public override bool TriggerUp(Hand hand) {
            return OVRInput.GetUp(_grab, GetOculusHand(hand));
        }

        public override bool Grip(Hand hand) {
            return OVRInput.Get(_gripButton, GetOculusHand(hand));
        }

        public override bool GripDown(Hand hand) {
            return OVRInput.GetDown(_gripButton, GetOculusHand(hand));
        }

        public override float GripAxis(Hand hand) {
            return OVRInput.Get(_gripAxis, GetOculusHand(hand));
        }

        public override bool GripUp(Hand hand) {
            return OVRInput.GetUp(_gripButton, GetOculusHand(hand));
        }

        public override bool TwoHandGrip() {
            return Grip(Hand.left) && Grip(Hand.right);
        }

        public override Vector2 PrimaryAxis2D(Hand hand) {
            OVRInput.Controller controller = hand == Hand.left ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;
            return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller);
        }

        private OVRInput.Controller GetOculusHand(Hand hand) {
            return hand == Hand.left ? _left : _right;
        }

        public override bool JoystickButton(Hand hand) {
            OVRInput.RawButton button = hand == Hand.left ? _repositionLeft : _repositionRight;
            return OVRInput.Get(button);
        }

        public override bool JoystickButtonDown(Hand hand) {
            OVRInput.RawButton button = hand == Hand.left ? _repositionLeft : _repositionRight;
            return OVRInput.GetDown(button);
        }
    }

    public enum Hand
    {
        left,
        right
    }
}