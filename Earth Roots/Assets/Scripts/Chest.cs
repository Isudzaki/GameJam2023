using UnityEngine;
using DG.Tweening;

public class Chest : MonoBehaviour
{
    [SerializeField] AudioSource _sounds;
    [SerializeField] AudioClip _winSound;
    [SerializeField] AudioClip _trigger;
    [SerializeField] SpriteRenderer _ESprite;
    [SerializeField] AudioSource _chest;
    private Tween _fadeAnim;
    private bool canAct;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canAct)
        {
            _sounds.clip = _winSound;
             Variables.win=true;
            _sounds.Play();
            canAct = false;
            _chest.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _ESprite.gameObject.SetActive(true);
            _fadeAnim.Kill();
            _fadeAnim = _ESprite.DOFade(1, 0.5f);
            canAct = true;
            _sounds.clip = _trigger;
            _sounds.Play();
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
            _fadeAnim.Kill();
            DOTween.Sequence()
                .Append(_fadeAnim = _ESprite.DOFade(0, 0.3f))
                .AppendCallback(SpriteFade);
            canAct = false;
        }
    }
}

