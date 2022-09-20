using System.Collections;
using UnityEngine;

public class AbilityReloaderLogic
{
    private MonoBehaviour Mono;
    private AbilityReloadVisual Visual;
    
    public AbilityReloaderLogic(MonoBehaviour mono, AbilityReloadVisual visual)
    {
        Mono = mono;
        Visual = visual;
    }
    
    public void StartReload(float reloadTime)
    {
        Mono.StartCoroutine(Reload(reloadTime));
    }
    
    private IEnumerator Reload(float reloadTime)
    {
        var timeAway = 0.1f;
        while (reloadTime > 0)
        {
            reloadTime -= timeAway;
            Visual.RefreshVisualValue(reloadTime);
            yield return new WaitForSeconds(timeAway);
        }
    }
}