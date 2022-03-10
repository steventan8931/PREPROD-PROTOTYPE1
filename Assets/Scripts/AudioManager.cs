using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum AudioTest
    {
        rockhit, treehit, onhit, enemyhit, playerhit, rockbreak, crafting, chestopen
    };

    public AudioTest test;
    [Range(0.0f, 1.0f)]
    public float volume = 1;
    public static AudioManager Instance;
    private AudioTest cacheTest;
    public AudioSource m_Source;
    public AudioClip rockhit,treehit,onhit,enemyhit,playerhit,rockbreak,crafting,chestopen;

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        m_Source = GetComponent<AudioSource>();
        cacheTest = test;
        //PlayAudio(test.ToString());
    }

    [ExecuteInEditMode]
    private void Update()
    {
        //Debug in inspector
        //if (test != cacheTest || m_Source.volume != volume)
        //{
        //    PlayAudio(test.ToString());
        //    m_Source.volume = volume;
        //    cacheTest = test;
        //}
    }

    public void PlayAudio(string _name)
    {
        switch(_name)
        {
            case "rockhit":
                m_Source.PlayOneShot(rockhit);
                break;
            case "treehit":
                m_Source.PlayOneShot(treehit);
                break;
            case "onhit":
                m_Source.PlayOneShot(onhit);
                break;
            case "enemyhit":
                m_Source.PlayOneShot(enemyhit);
                break;
            case "playerhit":
                m_Source.PlayOneShot(playerhit);
                break;
            case "rockbreak":
                m_Source.PlayOneShot(rockbreak);
                break;
            case "crafting":
                m_Source.PlayOneShot(crafting);
                break;
            case "chestopen":
                m_Source.PlayOneShot(chestopen);
                break;
        }
    }
}
