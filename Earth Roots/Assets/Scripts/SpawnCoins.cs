using System.Collections;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    [SerializeField] private GameObject _coin;
    public bool _canAdd;
    public GameObject newCoin;
    void Start()
    {
        StartCoroutine(Spawn());
        _canAdd = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            _canAdd = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _canAdd = true;
        }
    }
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(5, 10));
        if (_canAdd)
        {
            newCoin = Instantiate(_coin, gameObject.transform.position, Quaternion.identity);
            newCoin.transform.parent = gameObject.transform;
        }
        yield return Spawn();
    }
}
