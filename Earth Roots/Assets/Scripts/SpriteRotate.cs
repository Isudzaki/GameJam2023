using UnityEngine;

public class SpriteRotate : MonoBehaviour
{
    void Update()
    {
        transform.rotation=Quaternion.Euler(transform.rotation.x, Camera.main.transform.rotation.eulerAngles.y, transform.rotation.z);
    }
}
