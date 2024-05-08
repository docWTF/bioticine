using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthBar : MonoBehaviour
{
    [field: SerializeField] public int MaxValue { get; private set; }
    [field: SerializeField] public int Value { get; private set; }

    [SerializeField] private RectTransform _upperBar;
    [SerializeField] private RectTransform _lowerBar;
    [SerializeField] private float _speed = 10f;

    private float _full;
    private float TargerWidth => Value * _full / MaxValue;

    private Coroutine adjustBarWidth;

    private void Start()
    {
        _full = _upperBar.rect.width;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Change(20);
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Change(-20);
        }
    }

    private IEnumerator AdjustBarWidth(int amount)
    {
        var suddenChangeBar = amount >= 0 ? _lowerBar : _upperBar;
        var slowChangeBar = amount >= 0 ? _upperBar : _lowerBar;
        suddenChangeBar.SetWidth(TargerWidth);
        while (Mathf.Abs(suddenChangeBar.rect.width - slowChangeBar.rect.width) > 1f)
        {
            var currentWidth = Mathf.Lerp(slowChangeBar.sizeDelta.x, TargerWidth, Time.deltaTime * _speed);
            slowChangeBar.sizeDelta = new Vector2(currentWidth, slowChangeBar.sizeDelta.y);
            yield return null;
        }
        slowChangeBar.SetWidth(TargerWidth);
    }

    public void Change(int amount)
    {
        Value = Mathf.Clamp(Value + amount, 0, MaxValue);
        if (adjustBarWidth != null)
        {
            StopCoroutine(adjustBarWidth);
        }
        adjustBarWidth = StartCoroutine(AdjustBarWidth(amount));
    }
}
public static class RectTransformExtension
{
    public static void SetWidth(this RectTransform t, float width)
    {
        t.sizeDelta = new Vector2(width, t.rect.height);
    }
}