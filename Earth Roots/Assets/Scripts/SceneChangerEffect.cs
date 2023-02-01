using UnityEngine;
using DG.Tweening;

public class SceneChangerEffect : MonoBehaviour
{
    [SerializeField] Teleport _teleport;

    private void Start()
    {
         transform.DOScale(new Vector3(0, 0, 0), 1);
    }
    public void FadeIn()
    {
        Variables.placeSpawn = 1;
        DOTween.Sequence()
           .Append(transform.DOScale(new Vector3(1, 1, 1), 1))
           .AppendCallback(_teleport.SceneChange);
    }
}
