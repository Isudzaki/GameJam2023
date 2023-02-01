using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] Transform spawnHome;
    [SerializeField] Transform spawnHole;
    void Start()
    {
        if (Variables.placeSpawn == 0)
        {
            Instantiate(_player, spawnHome.position, Quaternion.identity);
        }
        else if (Variables.placeSpawn == 1)
        {
            Instantiate(_player,spawnHole.position,Quaternion.identity);
        }
    }
}
