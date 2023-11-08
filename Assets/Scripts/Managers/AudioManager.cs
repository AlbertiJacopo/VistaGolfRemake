using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer m_AudioMixer;
    [SerializeField] private Slider MusicSlider, EffectsSlider;

    private AudioSource m_AudioSource;
    [SerializeField] private List<string> m_AudioKeys = new List<string>();
    [SerializeField] private List<AudioClip> m_AudioValues = new List<AudioClip>();

    private void Start()
    {
		if(MusicSlider != null && EffectsSlider != null)
		{
			SetSliderSettings(MusicSlider, "MusicVolume");
			SetSliderSettings(EffectsSlider, "EffectsVolume");

		}

        m_AudioSource = GetComponent<AudioSource>();

        GameManager.instance.EventManager.Register(Constants.PLAY_SOUND, PlaySound);
    }

    public void PlaySound(object[] param)
    {
        int index = -1;
        for (int i = 0; i < m_AudioValues.Count; i++)
        {
            if (m_AudioKeys[i] == (string)param[0])
            {
                index = i; 
                break;
            }
        }
        if (index != -1)
        {
            m_AudioSource.clip = m_AudioValues[index];
            m_AudioSource.Play();
        }
    }

    /// <summary>
    /// Changes values of the audio mixer's groups by using sliders
    /// </summary>
    public void SetVolume()
    {
        SetAudioLevel(MusicSlider, "MusicVolume", "MusicVol");
        SetAudioLevel(EffectsSlider, "EffectsVolume", "EffectsVol");
    }

    /// <summary>
    /// saves the audio slider in the PlayerPrefs and perform the actual volume changing
    /// </summary>
    /// <param name="slider"></param>
    /// <param name="key"> string name used as key for the PlayerPrefs</param>
    /// <param name="groupName">Audio Mixer group name</param>
    private void SetAudioLevel(Slider slider, string key, string groupName)
    {
        m_AudioMixer.SetFloat(groupName, Mathf.Log10(slider.value) * 20);
        PlayerPrefs.SetFloat(key, slider.value);
    }

    /// <summary>
    /// set the slider value to 1
    /// </summary>
    /// <param name="slider"></param>
    /// <param name="key"></param>
    private void SetSliderSettings(Slider slider, string key)
    {
        slider.value = PlayerPrefs.GetFloat(key, 1f);
    }
}
