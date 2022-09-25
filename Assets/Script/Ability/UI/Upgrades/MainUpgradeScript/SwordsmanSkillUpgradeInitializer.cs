using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class SwordsmanSkillUpgradeInitializer : MonoBehaviour
{
    private UIDocument Document;
    
    [Inject]
    private void Construct(Document document)
    {
        Document = document.WindowWithSkills;
    }

    private void Start()
    {
        var root = Document.rootVisualElement;
        var rootChainHook = root.Q<VisualElement>("ChainHook-WindowWithUpgrades");
        var chainHookComponent = GetComponent<ChainHook>();
        InitializeChainHookStunDuration(chainHookComponent, rootChainHook);
        InitializeChainHookDealsDamage(chainHookComponent, rootChainHook);
        InitializeChainHookReduceCooldown(chainHookComponent, rootChainHook);
        
        var rootFuryStrike = root.Q<VisualElement>("FuryStrike-WindowWithUpgrades");
        var furyStrikeComponent = GetComponent<FuryStrike>();
        FuryStrikeIncreaseDamage(rootFuryStrike, furyStrikeComponent);
        FuryStrikeGetPeriodicDamage(rootFuryStrike, furyStrikeComponent);
        
        var rootFieryRage = root.Q<VisualElement>("FieryRage-WindowWithUpgrades");
        var fieryRageComponent = GetComponent<FieryRage>();
        FieryRageDuration(rootFieryRage, fieryRageComponent);
        FieryRageDamageAtMoment(rootFieryRage, fieryRageComponent);
        FieryRageDamagePerSecond(rootFieryRage, fieryRageComponent);
        
        var rootRush = root.Q<VisualElement>("Rush-WindowWithUpgrades");
        var rushComponent = GetComponent<Rush>();
        RushDealsPercentDamage(rootRush, rushComponent);
        
        var rootFuryJump = root.Q<VisualElement>("FuryJump-WindowWithUpgrades");
        var furyJumpComponent = GetComponent<FuryJump>();
        FuryJumpUpgradeBoundCount(rootFuryJump, furyJumpComponent);
        FuryJumpUpgradeDamagePerBound(rootFuryJump, furyJumpComponent);

        var rootRestoreHealth = root.Q<VisualElement>("RestoreHealth-WindowWithUpgrades");
        var restoreHealthComponent = GetComponent<RestoreHealth>();
        RestoreHealthIncreaseValue(rootRestoreHealth, restoreHealthComponent);
        RestoreHealthIncreaseMaxValue(rootRestoreHealth, restoreHealthComponent);
    }

    private void RestoreHealthIncreaseMaxValue(VisualElement rootRestoreHealth, RestoreHealth restoreHealthComponent)
    {
        var rootVisualElement = rootRestoreHealth.Q<VisualElement>("Upgrade-2");
        var restoreHealthIncreaseMaxValue =
            new RestoreHealthIncreaseMaxHealth(restoreHealthComponent.Ability, restoreHealthComponent, new DescriptionSkill(rootVisualElement));
        new UpgradeController<IUpgradableEffect>(restoreHealthIncreaseMaxValue, new UpgradeVisualMarker(rootVisualElement),
            rootVisualElement);
    }

    private void RestoreHealthIncreaseValue(VisualElement rootRestoreHealth, RestoreHealth restoreHealthComponent)
    {
        var rootVisualElement = rootRestoreHealth.Q<VisualElement>("Upgrade-1");
        var restoreHealthIncreaseValue =
            new RestoreHealthIncreaseValue(restoreHealthComponent.Ability, new DescriptionSkill(rootVisualElement));
        new UpgradeController<IUpgradableEffect>(restoreHealthIncreaseValue, new UpgradeVisualMarker(rootVisualElement),
            rootVisualElement);
    }

    private static void FuryJumpUpgradeDamagePerBound(VisualElement rootFuryJump, FuryJump furyJumpComponent)
    {
        var rootVisualElement = rootFuryJump.Q<VisualElement>("Upgrade-2");
        var furyJumpDamagePerBound =
            new FuryJumpUpgradeDamagePerBound(furyJumpComponent.Ability, new DescriptionSkill(rootVisualElement));
        new UpgradeController<IUpgradableEffect>(furyJumpDamagePerBound, new UpgradeVisualMarker(rootFuryJump),
            rootVisualElement);
    }

    private static void FuryJumpUpgradeBoundCount(VisualElement rootFuryJump, FuryJump furyJumpComponent)
    {
        var rootVisualElement = rootFuryJump.Q<VisualElement>("Upgrade-1");
        var furyJumpBoundCount =
            new FuryJumpUpgradeCountBound(furyJumpComponent.Ability, new DescriptionSkill(rootVisualElement));
        new UpgradeController<IUpgradableEffect>(furyJumpBoundCount, new UpgradeVisualMarker(rootFuryJump),
            rootVisualElement);
    }

    private void RushDealsPercentDamage(VisualElement rootRush, Rush rushComponent)
    {
        var rootVisualElement = rootRush.Q<VisualElement>("Upgrade-1");
        var rushDealsPercentDamage =
            new RushDealsPercentDamage(rushComponent.Ability, rushComponent, new DescriptionSkill(rootVisualElement));
        new UpgradeController<IUpgradableEffect>(rushDealsPercentDamage, new UpgradeVisualMarker(rootRush), rootVisualElement);
    }
    
    private void FieryRageDamagePerSecond(VisualElement rootFieryRage, FieryRage fieryRageComponent)
    {
        var rootVisualElement = rootFieryRage.Q<VisualElement>("Upgrade-3");
        var fieryRageDamagePerSecond = new FieryRageDamagePerSecondUpgrade(fieryRageComponent.Ability, new DescriptionSkill(rootVisualElement));
        new UpgradeController<IUpgradableEffect>(fieryRageDamagePerSecond, new UpgradeVisualMarker(rootVisualElement),
            rootVisualElement);
    }

    private void FieryRageDamageAtMoment(VisualElement rootFieryRage, FieryRage fieryRageComponent)
    {
        var rootVisualElement = rootFieryRage.Q<VisualElement>("Upgrade-2");
        var fieryRageDamageAtMoment = new FieryRageMomentDamageUpgrade(fieryRageComponent.Ability, new DescriptionSkill(rootVisualElement));
        new UpgradeController<IUpgradableEffect>(fieryRageDamageAtMoment, new UpgradeVisualMarker(rootVisualElement),
            rootVisualElement);
    }

    private void FieryRageDuration(VisualElement rootFieryRage, FieryRage fieryRageComponent)
    {
        var rootVisualElement = rootFieryRage.Q<VisualElement>("Upgrade-1");
        var fieryRageDurationUpgrade = new FieryRageDurationUpgrade(fieryRageComponent.Ability, new DescriptionSkill(rootVisualElement));
        new UpgradeController<IUpgradableEffect>(fieryRageDurationUpgrade, new UpgradeVisualMarker(rootVisualElement),
            rootVisualElement);
    }

    private void FuryStrikeIncreaseDamage(VisualElement rootFuryStrike, FuryStrike furyStrikeComponent)
    {
        var rootVisualElement = rootFuryStrike.Q<VisualElement>("Upgrade-1");
        var furyStrikeIncreaseDamageLogic = new FuryStrikeIncreaseDamage(furyStrikeComponent.Ability, new DescriptionSkill(rootVisualElement));
        new UpgradeController<IUpgradableEffect>(furyStrikeIncreaseDamageLogic, new UpgradeVisualMarker(rootVisualElement),
            rootVisualElement); 
    }
    
    private void FuryStrikeGetPeriodicDamage(VisualElement rootFuryStrike, FuryStrike furyStrikeComponent)
    {
        var rootVisualElement = rootFuryStrike.Q<VisualElement>("Upgrade-2");
        var furyStrikeGetPeriodicDamageLogic = new FuryStrikeGetPeriodicDamage(furyStrikeComponent.Ability, furyStrikeComponent, new DescriptionSkill(rootVisualElement));
        new UpgradeController<IUpgradableEffect>(furyStrikeGetPeriodicDamageLogic, new UpgradeVisualMarker(rootVisualElement),
            rootVisualElement); 
    }

    private void InitializeChainHookStunDuration(ChainHook chainHookComponent, VisualElement root)
    {
        var rootVisualElement = root.Q<VisualElement>("Upgrade-1");
        var chainHookStunDurationLogic = new ChainHookStunDurationUpgrade(chainHookComponent.Ability, new DescriptionSkill(rootVisualElement));
        new UpgradeController<IUpgradableEffect>(chainHookStunDurationLogic, new UpgradeVisualMarker(rootVisualElement), rootVisualElement);
    }

    private void InitializeChainHookDealsDamage(ChainHook chainHookComponent, VisualElement root)
    {
        var rootVisualElement = root.Q<VisualElement>("Upgrade-2");
        var chainHookDealsDamageLogic = new ChainHookDealsDamage(chainHookComponent.Ability, chainHookComponent, new DescriptionSkill(rootVisualElement));
        new UpgradeController<IUpgradableEffect>(chainHookDealsDamageLogic, new UpgradeVisualMarker(rootVisualElement), rootVisualElement);
    }

    private void InitializeChainHookReduceCooldown(ChainHook chainHookComponent, VisualElement root)
    {
        var rootVisualElement = root.Q<VisualElement>("Upgrade-3");
        var chainHookReduceCooldownLogic =
            new ChainHookReduceCooldown(chainHookComponent.InfoAbility.AbilityMain, new DescriptionSkill(rootVisualElement));
        new UpgradeController<IUpgradableEffect>(chainHookReduceCooldownLogic, new UpgradeVisualMarker(rootVisualElement),
            rootVisualElement);
    }
}