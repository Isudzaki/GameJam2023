using UnityEngine;
using DG.Tweening;

public class ZoneScale : MonoBehaviour
{
    [SerializeField] RectTransform _moleHead;
    [SerializeField] RectTransform _point1;
    [SerializeField] RectTransform _point2;
    [SerializeField] RectTransform _point3;
    [SerializeField] RectTransform _point4;
    [SerializeField] MeshRenderer floor;
    [SerializeField] Color color1;
    [SerializeField] Color color2;
    [SerializeField] Color color3;
    void Update()
    {
        if (Variables.zone == 1)
        {
            floor.material.color = color1;
            _moleHead.DOMoveY(_point1.position.y, 0.5f);
        }
        else if (Variables.zone == 2)
        {
            floor.material.color = color2;
            _moleHead.DOMoveY(_point2.position.y, 0.5f);
        }
        else if (Variables.zone == 3 && !Variables.chestSpawned)
        {
            floor.material.color = color3;
            _moleHead.DOMoveY(_point3.position.y, 0.5f);
        }
        else if (Variables.zone == 3 && Variables.chestSpawned)
        {
            _moleHead.DOMoveY(_point4.position.y, 0.5f);
        }
    }
}
