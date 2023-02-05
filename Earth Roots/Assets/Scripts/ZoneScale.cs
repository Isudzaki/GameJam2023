using UnityEngine;
using DG.Tweening;

public class ZoneScale : MonoBehaviour
{
    [SerializeField] RectTransform _moleHead;
    void Update()
    {
        if (Variables.zone == 1)
        {
            _moleHead.DOMoveY(583, 0.5f);
        }
        else if (Variables.zone == 2)
        {
            _moleHead.DOMoveY(548, 0.5f);
        }
        else
        {
            _moleHead.DOMoveY(503, 0.5f);
        }
    }
}
