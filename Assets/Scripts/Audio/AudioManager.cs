using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{



    AudioMixerGroup sfx;
    AudioMixerGroup music;
    AudioMixerGroup master;

    private const string musicVol = "MusicVol";
    private const string sfxVol = "SFXVol";
    private const string masterVol = "MasterVol";


    [Header("-----------------------------------------------------------------------------------------------------")]
    [Header("Filters")]
    [Header("-----------------------------------------------------------------------------------------------------")]
    [SerializeField]
    [Tooltip("The length of the transition to and from water")]
    float waterTimeout = 0.3f;
    [SerializeField]
    [Tooltip("The Filter for the underwater section")]
    AudioMixerSnapshot underwater;
    [SerializeField]
    [Tooltip("The default filter section")]
    AudioMixerSnapshot normal;
    [SerializeField]
    [Tooltip("The Filter for the Pause menu")]
    AudioMixerSnapshot pauseFilter;
    [SerializeField]
    [Tooltip("The length of the transition to and from pauseMenu")]
    float pauseTimeout = 0.3f;

    #region SFX
    [Header("-----------------------------------------------------------------------------------------------------")]
    [Header("OneShots")]
    [Header("-----------------------------------------------------------------------------------------------------")]
    [SerializeField] GameObject boing;
    [SerializeField] GameObject box_break;
    [SerializeField] GameObject coin_get;
    [SerializeField] GameObject thud;

    [Header("-----------------------------------------------------------------------------------------------------")]
    [Header("State Based SFX")]
    [Header("-----------------------------------------------------------------------------------------------------")]

    [SerializeField] GameObject move_obj;
    [SerializeField] GameObject wall_slide;
    #endregion

    [Header("-----------------------------------------------------------------------------------------------------")]
    [Header("Ambience")]
    [Header("-----------------------------------------------------------------------------------------------------")]

    #region Ambience
    [SerializeField] GameObject ocean;

    [Header("-----------------------------------------------------------------------------------------------------")]
    [Header("Music")]
    [Header("-----------------------------------------------------------------------------------------------------")]
    [SerializeField] GameObject CoolCPU;

    #endregion
    private void Awake()
    {
        DontDestroyOnLoad(this);


        AudioMixer mixer = Resources.Load("Mixer") as AudioMixer;

        if (normal != null)
            normal.TransitionTo(0.1f);

        if (mixer != null)
        {
            //only one of each group will be found, it just likes to return an array
            sfx = mixer.FindMatchingGroups("SFX")[0];
            music = mixer.FindMatchingGroups("BGM")[0];
            master = mixer.FindMatchingGroups("Master")[0];
           
            

        }
        else
        {
            Debug.LogError("Error Sound Mixer was not loaded properly");
        }

    }


    #region UI_INTERACTION
    public void setSFXVol(float value)
    {

        sfx.audioMixer.SetFloat(sfxVol, Mathf.Log10(value) * 20);
    }

    public void setMusicVol(float value)
    {
        music.audioMixer.SetFloat(musicVol, Mathf.Log10(value) * 20);
    }

    public void setMasterVol(float value)
    {
        master.audioMixer.SetFloat(masterVol, Mathf.Log10(value) * 20);
    }

    public float getSFXVol()
    {

        sfx.audioMixer.GetFloat(sfxVol, out float val);
        val = Mathf.Pow(10, val / 20);
        if (val <= 0 || val > 1)
        {
            return 0.5f;
        }
        return val;
    }

    public float getMusicVol()
    {
        music.audioMixer.GetFloat(musicVol, out float val);
        val = Mathf.Pow(10, val / 20);
        if (val <= 0 || val > 1)
        {
            return 0.5f;
        }
        return val;
    }

    public float getMasterVol()
    {
        master.audioMixer.GetFloat(masterVol, out float val);
        val = Mathf.Pow(10, val / 20);
        if (val <= 0 || val > 1)
        {
            return 0.5f;
        }
        return val;
    }

    #endregion

    #region ONE_SHOTS_IN_WORLDSPACE

    public void playBoingSound(Vector3 world_point)
    {
        var obj = Instantiate(boing);
        obj.transform.position = world_point;
    }

    public void playBoxBreak(Vector3 world_point)
    {
        var obj = Instantiate(box_break);
        obj.transform.position = world_point;
    }

    public void playThud(Vector3 world_point)
    {
        var obj = Instantiate(thud);
        obj.transform.position = world_point;
    }

    public void playCoin(Vector3 world_point)
    {
        var obj = Instantiate(coin_get);
        obj.transform.position = world_point;
    }
    #endregion

    #region FILTER_TRANSITIONS

    public void to_underwater()
    {
        underwater.TransitionTo(waterTimeout);
    }

    public void to_normal_from_water()
    {
        normal.TransitionTo(waterTimeout);
    }

    public void to_pause_menu()
    {
        pauseFilter.TransitionTo(pauseTimeout);
    }

    public void to_normal_from_pause()
    {
        normal.TransitionTo(pauseTimeout);
    }

    #endregion




}