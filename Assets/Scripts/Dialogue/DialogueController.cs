using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialougeController : MonoBehaviour
{
    public static DialougeController Instance { get; private set; }

    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Transform choiceContainer;
    public GameObject choiceButtonPrefab;
    public GameObject _LaNabUI;
    public GameObject _mouse;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _mouse = GameObject.Find("Mouse");
        PlayerInput Pinput = _mouse.GetComponent<PlayerInput>();
    }

    public void showDialogueUI(bool show)
    {
        dialoguePanel.SetActive(show);
    }

    public void showLaNabUI(bool show)
    {
        _LaNabUI.SetActive(show);
    }

    public void SetNPCInfo(string npcname)
    {
        nameText.text = npcname;
    }

    public void SetDialogueText(string text)
    {
        dialogueText.text = text;
    }

    public void ClearChoices()
    {
        EnableInput();
        foreach (Transform child in choiceContainer) Destroy(child.gameObject);
    }

    public void CreateChoiceButton(string choiceText, UnityEngine.Events.UnityAction onClick)
    {
        GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceContainer);
        choiceButton.GetComponentInChildren<TMP_Text>().text = choiceText;
        choiceButton.GetComponent<Button>().onClick.AddListener(onClick);
    }

    public void DisableMouseMove()
    {
        _mouse.GetComponent<CameraFollow>().enabled = false;
    }

    public void EnableMouseMove()
    {
        _mouse.GetComponent<CameraFollow>().enabled = true;
    }

    public void DisableInput()
    {
        _mouse.GetComponent<PlayerInput>().enabled = false;
    }

    public void EnableInput()
    {
        _mouse.GetComponent<PlayerInput>().enabled = true;
    }
}
