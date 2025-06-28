using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEditor.PackageManager;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class ObjectThatCanClick : MonoBehaviour, Clickable
{
    public static LaNubPuzzle Instance { get; private set; }
    private ActionType type;
    public NpcDialouge dialogueData;
    private DialougeController dialogueUI;
    
    private int dialogueIndex;
    private bool isTyping, isDialogueActive;
    private void Start()
    {
        dialogueUI = DialougeController.Instance;
    }
    public bool CanClick()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (dialogueData == null && (!isDialogueActive))
            return;

        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        dialogueUI.DisableMouseMove();
        isDialogueActive = true;
        dialogueIndex = 0;
        dialogueUI.SetNPCInfo(dialogueData.npcName);
        dialogueUI.showDialogueUI(true);

        DisplayCurrentLine();
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueUI.SetDialogueText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }

        dialogueUI.ClearChoices();
        dialogueUI.EnableInput();

        if (dialogueData.endDialogueLines.Length > dialogueIndex && dialogueData.endDialogueLines[dialogueIndex])
        {
            EndDialogue();
            return;
        }

        foreach (DialogueChoice dialogueChoice in dialogueData.choices)
        {
            if (dialogueChoice.dialogueIndex == dialogueIndex)
            {
                dialogueUI.DisableInput();
                DisplayChoices(dialogueChoice);
                return;
            }
        }

        if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            DisplayCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueUI.SetDialogueText("");

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text += letter);
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;
    }

    void DisplayChoices(DialogueChoice choice)
    {
        for (int i = 0; i < choice.choices.Length; i++)
        {
            int nextIndex = choice.nextDialogueIndexes[i];
            bool triggerEvent = choice.TriggerEvent[i];
            string result = choice.result[i];
            Enum action = choice.actionChoices[i];
            dialogueUI.CreateChoiceButton(choice.choices[i], () => ChooseOption(nextIndex, triggerEvent, result, action));
        }
    }

    void ChooseOption(int nextIndex, bool triggerEvent, string result, Enum action)
    {
        dialogueIndex = nextIndex;
        dialogueUI.ClearChoices();
        DisplayCurrentLine();

        if (triggerEvent)
        {
            switch (action)
            {
                case ActionType.Nothing:
                    return;

                case ActionType.Load:
                    Debug.Log($"Load {result}");
                    EventController.Instance.LoadSceneWithName(result);
                    return;

                case ActionType.GiveItem:
                    Debug.Log($"Give {result}");
                    return;

                case ActionType.ChangeCursePainting:
                    EventController.Instance.ChangePainting(result);
                    return;
                case ActionType.LaNabPuzzleOn:
                    dialogueUI.showLaNabUI(true);
                    StopAllCoroutines();
                    isDialogueActive = false;
                    dialogueUI.SetDialogueText("");
                    dialogueUI.showDialogueUI(false);
                    dialogueUI.DisableInput();
                    return;
                case ActionType.MC_CCP_Bad:
                    EventController.Instance.MC_CCP_Bad(result);
                    return;
                case ActionType.MC_CCP_Good:
                    EventController.Instance.MC_CCP_Good(result);
                    return;
                case ActionType.CheckingNote:
                    EventController.Instance.CheckingNote(result);
                    return;
                case ActionType.TurnMusicIntoScene4:
                    EventController.Instance.TurnMusicIntoScene4();
                    return;
            }
        }
    }

    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;       
        dialogueUI.SetDialogueText("");
        dialogueUI.showDialogueUI(false);
        dialogueUI.EnableMouseMove();
        if (dialogueData != null && dialogueData.ActionsTrigger != null && dialogueData.ActionsTrigger.Count > 0)
        {
            Debug.Log($"There will have {dialogueData.ActionsTrigger.Count} event happen");
            foreach (DialogueAction action in dialogueData.ActionsTrigger)
            {
                DoAnAction(action);
            }
        }
    }

    public void DoAnAction(DialogueAction ActionToDo)
    {
        if (ActionToDo == null) return;

        switch (ActionToDo.actionType)
        {
            case ActionType.Load:
                Debug.Log($"Load {ActionToDo.stringParameter}");
                EventController.Instance.LoadSceneWithName(ActionToDo.stringParameter);
                return;
            case ActionType.GiveItem:
                Debug.Log($"Give {ActionToDo.stringParameter}");
                return;
            case ActionType.OneTimeOnlyChat:
                Debug.Log("Test");
                GameObject destoryobj = GameObject.Find($"{ActionToDo.stringParameter}");
                Destroy(destoryobj);
                return;
            case ActionType.AddNextDialogue:
                GameObject objActivate = GameObject.Find($"{ActionToDo.stringParameter}");
                objActivate.GetComponent<BoxCollider2D>().enabled = true;
                return;
            case ActionType.NoteUp:
                EventController.Instance.NoteUp(ActionToDo.stringParameter);
                return;
            case ActionType.TurnMusicIntoScene4:
                EventController.Instance.TurnMusicIntoScene4();
                return;

}
    }
}
