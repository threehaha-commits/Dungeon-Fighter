using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class AbilityReloader
{
    private Button SkillButton;
    private const string ReloadTextName = "ReloadText";
    private Label ReloadTextLabel;
    private AbilityReloaderLogic ReloadLogic;
    private AbilityReloadVisual ReloadVisual;
    
    public AbilityReloader(MonoBehaviour mono, Button skillButton)
    {
        SkillButton = skillButton;
        ReloadTextLabel = SkillButton.Q<Label>(ReloadTextName);
        ReloadTextLabel.style.color = Color.white;
        ReloadVisual = new AbilityReloadVisual(ReloadTextLabel);
        ReloadLogic = new AbilityReloaderLogic(mono, ReloadVisual);
    }
    
    public async void StartVisualReload(float reloadTime)
    {
        var backgroundStyleColor = SkillButton.style.unityBackgroundImageTintColor;
        SkillButton.style.unityBackgroundImageTintColor = Color.gray;
        ReloadTextLabel.visible = true;
        ReloadLogic.StartReload(reloadTime);
        int reloadTimeInMillisecond = Mathf.RoundToInt(reloadTime * 1000);
        await Task.Delay(reloadTimeInMillisecond);
        SkillButton.style.unityBackgroundImageTintColor = backgroundStyleColor;
        ReloadTextLabel.visible = false;
    }
}