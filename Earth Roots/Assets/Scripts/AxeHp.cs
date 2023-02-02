using UnityEngine;

public class AxeHp : MonoBehaviour
{
    private RectTransform _axeBar;
    private void Start()
    {
        _axeBar = GetComponent<RectTransform>();
    }
    private void Update()
    {
        _axeBar.localScale = new Vector3(Variables.pickaxeBreak, _axeBar.localScale.y, _axeBar.localScale.z);
    }
}
