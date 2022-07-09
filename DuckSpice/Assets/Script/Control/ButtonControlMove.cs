using Script.Control;
using UnityEngine;

namespace Control
{
    public class ButtonControlMove : MonoBehaviour
    {
        private FloatingJoystick floatingJoystick;

        private void Start()
        {
            floatingJoystick = gameObject.GetComponent<FloatingJoystick>();
        }

        private void FixedUpdate()
        {
            Movement.ChangeVector?.Invoke(floatingJoystick.Horizontal, floatingJoystick.Vertical);
        }
    }
}