using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventController : MonoBehaviour
{
    public static EventController Instance { get; private set; }

    public bool paintingChange = false;
    public Sprite[] PaintPic;
    public GameObject PaintImage;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void LoadSceneWithName(string num)
    {
        SceneManager.LoadScene(num);
    }

    public void ChangePainting(string result)
    {
        Debug.Log($"Change into {result} ");
        paintingChange = true;
        PaintImage.GetComponent<SpriteRenderer>().sprite = PaintPic[int.Parse(result)];
    }
}
