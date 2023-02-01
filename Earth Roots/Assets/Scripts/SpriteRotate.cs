using UnityEngine;

public class SpriteRotate : MonoBehaviour
{
    void Update()
    {
        transform.rotation=Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
    }
}
