using UnityEngine;

public class ChestSpawn : MonoBehaviour
{
    [SerializeField] GameObject _chest;
    void Update()
    {
        if(Variables.deep >= 20f)
        {
            _chest.SetActive(true);
            Variables.chestSpawned = true;
        }
    }
}
