using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

public class AudioController : MonoBehaviour
{
    private List<EventInstance> eventInstances;
    public static AudioController instance { get; private set; }

    private EventInstance musicEventInstance;

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
            Debug.Log("Destory Dupe Audio Controll");
        }

            eventInstances = new List<EventInstance>();
    }

    private void Start()
    {
        InitializeMusic(FMOD_Event.instance.music);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    /////////////////////////////////////////////////////////////////////////

    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateEventInstance(musicEventReference);
        musicEventInstance.start();
    }

    public void SetMusicParameter(string parameterName, float value)
    {
        musicEventInstance.setParameterByName(parameterName, value);
    }

    public void SetMusicScene(MusicScene scene)
    {
        musicEventInstance.setParameterByName("scene", (float)scene);
    }


    /////////////////////////////////////////////////////////////////////////

    private void CleanUp()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }

}
