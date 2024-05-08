using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float _maxHp = 100;
    private float _hp;

    public float MaxHp => _maxHp;
    public float Hp
    {
        get => _hp;
        private set
        {
            var isDamage = value < _hp;
            _hp = Mathf.Clamp(value, min: 0, _maxHp);
            if (isDamage)
            {
                Damaged?.Invoke(_hp);
            }
            else
            {
                Healed?.Invoke(_hp);
            }

            if (_hp <= 0)
            {
                Died?.Invoke(_hp);
            }
        }
    }

    public UnityEvent<float> Damaged;
    public UnityEvent<float> Healed;
    public UnityEvent<float> Died;
    private void Awake()
    {
        _hp = _maxHp;
    }

    public void Damage(int amount) => Hp -= amount;
    public void Heal(int amount) => Hp += amount;
    public void HealMax(int amount) => Hp = _maxHp;
    public void Kill() => Hp = 0;
    public void Adjust(int value) => Hp = value;

}

