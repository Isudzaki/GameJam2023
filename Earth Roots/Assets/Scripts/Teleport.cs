using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Teleport : MonoBehaviour
{
    [SerializeField] Transform _effect;
    [SerializeField] AudioSource _music;
    [SerializeField] AudioSource _sounds;
    [SerializeField] AudioClip _button;
    [SerializeField] AudioClip _trigger;
    [SerializeField] SpriteRenderer _ESprite;
    private SceneChangerEffect _sceneEffect;
    private Scene _scene;
    private bool canAct;

    private void Start()
    {
        _sceneEffect = _effect.GetComponent<SceneChangerEffect>();
        _scene = SceneManager.GetActiveScene();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canAct)
        {
            _sounds.clip = _button;
            _sounds.Play();
            if (_scene.name == "TopLevel")
            {
                _music.DOPitch(0.25f, 1);
            }
            else
            {
                _music.DOPitch(1.75f, 1);
            }
            canAct = false;
            _sceneEffect.FadeIn();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _ESprite.gameObject.SetActive(true);
            _ESprite.DOFade(1, 0.5f);
            canAct = true;
            _sounds.clip = _trigger;
            _sounds.Play();
            //e img show
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
            canAct = false;
            //e img hide
        }
    }
    public void SceneChange()
    {
        if (_scene.name == "TopLevel")
        {
            SceneManager.LoadScene("BottomLevel");
        }
        else
        {
            SceneManager.LoadScene("TopLevel");
        }
    }
}
