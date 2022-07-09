using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Control;
using Script.Control;

[Serializable]
public enum ButtonType
{
    Nothing,
    Collect,
    Hide
}

namespace UI
{
    public class ButtonAnimation : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
    {
        [SerializeField] private Sprite spriteRelased;
        [SerializeField] private Sprite spritePressed;
        [SerializeField] private ButtonType buttonType;

        private Image thisImage;

        private void Start()
        {
            thisImage = GetComponent<Image>();
            thisImage.sprite = spriteRelased;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            thisImage.sprite = spritePressed;
            if (buttonType != ButtonType.Nothing)
                ButtonControlActions.OnButtonPress?.Invoke(buttonType);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            thisImage.sprite = spriteRelased;
            if (buttonType == ButtonType.Hide)
                ButtonControlActions.OnButtonRelase?.Invoke(buttonType);
        }
    }
}