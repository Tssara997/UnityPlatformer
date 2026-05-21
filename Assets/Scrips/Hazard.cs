using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var health = other.GetComponent<PlayerLife>();
        if (health != null) health.TakeDamage(200);
    }
}
