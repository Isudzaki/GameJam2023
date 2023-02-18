using UnityEngine;
using DG.Tweening;

public class PickaxeAdd : MonoBehaviour
{
    [SerializeField] MeshRenderer[] _meshes;
    [SerializeField] AudioSource _sounds;
    [SerializeField] AudioClip _button;
    [SerializeField] AudioClip _trigger;
    [SerializeField] SpriteRenderer _ESprite;
    private bool _canAct;
    private void Update()
    {
        if (Variables.coins >= 10 && Variables.pickaxeAdd == 0)
        {
            for (int i = 0; i < _meshes.Length; i++)
            {
                _meshes[i].material.SetColor("_OutlineColor", Color.green);
            }
        }
        else
        {
            for (int i = 0; i < _meshes.Length; i++)
            {
                _meshes[i].material.SetColor("_OutlineColor", Color.red);
            }
        }
        if(_canAct && Input.GetKeyDown(KeyCode.E) && Variables.coins>=10 && Variables.pickaxeAdd==0)
        {
            Variables.coins -= 10;
            _sounds.clip = _button;
            _sounds.Play();
            AddPickaxe();
        }
    }
    private void AddPickaxe()
    {
        Variables.pickaxeBreak = 1;
        Variables.pickaxeAdd = 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _ESprite.gameObject.SetActive(true);
            _ESprite.DOFade(1, 0.5f);
            _sounds.clip = _trigger;
            _sounds.Play();
            _canAct = true;
            for(int i = 0; i < _meshes.Length; i++)
            {
                _meshes[i].material.SetFloat("_OutlineWidth", 0.003f);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DOTween.Sequence()
         .Append(_ESprite.DOFade(0, 0.3f));
            _canAct = false;
            for (int i = 0; i < _meshes.Length; i++)
            {
                _meshes[i].material.SetFloat("_OutlineWidth", 0);
            }
        }
    }

}
