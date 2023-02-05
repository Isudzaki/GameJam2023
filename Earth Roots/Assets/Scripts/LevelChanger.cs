using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] AudioSource _music;
    [SerializeField] Image _changer;
    private void Start()
    {
        _changer.DOFade(0, 1);
    }
    public void FadeIn()
    {
        Variables.hungerTimer = 60;
        Variables.hunger = 5;
        Variables.coins = 0;
        Variables.pickaxeAdd = 0;
        Variables.zone = 0;
        Variables.win = false;
        Variables.gameOver = false;
        _music.DOFade(0f, 1);
        DOTween.Sequence()
           .Append(_changer.DOFade(1, 1))
           .AppendCallback(ChangeScene);
    }
    private void ChangeScene()
    {
        Variables.placeSpawn =0;
        SceneManager.LoadScene("TopLevel");
    }
}
