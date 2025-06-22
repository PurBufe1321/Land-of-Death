using UnityEngine;
using UnityEngine.SceneManagement;

public class EventController : MonoBehaviour
{
    public static EventController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void LoadSceneWithName(string num)
    {
        SceneManager.LoadScene(num);
    }
}
