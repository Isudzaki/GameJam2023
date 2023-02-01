using UnityEngine;

public class AxeTrigger : MonoBehaviour
{
    public bool _isMinerZone;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Axe")
        {
            _isMinerZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Axe")
        {
            _isMinerZone = false;
        }
    }
}
