using UnityEngine;
using System.Collections;
using System;
using static UnityEngine.Rendering.DebugUI;

public class HealthBar : MonoBehaviour
{
    public EnemyStats enemyStats; // Reference to the HealthSystem

    [SerializeField] private RectTransform _upperBar;
    [SerializeField] private RectTransform _lowerBar;
    [SerializeField] private float _speed = 10f;

    private float _full;
   
    private Coroutine adjustBarWidth;

    private void Awake()
    {
        enemyStats = GetComponentInParent<EnemyStats>();
    }

    private void Start()
    {
        _full = _upperBar.rect.width;

        // Update health bar initially
        UpdateHealthBar(enemyStats.Hp);
    }

    private IEnumerator AdjustBarWidth(float amount)
    {
        var suddenChangeBar = amount < 0 ? _upperBar : _lowerBar;
        var slowChangeBar = amount < 0 ? _lowerBar : _upperBar;
        float targetWidth = TargetWidth();

        suddenChangeBar.SetWidth(targetWidth);

        while (Mathf.Abs(slowChangeBar.sizeDelta.x - targetWidth) > 1f)
        {
            var currentWidth = Mathf.Lerp(slowChangeBar.sizeDelta.x, targetWidth, Time.deltaTime * _speed);
            slowChangeBar.SetWidth(currentWidth);
            yield return null;
        }
        slowChangeBar.SetWidth(targetWidth);
    }


    // Update health bar visuals when health changes
    public void UpdateHealthBar(float amount)
    {
        if (adjustBarWidth != null)
        {
            StopCoroutine(adjustBarWidth);
        }
        adjustBarWidth = StartCoroutine(AdjustBarWidth(amount));
    }


    private float TargetWidth()
    {
        return enemyStats.Hp * _full / enemyStats.MaxHp;
    }
}

public static class RectTransformExtension
{
    public static void SetWidth(this RectTransform t, float width)
    {
        t.sizeDelta = new Vector2(width, t.rect.height);
    }
}
