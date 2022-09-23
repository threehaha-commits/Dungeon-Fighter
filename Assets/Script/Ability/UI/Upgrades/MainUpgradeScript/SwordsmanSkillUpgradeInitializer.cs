using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class SwordsmanSkillUpgradeInitializer : MonoBehaviour
{
    private const string ParentPanel = "PanelWithSkills";
    private const string ParentWindow = "ParentSkillsWindow";
    private const string ChainHookWindowName = "ChainHook-WindowWithUpgrades";
    private const string FieryRageWindowName = "FieryRage-WindowWithUpgrades";
    private List<Button> SkillButtons = new();
    private List<VisualElement> WindowWithSkillUpgrades = new();
    private UIDocument Document;
    private VisualElement ParentPanelElement;
    private VisualElement ParentWindowElement;
    
    [Inject]
    private void Construct(Document document)
    {
        Document = document.WindowWithSkills;
    }

    private void Start()
    {
        var root = Document.rootVisualElement;
        ParentPanelElement = root.Q<VisualElement>(ParentPanel);
        ParentWindowElement = root.Q<VisualElement>(ParentWindow);
        FindSkillsButton();
        FindSkillsWindow();
        var chainHookComponent = GetComponent<ChainHook>();
        InitializeChainHookStunDuration(chainHookComponent, root);
        InitializeChainHookDealsDamage(chainHookComponent, root);
        InitializeChainHookReduceCooldown(root, chainHookComponent);
    }

    private void FindSkillsWindow()
    {
        foreach (var window in ParentWindowElement.Children())
        {
            WindowWithSkillUpgrades.Add(window);
        }
        //WindowWithSkillUpgrades.Add(ParentPanelElement.Q<VisualElement>(ChainHookWindowName));
        //WindowWithSkillUpgrades.Add(ParentPanelElement.Q<VisualElement>(FieryRageWindowName));
    }

    private void FindSkillsButton()
    {
        foreach (var button in ParentPanelElement.Children())
        {
            SkillButtons.Add(button.Q<Button>());
        }
    }

    private static void InitializeChainHookStunDuration(ChainHook chainHookComponent, VisualElement root)
    {
        var rootVisualElement = root.Q<VisualElement>("Upgrade-1");
        
        var chainHookStunDurationLogic = new ChainHookStunDurationUpgrade(chainHookComponent.Ability, chainHookComponent);
        var chainHookStunDurationMarkers = new UpgradeVisualMarker(rootVisualElement);
        
        new UpgradeController<IUpgradableEffect>(chainHookStunDurationLogic, chainHookStunDurationMarkers, rootVisualElement);
    }

    private static void InitializeChainHookDealsDamage(ChainHook chainHookComponent, VisualElement root)
    {
        var rootVisualElement = root.Q<VisualElement>("Upgrade-2");
        
        var chainHookDealsDamageLogic = new ChainHookDealsDamage(chainHookComponent.Ability, chainHookComponent);
        var chainHookDealsDamageMarkers = new UpgradeVisualMarker(rootVisualElement);
        
        new UpgradeController<IUpgradableEffect>(chainHookDealsDamageLogic, chainHookDealsDamageMarkers, rootVisualElement);
    }

    private static void InitializeChainHookReduceCooldown(VisualElement root, ChainHook chainHookComponent)
    {
        var rootVisualElement = root.Q<VisualElement>("Upgrade-3");
        
        var chainHookReduceCooldownLogic =
            new ChainHookReduceCooldown(chainHookComponent.InfoAbility.AbilityMain);
        var chainHookReduceCooldownMarkers = new UpgradeVisualMarker(rootVisualElement);
        
        new UpgradeController<IUpgradableEffect>(chainHookReduceCooldownLogic, chainHookReduceCooldownMarkers,
            rootVisualElement);
    }
}