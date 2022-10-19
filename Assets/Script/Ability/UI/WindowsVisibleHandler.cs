using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UiSkillUpgrade;

public class WindowsVisibleHandler
{
    private readonly VisualElement Window;
    private readonly Button[] LeftPanelButtons;
    private readonly List<VisualElement> ParentSkillsWindows = new();
    private UnityAction AllWindowsVisibleControl;
    
    public WindowsVisibleHandler(VisualElement windowForOpen, Button[] leftPanelButtons, List<VisualElement> parentSkillsWindows)
    {
        Window = windowForOpen;
        LeftPanelButtons = leftPanelButtons;
        ParentSkillsWindows = parentSkillsWindows;
        LinkButtonEventWithWindows();
    }
    
    public void MainWindowVisible()
    {
        Window.visible = !Window.visible;
        if(Window.visible == false)
            AllWindowsVisibleControl.Invoke();
    }
    
    private void LinkButtonEventWithWindows()
    {
        for (int i = 0; i < ParentSkillsWindows.Count; i++)
            new EventSetter(LeftPanelButtons[i], ParentSkillsWindows[i], ParentSkillsWindows, ref AllWindowsVisibleControl);
    }
}