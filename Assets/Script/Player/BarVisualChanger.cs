using UnityEngine;
using UnityEngine.UIElements;

public interface IBarVisualChanger
{
    void ChangeBar(float currentHealth, float MaxHealth);
    void ChangeBar(float fullHpBarLenght, float currentHealth, float maxHealth);
}

public class BarVisualChanger : IBarVisualChanger
{
    private readonly ProgressBar Bar;
    private readonly SpriteRenderer Renderer;
    
    /// <summary>
    /// For player
    /// </summary>
    /// <param name="barName"> name bar which you want to change</param>
    public BarVisualChanger(UIDocument document, string barName)
    {
        var root = document.rootVisualElement;
        Bar = root.Q<ProgressBar>(barName);
    }

    /// <summary>
    /// For enemy
    /// </summary>
    public BarVisualChanger(SpriteRenderer renderer)
    {
        Renderer = renderer;
    }
    
    /// <summary>
    /// Bar - progress bar. For player or other progress bar
    /// </summary>
    public void ChangeBar(float current, float max)
    {
        Bar.title = $"{Mathf.RoundToInt(current)}/{max}";
        Bar.value = Bar.highValue * current / max;
    }

    /// <summary>
    /// Bar - spriteRenderer. For enemy or other SpriteRenderer bar
    /// </summary>
    /// <param name="fullRendererLength"> length renderer bar value</param>
    public void ChangeBar(float fullRendererLength, float current, float max)
    {
        var x = fullRendererLength * current / max;
        var heightBar = 1;
        Renderer.size = new Vector2(x, heightBar);
    }
}