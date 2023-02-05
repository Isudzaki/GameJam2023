using System.Collections;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(HungerTimer());
    }
    private void Update()
    {
        if (Variables.hungerTimer <= 0)
        {
            Variables.hungerTimer = 50;
            Variables.hunger--;
        }
    }
    IEnumerator HungerTimer()
    {
        yield return new WaitForSeconds(1);
        Variables.hungerTimer--;
        yield return HungerTimer();
    }
}
