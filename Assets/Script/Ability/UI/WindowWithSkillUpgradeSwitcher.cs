using System.Collections.Generic;
using UnityEngine.UIElements;

public class WindowWithSkillUpgradeSwitcher
{
    private const string PanelWithSkillsName = "PanelWithSkills";
    private const string ParentSkillsWindowName = "ParentSkillsWindow";
    private readonly List<Button> ButtonsFromPanelWithSkills = new();
    private readonly List<VisualElement> ParentSkillsWindows = new();

    public WindowWithSkillUpgradeSwitcher(VisualElement rootWindowWithSkills)
    {
        FindSkillButtons(rootWindowWithSkills);

        FindWindowsWithUpgrades(rootWindowWithSkills);

        LinkButtonEventWithWindows();
    }

    private void FindSkillButtons(VisualElement rootWindowWithSkills)
    {
        var panelWithSkillsName = rootWindowWithSkills.Q<VisualElement>(PanelWithSkillsName);
        foreach (var skillButton in panelWithSkillsName.Children())
            ButtonsFromPanelWithSkills.Add(skillButton.Q<Button>());
    }
    
    private void FindWindowsWithUpgrades(VisualElement rootWindowWithSkills)
    {
        var parentSkillsWindowName = rootWindowWithSkills.Q<VisualElement>(ParentSkillsWindowName);
        foreach (var window in parentSkillsWindowName.Children())
            ParentSkillsWindows.Add(window);
    }

    private void LinkButtonEventWithWindows()
    {
        for (int i = 0; i < ParentSkillsWindows.Count; i++)
            new EventSetter(ButtonsFromPanelWithSkills[i], ParentSkillsWindows[i], ParentSkillsWindows);
    }

    private class EventSetter
    {
        private readonly VisualElement UpgradeWindow;
        private readonly List<VisualElement> ParentSkillsWindows = new();
        
        public EventSetter(Button button, VisualElement window, List<VisualElement> parentSkillsWindows)
        {
            UpgradeWindow = window;
            ParentSkillsWindows = parentSkillsWindows;
            button.clicked += WindowVisible;
        }

        private void WindowVisible()
        {
            foreach (var window in ParentSkillsWindows)
            {
                window.visible = false;
            }

            UpgradeWindow.visible = true;
        }
    }
}