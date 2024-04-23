using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MuteManager : MonoBehaviour
{
    private bool isMuted = false;

    void Start()
    {
        // Check if the game was previously muted
        if (PlayerPrefs.HasKey("isMuted"))
        {
            isMuted = PlayerPrefs.GetInt("isMuted") == 1;
            AudioListener.pause = isMuted;
        }
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;

        // Save the mute state
        PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }
}