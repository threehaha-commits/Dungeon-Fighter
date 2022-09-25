using UnityEngine.UIElements;

public class WindowVisibleSwitcher
{
    private readonly VisualElement Window;
    
    public WindowVisibleSwitcher(VisualElement windowForOpen)
    {
        Window = windowForOpen;
    }

    public void Visibly()
    {
        Window.visible = !Window.visible;
    }
}