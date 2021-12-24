using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Core.Dialogue;
using CustomInput;
using UI.Base;

namespace UI.Dialogue
{
    /// <summary>
    /// Manages dialogue UI
    /// </summary>
    public class DialogueElementUI : BaseElementUI
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

        public override void HandlePrintingState()
        {
            Debug.Log("Printing dialogue state");
            speakerText.text = curObj.speakerName;
            textMesh.text = curObj.dialogueText;
            base.HandlePrintingState();
        }

        public override void HandleDisplayState()
        {
            if (Core.CoreManager.Instance.inputMap.GetInput(InputEnums.InputName.Interact, InputEnums.InputAction.Down))
            {
                // If it has a request confirmation, subscribe releveant events and invoke it
                if (curObj.requestConfirm)
                {
                    ConfirmElementUI.Confirm += AfterConfirmEvent;
                    ConfirmElementUI.Deny += AfterConfirmEvent;
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
            if (curObj.requestConfirm)
            {
                ConfirmElementUI.Confirm -= AfterConfirmEvent;
                ConfirmElementUI.Deny -= AfterConfirmEvent;
            }

            AfterDialogueEvent();
        }

        /// <summary>
        /// Fires after dialogue event with inputs
        /// </summary>
        private void AfterDialogueEvent()
        {
            if (curObj.isNotScene)
            {
                Core.CoreManager.Instance.dialogueManager.FireAfterDialogue(curObj);
            }
            else
            {
                Core.CoreManager.Instance.dialogueManager.FireAfterDialogueEvent(curObj.sceneName, curObj.dialogueID);
            }
        }

        /// <summary>
        /// Turns dialogue UI on
        /// Turns off player interact
        /// </summary>
        /// <param name="obj"></param>
        private void DialogueOn(DialogueObject obj)
        {
            Debug.Log("Turning on dialogue");
            ChangeState(UIState.Printing);
            curObj = obj;
            Core.CoreManager.Instance.player.DisableInputMovement();
        }

        /// <summary>
        /// Turns dialogue UI off
        /// Re-enables player interact
        /// </summary>
        /// <param name="obj"></param>
        private void DialogueOff(DialogueObject obj)
        {
            Debug.Log("Turning off dialogue");
            ChangeState(UIState.Hiding);
            curObj = null;
            textMesh.text = "";
            Core.CoreManager.Instance.player.EnableInputMovement();
        }

        public override void EnableElement()
        {
            throw new System.NotImplementedException();
        }

        public override void DisableElement()
        {
            throw new System.NotImplementedException();
        }

        public override void RefreshElement()
        {
            throw new System.NotImplementedException();
        }
    }
}
