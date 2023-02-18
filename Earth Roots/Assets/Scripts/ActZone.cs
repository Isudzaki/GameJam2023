using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ActZone : MonoBehaviour
{
    private bool canAct;
    [SerializeField] AudioSource _sounds;
    [SerializeField] AudioClip _button;
    [SerializeField] AudioClip _trigger;
    [SerializeField] SpriteRenderer _Cloud;
    [SerializeField] SpriteRenderer _Ebutton;
    [SerializeField] Text _textCloud;
    [SerializeField] string[] _replicas;
    [SerializeField] string _replicaNotCompleted;
    public int replic=0;
    private bool _canSkip;
    private Tween _fadeAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _canSkip=true;
            _fadeAnim.Kill();
            _Ebutton.DOFade(1, 0.5f);
            replic = 1;
            _Cloud.gameObject.SetActive(true);
            if (Variables.interactQuest == 0)
                DOTween.Sequence()
                  .Append(_fadeAnim = _Cloud.DOFade(1, 0.5f))
                  .AppendCallback(TextWrite);
            else
                DOTween.Sequence()
            .Append(_Cloud.DOFade(1, 0.5f))
            .AppendCallback(TextWriteQuest);
            _textCloud.DOFade(1, 0.5f);
            _sounds.clip = _trigger;
            _sounds.Play();
        }
    }
    private void Update()
    {
        if (Variables.interactQuest == 0 && _canSkip)
        {
            if (Input.GetKeyDown(KeyCode.Space) && replic < _replicas.Length)
            {
                _sounds.clip = _trigger;
                _sounds.Play();
                replic++;
                TextWrite();
            }
            else if (Input.GetKeyDown(KeyCode.Space) && replic == _replicas.Length)
            {
                _sounds.clip = _trigger;
                _sounds.Play();
                Variables.interactQuest = 1;
                TextFade();
            }
        }
        else if (Variables.interactQuest == 1 && _canSkip)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _sounds.clip = _trigger;
                _sounds.Play();
                TextFade();
            }
        }
    }
    private void TextWriteQuest()
    {
        DOTween.Sequence()
          .Append(_textCloud.DOText("", 0.25f))
          .Append(_textCloud.DOText(_replicaNotCompleted, 1f));
    }
    private void TextWrite()
    {
        DOTween.Sequence()
          .Append(_textCloud.DOText("", 0.25f))
          .Append(_textCloud.DOText(_replicas[replic - 1], 1f));
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _canSkip = false;
            TextFade();
        }
    }
    private void TextFade()
    {
        _fadeAnim.Kill();
        _textCloud.DOFade(0, 0.3f);
        _Ebutton.DOFade(0, 0.3f);
        DOTween.Sequence()
            .Append(_fadeAnim = _Cloud.DOFade(0, 0.3f));
    }
}
