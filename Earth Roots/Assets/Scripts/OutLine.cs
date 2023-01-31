using UnityEngine;

public class OutLine : MonoBehaviour
{
    private Material _mat;
    private void Start()
    {
        _mat = gameObject.GetComponent<MeshRenderer>().material;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _mat.SetFloat("_OutlineWidth", 0.01f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _mat.SetFloat("_OutlineWidth", 0f);
        }
    }
}
