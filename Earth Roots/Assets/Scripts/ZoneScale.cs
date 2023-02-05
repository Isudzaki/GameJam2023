using UnityEngine;
using DG.Tweening;

public class ZoneScale : MonoBehaviour
{
    [SerializeField] RectTransform _moleHead;
    [SerializeField] MeshRenderer floor;
    [SerializeField] Color color1;
    [SerializeField] Color color2;
    [SerializeField] Color color3;
    void Update()
    {
        if (Variables.zone == 1)
        {
            floor.material.color = color1;
            _moleHead.DOMoveY(595, 0.5f);
        }
        else if (Variables.zone == 2)
        {
            floor.material.color = color2;
            _moleHead.DOMoveY(555, 0.5f);
        }
        else
        {
            floor.material.color = color3;
            _moleHead.DOMoveY(515, 0.5f);
        }
    }
}
