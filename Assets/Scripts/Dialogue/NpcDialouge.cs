using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialouge")]
public class NpcDialouge : ScriptableObject
{
    public string npcName;
    public string[] dialogueLines;
    public bool[] endDialogueLines; // this end dialogue at that number we want
    public int[] SceneNumUpDown;
    public float typingSpeed = 0.05f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;
    public DialogueChoice[] choices;
    public List<DialogueAction> ActionsTrigger;
}

[System.Serializable]

public class DialogueChoice
{
    public int dialogueIndex; //when choice will start
    public string[] choices; // choice
    public int[] nextDialogueIndexes; // make dialogue go to the choice we choose
    public bool[] TriggerEvent;
    public string[] result;
    public ActionType[] actionChoices;
}

[System.Serializable]

public class DialogueAction
{
    public ActionType actionType;
    public string stringParameter;
    public int intParameter;
}
