using UnityEngine;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    [SerializeField] AudioSource sounds;
    [SerializeField] AudioClip gameOver;
    [SerializeField] GameObject gameOverPanel;
    void Update()
    {
        if(Variables.hunger<=0 && !Variables.gameOver)
        {
            Variables.gameOver=true;
            sounds.clip = gameOver;
            sounds.Play();
            gameOverPanel.transform.DOScale(new Vector3(1, 1, 1),0.5f);
        }
    }
}
