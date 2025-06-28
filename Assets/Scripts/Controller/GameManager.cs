using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] private MusicScene scene;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Destory Dupe GameManager");
        }
    }

    public void Start()
    {
        AudioController.instance.SetMusicScene(scene);
    }
}
