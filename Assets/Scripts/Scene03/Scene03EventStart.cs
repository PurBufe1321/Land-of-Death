using System.Collections;
using UnityEngine;

public class Scene03EventStart : MonoBehaviour
{

    public Scene03EventStart Instance { get; private set; }

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
        charName.GetComponent<TMPro.TMP_Text>().text = "กิท";
        mainTextObject.SetActive(true);
        textToSpeak = "ความทรงจำที่แตกสลาย...ข้าต้องปะติดปะต่อมัน...ไพ่ของข้า...จะต้องมองเห็นเศษเสี้ยวเหล่านั้น";
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
        }
    }
}
