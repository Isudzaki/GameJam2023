using UnityEngine;
using DG.Tweening;

public class ShowCatScene : MonoBehaviour
{
    [SerializeField] GameObject catScene;
    public void ShowScene()
    {
        catScene.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
    }
}
