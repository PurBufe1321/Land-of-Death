using UnityEngine;
using FMODUnity;

public class FMOD_Event : MonoBehaviour

{
    [field: Header("Music")]
    [field: SerializeField] public EventReference music { get; private set; }


    [field: Header("Enter Room")]
    [field: SerializeField] public EventReference EnterRoom {  get; private set; }

    public static FMOD_Event instance { get; private set; }

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
            Debug.Log("Destory Dupe FMODEvent");
        }
    }
}
