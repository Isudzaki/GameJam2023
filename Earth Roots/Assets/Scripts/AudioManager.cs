using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    [SerializeField] Scrollbar music;
    [SerializeField] AudioChange audioChange;
    private void Start()
    {
         Variables.music = PlayerPrefs.GetFloat("MusicVolume");
         music.value = Variables.music;
    }
    public void MusicChange()
    {
        Variables.music = music.value;
        audioChange.Change();
        PlayerPrefs.SetFloat("MusicVolume", Variables.music);
    }
}
