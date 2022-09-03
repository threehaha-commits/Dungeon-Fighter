using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class DropWindowVisible : MonoBehaviour
{
    private UIDocument Document;
    private VisualElement Window;
    private Button CloseButton;
    
    [Inject]
    public void Construct(Document document)
    {
        Document = document.DescriptionWindow;
    }

    private void Start()
    {
        var root = Document.rootVisualElement;
        Window = root.Q<VisualElement>("DropWindow");
        CloseButton = root.Q<Button>("CloseButton");
        CloseButton.clicked += () =>
        {
            Window.visible = false;
        };
    }
    
    public void Open()
    {
        Window.visible = true;
    }
}