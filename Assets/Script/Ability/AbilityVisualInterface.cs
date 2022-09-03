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
    
    private void Start()
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

                element.name = SetSlotName(Abilities[index].InfoAbility);
                
                button.clicked += Abilities[index].Use;
                index++;
            }
        }
    }

    private string SetSlotName(AbilityInfo info)
    {
       return info.name;
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
}