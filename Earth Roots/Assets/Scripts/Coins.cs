using UnityEngine;

public class Coins : MonoBehaviour
{
    private AudioSource _sounds;
    [SerializeField]private AudioClip _coin;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _sounds = GameObject.FindGameObjectWithTag("Sounds").GetComponent<AudioSource>();
            _sounds.clip = _coin;
            _sounds.Play();
            Variables.coins++;
            Destroy(gameObject);
        }
    }
}
