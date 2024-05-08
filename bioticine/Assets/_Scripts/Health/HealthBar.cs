using System.Collections;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem; // Reference to the HealthSystem

    [SerializeField] private RectTransform _upperBar;
    [SerializeField] private RectTransform _lowerBar;
    [SerializeField] private float _speed = 10f;

    private float _full;

    private float TargetWidth => healthSystem.Hp * _full / healthSystem.MaxHp;

    private Coroutine adjustBarWidth;

    private void Start()
    {
        _full = _upperBar.rect.width;

        // Subscribe to events in HealthSystem
        healthSystem.Damaged.AddListener(UpdateHealthBar);
        healthSystem.Healed.AddListener(UpdateHealthBar);
        healthSystem.Died.AddListener(UpdateHealthBar);
    }

    private IEnumerator AdjustBarWidth(int amount)
    {
        var suddenChangeBar = amount >= 0 ? _lowerBar : _upperBar;
        var slowChangeBar = amount >= 0 ? _upperBar : _lowerBar;
        suddenChangeBar.SetWidth(TargetWidth);
        while (Mathf.Abs(suddenChangeBar.rect.width - slowChangeBar.rect.width) > 1f)
        {
            var currentWidth = Mathf.Lerp(slowChangeBar.sizeDelta.x, TargetWidth, Time.deltaTime * _speed);
            slowChangeBar.sizeDelta = new Vector2(currentWidth, slowChangeBar.sizeDelta.y);
            yield return null;
        }
        slowChangeBar.SetWidth(TargetWidth);
    }

    // Update health bar visuals when health changes
    private void UpdateHealthBar(float currentHealth)
    {
        int currentHealthInt = Mathf.RoundToInt(currentHealth); // Convert float to int
        if (adjustBarWidth != null)
        {
            StopCoroutine(adjustBarWidth);
        }
        adjustBarWidth = StartCoroutine(AdjustBarWidth(currentHealthInt - Mathf.RoundToInt(healthSystem.Hp)));
    }

}

public static class RectTransformExtension
{
    public static void SetWidth(this RectTransform t, float width)
    {
        t.sizeDelta = new Vector2(width, t.rect.height);
    }
}
