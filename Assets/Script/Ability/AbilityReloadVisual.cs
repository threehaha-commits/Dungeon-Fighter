using System;
using UnityEngine.UIElements;

public class AbilityReloadVisual
{
    private Label ReloadTextLabel;

    public AbilityReloadVisual(Label reloadTextLabel)
    {
        ReloadTextLabel = reloadTextLabel;
    }

    public void RefreshVisualValue(float value)
    {
        ReloadTextLabel.text = $"{(float)Math.Round(value, 2)}";
    }
}