using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Base
{
    [RequireComponent(typeof(BasePointerElementUI))]
    public class PointerColorPicker : MonoBehaviour
    {
        [Header("Pointer Color Options")]
        [SerializeField]
        private Image targetImg;

        [Tooltip("Default color")]
        [SerializeField]
        public Color DefaultColor;

        [Tooltip("If true, will set DefaultColor to what was already set. If false it will use the set DefaultColor.")]
        [SerializeField]
        private bool GrabDefault = false;

        [SerializeField]
        [Tooltip("Pointer enter color")]
        public Color PointerEnter;

        [SerializeField]
        [Tooltip("Pointer down color")]
        public Color PointerDown;

        [SerializeField]
        [Tooltip("Selected color")]
        public Color SelectedColor;

        /// <summary>
        /// Locks the color so it doesnt change no matter what event
        /// </summary>
        private bool colorLocked = false;

        private void Start()
        {
            BasePointerElementUI pointerUI = GetComponent<BasePointerElementUI>();

            if (GrabDefault)
            {
                DefaultColor = targetImg.color;
            }

            pointerUI.OnPointerExitEvent.AddListener(() => ChangeColor(DefaultColor));
            pointerUI.OnPointerEnterEvent.AddListener(() => ChangeColor(PointerEnter));
            pointerUI.OnPointerDownEvent.AddListener(() => ChangeColor(PointerDown));
            pointerUI.OnPointerUpEvent.AddListener(() => ChangeColor(DefaultColor));
        }

        public void ChangeColor(Color color)
        {
            if (!colorLocked)
            {
                targetImg.color = color;
            }          
        }

        /// <summary>
        /// Sets color picker lock so it will stop changing colors
        /// </summary>
        /// <param name="state"></param>
        public void SetLock(bool state)
        {
            colorLocked = state;
        }
    }
}
