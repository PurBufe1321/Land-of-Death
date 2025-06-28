using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMOD.Studio;
using JetBrains.Annotations;

public class Scene01Events : MonoBehaviour
{

    [SerializeField] private MusicScene scene;
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
    [SerializeField] GameObject Door;
    [SerializeField] GameObject Fadeout;
    private EventInstance FootsSteps;
    void Update()
    {
        textLength = TextCreator.charCount;
    }

    void Start()
    {
        StartCoroutine(EventStarter());
        FootsSteps = AudioController.instance.CreateEventInstance(FMOD_Event.instance.EnterRoom);
    }

    IEnumerator EventStarter()
    {
        // event 0
        Door.SetActive(false);
        yield return new WaitForSeconds(2);
        fadeScreenIn.SetActive(false);
        yield return new WaitForSeconds(2);
        charName.GetComponent<TMPro.TMP_Text>().text = "กิท";
        mainTextObject.SetActive(true);
        textToSpeak = "ที่นี้สินะ..";
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
        // event 1
        nextButton.SetActive(false);
        textBox.SetActive(true);
        textToSpeak = "เอาหล่ะไปกัน";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);
        eventPos = 2;
    }
    IEnumerator EventEnd()
    {
        nextButton.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        mainTextObject.SetActive(false);
        camCanMove.enabled = true;
        Door.SetActive(true);
    }

    public void NextButton()
    {
        switch (eventPos)
        {
            case 1:
                StartCoroutine(EventOne());
                break;
            case 2:
                eventPos = 3;
                StartCoroutine(EventEnd());
                break;
            case 3:
                StartCoroutine(NextRoom());
                break;
        }
    }

    IEnumerator NextRoom()
    {
        PLAYBACK_STATE playbackState;
        FootsSteps.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            FootsSteps.start();
        }

        Debug.Log("Load");
        Fadeout.SetActive(true);
        yield return new WaitForSeconds(3);
        FootsSteps.stop(STOP_MODE.ALLOWFADEOUT);
        AudioController.instance.SetMusicScene(scene);
        SceneManager.LoadScene(1);
    }
}
