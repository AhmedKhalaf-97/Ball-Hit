using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonAudioSource : MonoBehaviour
{
    Button button;
    Slider slider;
    Toggle toggle;
    Dropdown dropdown;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (GetComponent<Button>() != null)
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(PlayAudioClip);
        }
        else
        if (GetComponent<Slider>() != null)
        {
            slider = GetComponent<Slider>();
            slider.onValueChanged.AddListener(delegate { PlayAudioClip(); });
        }
        else
        if (GetComponent<Toggle>() != null)
        {
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(delegate { PlayAudioClip(); });
        }
        else
        if (GetComponent<Dropdown>() != null)
        {
            dropdown = GetComponent<Dropdown>();
            dropdown.onValueChanged.AddListener(delegate { PlayAudioClip(); });
        }
    }

    void PlayAudioClip()
    {
        audioSource.Play();
    }
}
