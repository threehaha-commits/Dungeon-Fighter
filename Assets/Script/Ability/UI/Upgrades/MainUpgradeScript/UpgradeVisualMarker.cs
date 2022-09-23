using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeVisualMarker : IUpgradableEffect
{
    private readonly Queue<VisualElement> Markers = new();
    private const string VisualMarkersBoxName = "VisualMarkersBox";
    
    public UpgradeVisualMarker(VisualElement root)
    {
        var parentPanel = root.Q<VisualElement>(VisualMarkersBoxName);
        foreach (var visualMarker in parentPanel.Children())
        {
            Markers.Enqueue(visualMarker);
        }
    }
    
    void IUpgradableEffect.Upgrade()
    {
        Markers.Dequeue().style.backgroundColor = new StyleColor(Color.green);
    }
}