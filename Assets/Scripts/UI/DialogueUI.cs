using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Core.Dialogue;

namespace UI.Overworld
{
    /// <summary>
    /// Manages dialogue UI
    /// </summary>
    public class DialogueUI : BaseUI
    {
        [SerializeField]
        TextMeshProUGUI textMesh;

        [SerializeField]
        TextMeshProUGUI speakerText;

        DialogueObject curObj;

        public delegate void DialogueEvent();

        /// <summary>
        /// Requests confirm UI to come up
        /// </summary>
        public static DialogueEvent OnConfirmRequest;

        private void OnEnable()
        {
            DialogueManager.OnDialogueFire += DialogueOn;
            DialogueManager.OnDialogueAfterFire += DialogueOff;
        }

        private void OnDisable()
        {
            DialogueManager.OnDialogueFire -= DialogueOn;
            DialogueManager.OnDialogueAfterFire -= DialogueOff;
        }

        protected override void HandlePrintingState()
        {
            speakerText.text = curObj.speakerName;
            textMesh.text = curObj.dialogueText;
            ChangeState(UIState.Displaying);
        }

        protected override void HandleDisplayState()
        {
            if (Input.GetKeyDown(Core.CoreManager.Instance.inputMap.interactKey))
            {
                // If it has a request confirmation, subscribe releveant events and invoke it
                if (curObj.requestConfirm)
                {
                    ConfirmUI.Confirm += AfterConfirmEvent;
                    ConfirmUI.Deny += AfterConfirmEvent;
                    OnConfirmRequest?.Invoke();
                }
                else if (curObj.hasNext) //If it has next, display next
                {
                    curObj = Core.CoreManager.Instance.dialogueManager.GrabDialogueObject(curObj.sceneName, curObj.dialogueNextID);
                    ChangeState(UIState.Printing);
                }
                else
                {
                    AfterDialogueEvent();
                }
            }
        }

        /// <summary>
        /// Does AfterDialogueEvent as well as cleans up UI subscriptions
        /// </summary>
        private void AfterConfirmEvent()
        {
            ConfirmUI.Confirm -= AfterConfirmEvent;
            ConfirmUI.Deny -= AfterConfirmEvent;
            AfterDialogueEvent();
        }

        /// <summary>
        /// Fires after dialogue event with inputs
        /// </summary>
        private void AfterDialogueEvent()
        {
            Core.CoreManager.Instance.dialogueManager.FireAfterDialogueEvent(curObj.sceneName, curObj.dialogueID);
        }

        /// <summary>
        /// Turns dialogue UI on
        /// Turns off player interact
        /// </summary>
        /// <param name="obj"></param>
        private void DialogueOn(DialogueObject obj)
        {
            state = UIState.Printing;
            curObj = obj;
            SetUIActive(true);
            Core.CoreManager.Instance.player.DisableInput();
        }

        /// <summary>
        /// Turns dialogue UI off
        /// Re-enables player interact
        /// </summary>
        /// <param name="obj"></param>
        private void DialogueOff(DialogueObject obj)
        {
            state = UIState.Off;
            curObj = null;
            textMesh.text = "";
            SetUIActive(false);
            Core.CoreManager.Instance.player.EnableInput();
        }      
    }
}
