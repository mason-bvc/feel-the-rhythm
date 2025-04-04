using UnityEngine;

public class LevelCompleter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (FindAnyObjectByType<Timer>())
        {
            FindAnyObjectByType<Timer>().updateTimer = false;
            Destroy(gameObject);
        }
    }
}
