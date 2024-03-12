using UnityEngine;
using UnityEngine.Audio;
using static GameManager.ServerManager;

public class UIAudioControl : MonoBehaviour
{
    /// <summary>
    /// 控制
    /// </summary>
    public AudioMixer audioMixer;
    public void Start ()
    {
        Audio.AudioControl = this;
    }
    public void SetVolume (string name, float volume)
    {
        audioMixer.SetFloat(name, volume);
    }

}
