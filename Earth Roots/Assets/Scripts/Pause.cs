using UnityEngine;
public class Pause : MonoBehaviour
{
    public bool _isPaused;
    [SerializeField] private GameObject _pause;
    private void Start()
    {
        _isPaused = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isPaused && !Variables.gameOver)
        {
            _pause.SetActive(true);
            Time.timeScale = 0;
            _isPaused=true;
            Variables.isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isPaused && !Variables.gameOver)
        {
            _pause.SetActive(false);
            Time.timeScale = 1;
            _isPaused = false;
            Variables.isPaused = false;
        }
    }
}
