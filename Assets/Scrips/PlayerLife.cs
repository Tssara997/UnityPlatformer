using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private HealthLogic _health;

    void Awake()
    {
        _health = new HealthLogic(100);
    }

    public void TakeDamage(int dmg)
    {
        _health.TakeDamage(dmg);
        if (_health.IsDead)
            Die();
    }

    public void Die()
    {
        transform.position = new Vector3(0, 0, 0);
    }
}

public class HealthLogic
{
    public int Hp { get; private set; }
    public bool IsDead => Hp <= 0;

    public HealthLogic(int hp)
    {
        Hp = hp;
    }

    public void TakeDamage(int dmg)
    {
        Hp -= dmg;
    }
}