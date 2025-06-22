using System.Collections;
using UnityEngine;

public class Scene02EventStart : MonoBehaviour
{
    public GameObject fadeScreenIn;
    public GameObject textBox;
    public CameraFollow camCanMove;
    [SerializeField] string textToSpeak;
    [SerializeField] int currentTextLength;
    [SerializeField] int textLength;
    [SerializeField] GameObject mainTextObject;
    [SerializeField] GameObject nextButton;
    [SerializeField] int eventPos = 0;
    [SerializeField] GameObject charName;
    void Update()
    {
        textLength = TextCreator.charCount;
    }

    void Start()
    {
        StartCoroutine(EventStarter());
    }

    IEnumerator EventStarter()
    {
        // event 0
        yield return new WaitForSeconds(2);
        fadeScreenIn.SetActive(false);
        yield return new WaitForSeconds(2);
        charName.GetComponent<TMPro.TMP_Text>().text = "กิท";
        mainTextObject.SetActive(true);
        textToSpeak = "...";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);
        eventPos = 1;
    }

    IEnumerator EventOne()
    {
        nextButton.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        mainTextObject.SetActive(false);
        camCanMove.enabled = true;
        eventPos = 2;
    }

    public void NextButton()
    {
        switch (eventPos)
        {
            case 1:
                StartCoroutine(EventOne());
                break;
            case 2:
                StartCoroutine(OnChoices());
                break;
            case 3:
                ChooseChoices();
                break;
        }
    }

    IEnumerator OnChoices()
    {
        //Dialouge line 1//
        mainTextObject.SetActive(true);
        textToSpeak = "ระนาบอย่างนั้นหรอ?";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(2);
        //Dialouge line 2//
        textToSpeak = "มันเหมือนจะมีพลังอะไรบางอย่าง....";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        nextButton.SetActive(true);
        eventPos = 3;
    }

    void ChooseChoices()
    {
        Debug.Log("choose choice");
        nextButton.SetActive(false);
        mainTextObject.SetActive(false);
    }
}
