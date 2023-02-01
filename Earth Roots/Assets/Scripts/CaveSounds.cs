using System.Collections;
using UnityEngine;

public class CaveSounds : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _clips;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(CaveSound());
    }
    private IEnumerator CaveSound()
    {
        yield return new WaitForSeconds(Random.Range(5,20));
        _audioSource.clip = _clips[Random.Range(0,5)];
        _audioSource.pitch = Random.Range(0.6f, 1.2f);
        _audioSource.Play();
        StartCoroutine(CaveSound());
    }
}
