using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField]
    List<Sound> listSounds;

    protected override void Awake()
    {
        base.Awake();
       
        foreach (Sound s in listSounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.clip = s.clip;
            if (s.PlayOnAwake) s.audioSource.Play();
        }
    }

    public void PlaySound(SoundName soundName)
    {
        foreach (Sound s in listSounds)
        {
            if (s.soundName == soundName)
            {
                if (s.audioSource.isPlaying) continue;
                else
                {
                    s.audioSource.Play();
                    break;
                }

            }
        }
    }

    public void PlaySound(SoundName soundName, float volumeMultiplier)
    {
        foreach (Sound s in listSounds)
        {
            if (s.soundName == soundName)
            {
                if (s.audioSource.isPlaying) continue;
                else
                {
                    s.audioSource.PlayOneShot(s.clip, s.volume + s.volume * volumeMultiplier);
                    break;
                }

            }
        }
    }
    public void StopSound(SoundName soundName)
    {
        foreach (Sound s in listSounds)
        {
            if (s.soundName == soundName)
            {
                s.audioSource.Stop();
            }
        }
    }

    public void PlayLoop(SoundName soundName)
    {
        foreach (Sound s in listSounds)
        {
            if (s.soundName == soundName)
            {
                if (s.audioSource.isPlaying) return;
                s.audioSource.Stop();
                s.audioSource.loop = true;
            }
        }
    }

    public void SetVolume(float volume , SoundType type = SoundType.SFX)
    {
        foreach (Sound s in listSounds)
        {
            if (s.type == type)
            {
                s.audioSource.volume = s.volume * volume;
            }
        }
    }
}
[System.Serializable]
public class Sound
{
    public SoundName soundName;
    public SoundType type = SoundType.SFX;
    [Range(0.1f, 4f)]
    public float volume;
    [Range(0.5f, 2f)]
    public float pitch;
    public AudioClip clip;
    [HideInInspector]
    public AudioSource audioSource;
    public bool PlayOnAwake;

}

public enum SoundName
{
    MusicBg,
    ButtonClick,
    Victory,
    Slide
}

public enum SoundType
{
    Music,
    SFX
}
