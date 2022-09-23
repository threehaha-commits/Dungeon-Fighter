using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class AbilityVisualInterface : MonoBehaviour
{
    private UIDocument Document;
    [Inject] private IAbility[] Abilities;

    [Inject]
    private void Construct(Document document)
    {
        Document = document.Skill;
    }
    
    private void Awake()
    {
        RegisterAbilityInTheInterface();
    }

    private void RegisterAbilityInTheInterface()
    {
        var root = Document.rootVisualElement;
        var main = root.Q<VisualElement>("AbilityMain");
        int index = 0;
        foreach (var element in main.Children())
        {
            if(index == Abilities.Length)
                break;
            if (element.name == "EmptySlot")
            {
                var button = SetAbilityIcon(Abilities[index].InfoAbility, element);

                SetAbilityManaCost(Abilities[index].InfoAbility, element);

                ChangeSlotNameToInfoAbilityName(element, index);

                SetAbilityToReloader(index, button);
                
                button.clicked += Abilities[index].Use;
                index++;
            }
        }
    }

    private void ChangeSlotNameToInfoAbilityName(VisualElement element, int index)
    {
        element.name = Abilities[index].InfoAbility.name;
    }

    private void SetAbilityManaCost(AbilityInfo info, VisualElement root)
    {
        var manaInfo = root.Q<Label>();
        manaInfo.text = info.ManaCost.ToString();
    }

    private Button SetAbilityIcon(AbilityInfo info, VisualElement root)
    {
        var button = root.Q<Button>();
        button.style.backgroundImage = new StyleBackground(info.Icon);
        return button;
    }

    private void SetAbilityToReloader(int index, Button button)
    {
        Abilities[index].InfoAbility.AbilityVisualReload = new AbilityReloader(this, button);
    }
}