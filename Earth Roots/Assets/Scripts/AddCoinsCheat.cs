using UnityEngine;

public class AddCoinsCheat : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Debug.isDebugBuild) Variables.coins++;
    }
}
