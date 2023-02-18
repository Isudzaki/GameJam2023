using UnityEngine;
public class AudioChange : MonoBehaviour
{
    [SerializeField] AudioSource[] music;
    void Start()
    {
        Change();
    }
    public void Change()
    {
        for (int i = 0; i < music.Length; i++)
        {
            music[i].volume = Variables.music;
        }
    }
}
