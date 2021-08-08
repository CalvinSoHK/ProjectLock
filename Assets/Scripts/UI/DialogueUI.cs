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
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI textMesh;

        [SerializeField]
        TextMeshProUGUI speakerText;

        DialogueObject curObj;

        private enum DialogueUIState
        {
            Off,
            Printing,
            Displaying
        }

        private DialogueUIState state = DialogueUIState.Off;


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

        private void Update()
        {
            HandleState(state);
        }

        /// <summary>
        /// Handles UI states
        /// </summary>
        /// <param name="_state"></param>
        private void HandleState(DialogueUIState _state)
        {
            switch (_state)
            {
                case DialogueUIState.Printing:
                    speakerText.text = curObj.speakerName;
                    textMesh.text = curObj.dialogueText;
                    ChangeState(DialogueUIState.Displaying);
                    break;
                case DialogueUIState.Displaying:
                    if (Input.GetKeyDown(Core.CoreManager.Instance.inputMap.interactKey))
                    {
                        //If it has next, display next
                        if (curObj.hasNext)
                        {
                            curObj = Core.CoreManager.Instance.dialogueManager.GrabDialogueObject(curObj.sceneName, curObj.dialogueNextID);
                            ChangeState(DialogueUIState.Printing);
                        }
                        else
                        {
                            Core.CoreManager.Instance.dialogueManager.FireAfterDialogueEvent(curObj.sceneName, curObj.dialogueID);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Changes internal UI state
        /// </summary>
        /// <param name="_state"></param>
        private void ChangeState(DialogueUIState _state)
        {
            state = _state;
        }

        /// <summary>
        /// Turns dialogue UI on
        /// Turns off player interact
        /// </summary>
        /// <param name="obj"></param>
        private void DialogueOn(DialogueObject obj)
        {
            state = DialogueUIState.Printing;
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
            state = DialogueUIState.Off;
            curObj = null;
            textMesh.text = "";
            SetUIActive(false);
            Core.CoreManager.Instance.player.EnableInput();
        }

        /// <summary>
        /// Sets all children to active state
        /// </summary>
        /// <param name="active"></param>
        private void SetUIActive(bool active)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(active);
            }
        }
    }
}
