using UnityEngine.UIElements;

public class OpenCloseButtonLogic
{
    private readonly VisualElement Window;
    private bool Enable = false;
    
    public OpenCloseButtonLogic(VisualElement windowForOpen)
    {
        Window = windowForOpen;
    }

    public void Visibly()
    {
        Enable = !Enable;
        Window.visible = Enable;
    }
}