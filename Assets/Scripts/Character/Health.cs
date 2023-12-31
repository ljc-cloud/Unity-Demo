using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Health 每个角色都会携带这个组件
/// </summary>
public class Health : MonoBehaviour
{
    public float PH = 100f;
    public float MaxPH = 100f;
    public GameObject BotExplosion;

    public event Action<float, float, float> OnDamaged;

    public event Action OnPlayerDie;
    public event Action<Transform> OnEnemyDie;

    private void Update()
    {
        if (PH <= 0)
        {
            if (gameObject.CompareTag("Player"))
            {
                OnPlayerDie.Invoke();
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                OnEnemyDie.Invoke(transform);
            }
            CreateBotExplosion();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        if (PH > 0)
        {
            PH = Mathf.Max(PH - damage, 0);
            OnDamaged?.Invoke(MaxPH, PH, damage);
        }
    }

    private void CreateBotExplosion()
    {
        if (BotExplosion)
        {
            GameObject exp = Instantiate(BotExplosion, transform.position, transform.rotation);
            Destroy(exp, 1.3f);
        }
    }
}
