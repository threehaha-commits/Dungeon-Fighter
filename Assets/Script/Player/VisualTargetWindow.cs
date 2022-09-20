using UnityEngine;
using UnityEngine.UIElements;

public class VisualTargetWindow
{
    private readonly VisualElement Window;
    private readonly Label TargetName;
    
    public VisualTargetWindow(Document Document)
    {
        var root = Document.Target.rootVisualElement;
        Window = root.Q<VisualElement>("Window");
        TargetName = root.Q<Label>("TargetName");
        Window.visible = false;
    }

    public void OpenWithTarget(Transform target)
    {
        Window.visible = true;
        TargetName.text = target.name;
        EnemyBarColorChanger.ChangeColorBar(target);
    }

    public void Close()
    {
        Window.visible = false;
        TargetName.text = "null";
    }
}