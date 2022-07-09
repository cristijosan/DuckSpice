using System;
using Control;
using UnityEngine;

namespace Script.Control
{
    public class ButtonControlActions : MonoBehaviour
    {
        public static Action<ButtonType> OnButtonPress;
        public static Action<ButtonType> OnButtonRelase;

        private void Awake() {
            OnButtonPress += OnButtonPressed;
            OnButtonRelase += OnButtonRelased;
        }

        private void OnDestroy() {
            OnButtonPress -= OnButtonPressed;
            OnButtonRelase -= OnButtonRelased;
        }

        private void OnButtonPressed(ButtonType buttonType) {
            switch (buttonType)
            {
                case ButtonType.Collect:
                    Movement.ChangeCollectAction?.Invoke();
                    break;
                case ButtonType.Hide:
                    Movement.ChangeHideAction?.Invoke(true);
                    break;
                case ButtonType.Nothing:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonType), buttonType, null);
            }
        }

        private void OnButtonRelased(ButtonType buttonType) {
            switch (buttonType)
            {
                case ButtonType.Hide:
                    Movement.ChangeHideAction?.Invoke(false);
                    break;
                case ButtonType.Nothing:
                    break;
                case ButtonType.Collect:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonType), buttonType, null);
            }
        }
    }
}