using UnityEngine;
using DG.Tweening;

public class ShowWinKatScene : MonoBehaviour
{
    [SerializeField] GameObject catScene;
    private bool _sceneAdded;
    private void Start()
    {
        _sceneAdded = false;
    }
    public void Update()
    {
        if (Variables.win == true && !_sceneAdded)
        {
            _sceneAdded = true;
            catScene.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        }
    }
}
