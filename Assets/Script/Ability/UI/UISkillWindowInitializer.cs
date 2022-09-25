using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class UISkillWindowInitializer : MonoBehaviour
{
    private Document Document;
    private VisualElement WindowWithLearnSkills;
    private const string WindowSwitcherButtonName = "WindowVisibleSwitcher";
    private Button WindowSwitcherButton;
    private readonly Button[] SkillButtonsFromSkillGroupUI = new Button[6];
    private readonly Button[] SlotButtons = new Button[6];
    
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
        new WindowWithSkillUpgradeSwitcher(rootWindowWithSkills);
        FindSkillSlots();
        FindEmptySlotFromSkillGroup(rootSkill);
        
        FindAndSetEventToButton(rootWindowWithSkills);
        
        FillSlots();
        
    }

    private void FillSlots()
    {
        for (int i = 0; i < SlotButtons.Length; i++)
        {
            var spriteFromSkillGroup = SkillButtonsFromSkillGroupUI[i].style.backgroundImage.value.sprite;
            SlotButtons[i].style.backgroundImage = new StyleBackground(spriteFromSkillGroup);
        }
    }

    private void FindSkillSlots()
    {
        var slotIndex = 0;
        var panel = WindowWithLearnSkills.Q<VisualElement>("PanelWithSkills");
        foreach (var slotButton in panel.Children())
        {
            SlotButtons[slotIndex] = slotButton.Q<Button>();
            slotIndex++;
        }
    }
    
    private void FindEmptySlotFromSkillGroup(VisualElement rootSkill)
    {
        var main = rootSkill.Q<VisualElement>("AbilityMain");
        var slotIndex = 0;
        foreach (var emptySlot in main.Children())
        {
            SkillButtonsFromSkillGroupUI[slotIndex] = emptySlot.Q<Button>();
            slotIndex++;
        }
    }

    private void FindAndSetEventToButton(VisualElement root)
    {
        WindowSwitcherButton = root.Q<Button>(WindowSwitcherButtonName);
        var openCloseButtonLogic = new WindowVisibleSwitcher(WindowWithLearnSkills);
        WindowSwitcherButton.clicked += openCloseButtonLogic.Visibly;
    }
}