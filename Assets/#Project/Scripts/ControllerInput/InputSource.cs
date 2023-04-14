using UnityEngine;

namespace Rowhouse
{
    public class InputSource : MonoBehaviour
    {
        public virtual bool MenuPressed(Hand hand) {
            return false;
        }

        public virtual bool MenuPressed() {
            return false;
        }

        public virtual bool MenuDown(Hand hand) {
            return false;
        }

        public virtual bool MenuDown() {
            return false;
        }

        public virtual bool SecondaryButton(Hand hand) {
            return false;
        }

        public virtual bool SecondaryButtonDown(Hand hand) {
            return false;
        }

        public virtual bool SecondaryButtonTouch(Hand hand) {
            return false;
        }

        public virtual bool PrimaryButton(Hand hand) {
            return false;
        }

        public virtual bool PrimaryButtonDown(Hand hand) {
            return false;
        }

        public virtual bool PrimaryButtonTouch(Hand hand) {
            return false;
        }

        public virtual bool Trigger(Hand hand) {
            return false;
        }

        public virtual float TriggerAxis(Hand hand) {
            return 0;
        }

        public virtual bool TriggerDown(Hand hand) {
            return false;
        }

        public virtual bool TriggerUp(Hand hand) {
            return false;
        }

        public virtual bool Grip(Hand hand) {
            return false;
        }

        public virtual float GripAxis(Hand hand) {
            return 0;
        }

        public virtual bool GripDown(Hand hand) {
            return false;
        }

        public virtual bool GripUp(Hand hand) {
            return false;
        }

        public virtual bool DebugHold(Hand hand) {
            return false;
        }

        public virtual bool DebugDown(Hand hand) {
            return false;
        }

        public virtual bool JoystickButton(Hand hand) {
            return false;
        }

        public virtual bool JoystickButtonDown(Hand hand) {
            return false;
        }

        public virtual bool TwoHandGrip() {
            return Grip(Hand.left) && Grip(Hand.right);
        }

        public virtual Vector2 PrimaryAxis2D(Hand hand) {
            return Vector2.zero;
        }
    }
}