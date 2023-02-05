using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    [SerializeField] Text _text;
    private void Start()
    {
        if (Variables.interactQuest == 0 && Variables.questNum == 1)
        {
            DOTween.Sequence()
               .Append(_text.DOText("", 0.25f))
               .Append(_text.DOText("TASK:Talk to the beaver", 1f));
        }
        else if (Variables.interactQuest == 1 && Variables.questNum == 2)
        {
            DOTween.Sequence()
               .Append(_text.DOText("", 0.25f))
               .Append(_text.DOText("TASK:Buy a pickaxe in the IMPLEMENTS SHOP", 1f));
        }
        else if (Variables.pickaxeAdd == 1 && Variables.questNum == 3)
        {
            DOTween.Sequence()
               .Append(_text.DOText("", 0.25f))
               .Append(_text.DOText("TASK:Go to the cave", 1f));
        }
        else if (Variables.interactCave == 1 && Variables.questNum == 4)
        {
            DOTween.Sequence()
               .Append(_text.DOText("", 0.25f))
               .Append(_text.DOText("TASK:Dig down(LEFT MOUSE BUTTON)", 1f));
        }
        else if (Variables.deep >= 20f && Variables.questNum == 5)
        {
            DOTween.Sequence()
               .Append(_text.DOText("", 0.25f))
               .Append(_text.DOText("TASK:Open the TIME CAPSULE", 1f));
        }
    }
    void Update()
    {
        if (Variables.interactQuest == 0 && Variables.questNum==0)
        {
            Variables.questNum = 1;
            DOTween.Sequence()
               .Append(_text.DOText("", 0.25f))
               .Append(_text.DOText("TASK:Talk to the beaver", 1f));
        }
        else if(Variables.interactQuest == 1 && Variables.questNum==1)
        {
            Variables.questNum = 2;
            DOTween.Sequence()
               .Append(_text.DOText("", 0.25f))
               .Append(_text.DOText("TASK:Buy a pickaxe in the IMPLEMENTS SHOP", 1f));
        }
        else if (Variables.pickaxeAdd == 1 && Variables.questNum == 2)
        {
            Variables.questNum = 3;
            DOTween.Sequence()
               .Append(_text.DOText("", 0.25f))
               .Append(_text.DOText("TASK:Go to the cave", 1f));
        }
        else if (Variables.interactCave == 1 && Variables.questNum == 3)
        {
            Variables.questNum = 4;
            DOTween.Sequence()
               .Append(_text.DOText("", 0.25f))
               .Append(_text.DOText("TASK:Dig down(LEFT MOUSE BUTTON)", 1f));
        }
        else if (Variables.deep >= 20f && Variables.questNum == 4)
        {
            Variables.questNum = 5;
            DOTween.Sequence()
               .Append(_text.DOText("", 0.25f))
               .Append(_text.DOText("TASK:Open the TIME CAPSULE", 1f));
        }
    }
}
