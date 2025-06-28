using System.Collections;
using System.Security.Cryptography;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventController : MonoBehaviour
{
    public static EventController Instance { get; private set; }

    [SerializeField] private MusicScene scene;

    public bool paintingChange = false;
    public Sprite[] PaintPic;
    public GameObject PaintImage;
    public LaNubPuzzle LaNabUI;
    public int SinCounter;
    public int NoteCollected;
    public GameObject FadeOut;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void LoadSceneWithName(string num)
    {
        int.Parse(num);
        SceneManager.LoadScene(int.Parse(num));
    }

    public void ChangePainting(string result)
    {
        Debug.Log($"Change into {result} ");
        paintingChange = true;
        PaintImage.GetComponent<SpriteRenderer>().sprite = PaintPic[int.Parse(result)];
    }

    public void MC_CCP_Bad(string result)
    {
        SinCounter++;
        ChangePainting(result);
        scene = MusicScene.Scene2to3_Tormented;

        AudioController.instance.SetMusicScene(scene);
    }

    public void MC_CCP_Good(string result)
    {
        ChangePainting(result);
        scene = MusicScene.Scene2to3_SlightlyBetter;

        AudioController.instance.SetMusicScene(scene);
    }

    public void NoteUp(string result)
    {
        NoteCollected++;
        GameObject destoryobj = GameObject.Find($"{result}");
        Destroy(destoryobj);
    }

    public void CheckingNote (string result)
    {
        if (NoteCollected >= 2)
        {
            LoadSceneWithName(result);
            scene = MusicScene.Scene5;
            AudioController.instance.SetMusicScene(scene);
        }
    }

    public void TurnMusicIntoScene4()
    {
        FadeOut.SetActive(true);
        scene = MusicScene.Scene4;
        AudioController.instance.SetMusicScene(scene);
    }
}
