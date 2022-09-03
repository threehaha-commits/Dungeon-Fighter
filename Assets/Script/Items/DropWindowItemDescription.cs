using UnityEngine.UIElements;

public class DropWindowItemDescription
{
    private bool WindowAcitve = false;
    private VisualElement Window;
    
    public DropWindowItemDescription(UIDocument document)
    {
        var root = document.rootVisualElement;
        Window = root.Q<VisualElement>("DescriptionDindow");
    }
    
    public void SetDescription(Button button, Item item)
    {
        var label = Window.Q<Label>("DescriptionText");
        button.clicked += () =>
        {
            label.text = item.GetDescriptionInfo();
            DescriptionWindowEnable();
        };
    }

    private void DescriptionWindowEnable()
    {
        WindowAcitve = !WindowAcitve;
        Window.visible = WindowAcitve;
    }
}