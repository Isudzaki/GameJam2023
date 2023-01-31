using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Teleport : MonoBehaviour
{
    [SerializeField] Transform _effect;
    [SerializeField] AudioSource _music;
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
            canAct = true;
            //e img show
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
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
