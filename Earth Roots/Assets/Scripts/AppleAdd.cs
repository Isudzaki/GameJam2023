using UnityEngine;
using DG.Tweening;

public class AppleAdd : MonoBehaviour
{
    [SerializeField] MeshRenderer _mesh;
    [SerializeField] AudioSource _sounds;
    [SerializeField] AudioClip _button;
    [SerializeField] AudioClip _trigger;
    [SerializeField] SpriteRenderer _ESprite;
    private bool _canAct;
    private void Update()
    {
        if (Variables.coins >= 5)
        {
            _mesh.material.SetColor("_OutlineColor", Color.green);
        }
        else
        {
            _mesh.material.SetColor("_OutlineColor", Color.red);
        }
        if (Input.GetKeyDown(KeyCode.E) && Variables.coins >= 5 && Variables.hunger < 5 && _canAct)
        {
            Variables.coins -= 5;
            _sounds.clip = _button;
            _sounds.Play();
            Variables.hunger++;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _canAct=true;
            _ESprite.gameObject.SetActive(true);
            _ESprite.DOFade(1, 0.5f);
            _sounds.clip = _trigger;
            _sounds.Play();
            _mesh.material.SetFloat("_OutlineWidth", 0.001f);
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
            _canAct = false;
            DOTween.Sequence()
         .Append(_ESprite.DOFade(0, 0.3f))
         .AppendCallback(SpriteFade);
          _mesh.material.SetFloat("_OutlineWidth", 0);
        }
    }
}
