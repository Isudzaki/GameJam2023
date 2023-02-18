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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _changer.DOFade(0, 1);
    }
    public void FadeIn()
    {
        Variables.hungerTimer = 65;
        Variables.hunger = 5;
        Variables.coins = 0;
        Variables.pickaxeAdd = 0;
        Variables.zone = 0;
        Variables.win = false;
        Variables.gameOver = false;
        Variables.chestSpawned = false;
        Variables.deep = 0;
        Variables.interactCave = 0;
        Variables.interactWorm1 = 0;
        Variables.interactQuest = 0;
        Variables.interactWorm2 = 0;
        Variables.questNum = 0;
        Variables.pickaxeBreak = 0;
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
