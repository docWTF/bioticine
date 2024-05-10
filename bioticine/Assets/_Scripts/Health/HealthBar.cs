using UnityEngine;
using System.Collections;
using System;
using static UnityEngine.Rendering.DebugUI;

public class HealthBar : MonoBehaviour
{
    public EnemyStats enemyStats; // Reference to the HealthSystem

    [SerializeField] private RectTransform _upperBar;
    [SerializeField] private RectTransform _lowerBar;
    [SerializeField] private float _speed = 0.05f;

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

    // Update health bar visuals when health changes
    public void UpdateHealthBar(float amount)
    {

        adjustBarWidth = StartCoroutine(AdjustBarWidth(amount));
    }
    private IEnumerator AdjustBarWidth(float amount)
    {
        var suddenChangeBar = amount < 0 ? _upperBar : _lowerBar;
        var slowChangeBar = amount < 0 ? _lowerBar : _upperBar;
        float targetWidth = TargetWidth();

        suddenChangeBar.SetWidth(targetWidth);

        float currentWidth = slowChangeBar.sizeDelta.x;
        float timeElapsed = 0;  // Track the time elapsed for the interpolation
        float lerpDuration = 1.0f / _speed;  // Calculate the total duration of the lerp based on the speed

        while (timeElapsed < lerpDuration)
        {
            currentWidth = Mathf.Lerp(slowChangeBar.sizeDelta.x, targetWidth, timeElapsed / lerpDuration);
            slowChangeBar.SetWidth(currentWidth);
            timeElapsed += Time.deltaTime;  // Increment the time elapsed by the time per frame
            yield return null;
        }

        slowChangeBar.SetWidth(targetWidth);  // Ensure it ends exactly at target width
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
