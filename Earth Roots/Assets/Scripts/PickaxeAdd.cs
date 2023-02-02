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
    private void Start()
    {
        if (Variables.pickaxeAdd == 1)
        {
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if(_canAct && Input.GetKeyDown(KeyCode.E))
        {
            _sounds.clip = _button;
            _sounds.Play();
            DOTween.Sequence()
                .Append(transform.DOScale(new Vector3(0, 0, 0), 0.25f))
                .AppendCallback(AddPickaxe);
        }
    }
    private void AddPickaxe()
    {
        Variables.pickaxeBreak = 1;
        Variables.pickaxeAdd = 1;
        PlayerPrefs.SetInt("PickAxe",1);
        gameObject.SetActive(false);
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
    private void SpriteFade()
    {
        _ESprite.gameObject.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DOTween.Sequence()
         .Append(_ESprite.DOFade(0, 0.3f))
         .AppendCallback(SpriteFade);
            _canAct = false;
            for (int i = 0; i < _meshes.Length; i++)
            {
                _meshes[i].material.SetFloat("_OutlineWidth", 0);
            }
        }
    }

}
