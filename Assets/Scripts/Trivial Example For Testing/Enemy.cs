using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    private float _health; //private: only this script can get/set this value
    public float Health => _health; //public getter that returns the private _health value, gets but doesn't set

    private bool _isDead = false;
    public bool IsDead => _isDead;
    public Gradient col;
    public float maxHealth = 10;

    private SpriteRenderer sr;

    public void Damage(float amount)
    {
        UpdateVisuals();
        UpdateHealth(amount);
    }

    public void UpdateHealth(float amount)
    {
        _health -= amount;

        if (_health < 0)
        {
            _health = 0;
            _isDead = true;
        }
    }

    void Start()
    {
        _health = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        UpdateVisuals();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Damage(1);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Damage(-1);
        }
    }

    void UpdateVisuals()
    {
        sr.color = col.Evaluate(EnemyData.Map(_health, 0, maxHealth, 0, 1));
    }
}

public class EnemyData
{
    public static float Map(float value, float min1, float max1, float min2, float max2)
    {
        return min2 + (value - min1) * (max2 - min2) / (max1 - min1);
    }
}
