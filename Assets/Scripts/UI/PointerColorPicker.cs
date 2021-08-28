using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(BasePointerUI))]
    public class PointerColorPicker : MonoBehaviour
    {
        [Header("Pointer Color Options")]
        [SerializeField]
        private Image targetImg;

        [Tooltip("Default color")]
        [SerializeField]
        private Color DefaultColor;

        [SerializeField]
        [Tooltip("Pointer enter color")]
        private Color PointerEnter;

        [SerializeField]
        [Tooltip("Pointer down color")]
        private Color PointerDown;

        private void Start()
        {
            BasePointerUI pointerUI = GetComponent<BasePointerUI>();

            pointerUI.OnPointerExitEvent.AddListener(() => ChangeColor(DefaultColor));
            pointerUI.OnPointerEnterEvent.AddListener(() => ChangeColor(PointerEnter));
            pointerUI.OnPointerDownEvent.AddListener(() => ChangeColor(PointerDown));
            pointerUI.OnPointerUpEvent.AddListener(() => ChangeColor(DefaultColor));
        }

        private void ChangeColor(Color color)
        {
            targetImg.color = color;
        }
    }
}
