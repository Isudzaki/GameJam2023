using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Apple : MonoBehaviour
{
    public int _hungerNum;
    private Image _sprite;
    private bool canChangeFade;
    private bool canChangeUnFade;
    private void Start()
    {
        _sprite = GetComponent<Image>();
        canChangeFade = true;
        canChangeUnFade = true;
    }
    void Update()
    {
        if (Variables.hunger < _hungerNum && canChangeFade)
        {
            canChangeFade=false;
            _sprite.DOFade(0, 0.5f);
            canChangeUnFade=true;
        }
        else if(Variables.hunger >= _hungerNum && canChangeUnFade)
        {
            canChangeUnFade = false;
            _sprite.DOFade(1, 0.5f);
            canChangeFade = true;
        }
    }
}
