using System.Collections.Generic;
using UnityEngine.UIElements;

public class MouseOverUI
{
    private static List<VisualElement> ElementList = new List<VisualElement>();
    public static bool On { get; private set; }

    public static void AddElement(UIDocument document, string parentName)
    {
        var root = document.rootVisualElement;
        ElementList.Add(root.Q<VisualElement>(parentName));
        int elementCount = ElementList.Count - 1;
        RegisterMouseEnterCallback(elementCount);
        RegisterMouseLeaveCallback(elementCount);
    }

    private static void MouseEnterCallback(MouseEnterEvent evt)
    {
        if (MouseIsOver(evt.target))
            On = true;
    }

    private static void MouseLeaveCallback(MouseLeaveEvent evt)
    {
        if (MouseIsOver(evt.target))
            On = false;
    }

    private static void RegisterMouseEnterCallback(int id)
    {
        ElementList[id].RegisterCallback<MouseEnterEvent>(MouseEnterCallback);
    }

    private static void RegisterMouseLeaveCallback(int id)
    {
        ElementList[id].RegisterCallback<MouseLeaveEvent>(MouseLeaveCallback);
    }

    private static bool MouseIsOver(IEventHandler target) 
    {
        for(int i = 0; i < ElementList.Count; i++)
        {
            if (target == ElementList[i])
                return true;
        }
        return false;
    }
}
