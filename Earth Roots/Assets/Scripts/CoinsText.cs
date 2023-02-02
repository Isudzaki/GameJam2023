using UnityEngine;
using UnityEngine.UI;

public class CoinsText : MonoBehaviour
{
    private Text _txt;
    private void Start()
    {
        _txt = GetComponent<Text>();
    }
    private void Update()
    {
        _txt.text = Variables.coins.ToString();
    }
}
