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
        _music.DOFade(0f, 1);
        DOTween.Sequence()
           .Append(_changer.DOFade(1, 1))
           .AppendCallback(ChangeScene);
    }
    private void ChangeScene()
    {
        SceneManager.LoadScene("TopLevel");
    }
}
