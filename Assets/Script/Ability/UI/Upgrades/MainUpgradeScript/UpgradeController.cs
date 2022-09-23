using UnityEngine.UIElements;

public class UpgradeController<T> where T : IUpgradableEffect
{
    private readonly T TLogic;
    private readonly T TVisual;
    private const string UpgradeButtonName = "UpgradeButton";
    
    public UpgradeController(T upgradableLogic, T upgradableVisual, VisualElement root)
    {
        TLogic = upgradableLogic;
        TVisual = upgradableVisual;
        var upgradeButton = root.Q<Button>(UpgradeButtonName);
        upgradeButton.clicked += Invoke;
    }

    private void Invoke()
    {
        TLogic.Upgrade();
        TVisual.Upgrade();
    }
}