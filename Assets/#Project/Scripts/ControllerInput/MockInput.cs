using UnityEngine;
using Input = UnityEngine.Input;

namespace Rowhouse
{
    public class MockInput : InputSource
    {
        public override bool Trigger(Hand hand) {
            return Input.GetMouseButton(GetMouseButton(hand));
        }

        public override float TriggerAxis(Hand hand) {
            return Input.GetMouseButton(GetMouseButton(hand)) ? 1f : 0;
        }

        public override bool TriggerDown(Hand hand) {
            return Input.GetMouseButtonDown(GetMouseButton(hand));
        }

        public override bool TriggerUp(Hand hand) {
            return Input.GetMouseButtonUp(GetMouseButton(hand));
        }

        private int GetMouseButton(Hand hand) {
            return hand == Hand.left ? 0 : 1;
        }
    }
}