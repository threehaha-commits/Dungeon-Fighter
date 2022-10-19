using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class UISkillUpgradesInitializer : MonoBehaviour
{
    private const string WindowSwitcherButtonName = "WindowVisibleSwitcher";
    private const string ParentSkillsWindowName = "ParentSkillsWindow";
    private Document Document;
    private VisualElement WindowWithLearnSkills;
    private Button WindowSwitcherButton;
    private readonly Button[] SlotButtons = new Button[6];
    private readonly List<VisualElement> ParentSkillsWindows = new();
    
    [Inject]
    private void Construct(Document document)
    {
        Document = document;
        MouseOverUI.AddElement(document.WindowWithSkills, "MainWindow");
        MouseOverUI.AddElement(document.WindowWithSkills, WindowSwitcherButtonName);
    }

    private void Start()
    {
        var rootWindowWithSkills = Document.WindowWithSkills.rootVisualElement;
        var rootSkill = Document.Skill.rootVisualElement;
        WindowWithLearnSkills = rootWindowWithSkills.Q<VisualElement>("MainWindow");
        
        FindLeftPanelButtons();
        
        FindWindowsWithUpgrades(rootWindowWithSkills);
        FindAndSetEventToButton(rootWindowWithSkills);
        
        SetSpriteLeftPanelButtons(rootSkill);
        
    }

    private void SetSpriteLeftPanelButtons(VisualElement rootSkill)
    {
        var buttonsFromAbilityGroup = GetButtonsFromAbilityGroup(rootSkill);
        for (int i = 0; i < SlotButtons.Length; i++)
        {
            var spriteFromSkillGroup = buttonsFromAbilityGroup[i].style.backgroundImage.value.sprite;
            SlotButtons[i].style.backgroundImage = new StyleBackground(spriteFromSkillGroup);
        }
    }

    private void FindLeftPanelButtons()
    {
        var slotIndex = 0;
        var panel = WindowWithLearnSkills.Q<VisualElement>("PanelWithSkills");
        foreach (var slotButton in panel.Children())
        {
            SlotButtons[slotIndex] = slotButton.Q<Button>();
            slotIndex++;
        }
    }
    
    private void FindWindowsWithUpgrades(VisualElement rootWindowWithSkills)
    {
        var parentSkillsWindowName = rootWindowWithSkills.Q<VisualElement>(ParentSkillsWindowName);
        foreach (var window in parentSkillsWindowName.Children())
            ParentSkillsWindows.Add(window);
    }
    
    private Button[] GetButtonsFromAbilityGroup(VisualElement rootSkill)
    {
        var main = rootSkill.Q<VisualElement>("AbilityMain");
        var buttons = new Button[6];
        var slotIndex = 0;
        foreach (var emptySlot in main.Children())
        {
            buttons[slotIndex] = emptySlot.Q<Button>();
            slotIndex++;
        }

        return buttons;
    }

    private void FindAndSetEventToButton(VisualElement root)
    {
        WindowSwitcherButton = root.Q<Button>(WindowSwitcherButtonName);
        var openCloseButtonLogic = new WindowsVisibleHandler(WindowWithLearnSkills, SlotButtons, ParentSkillsWindows);
        WindowSwitcherButton.clicked += openCloseButtonLogic.MainWindowVisible;
    }
}