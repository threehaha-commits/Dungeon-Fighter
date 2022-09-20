using UnityEngine;

public static class EnemyBarColorChanger
{
    private static IChangeBarColor TargetBar;
    
    public static void ChangeColorBar(Transform Target)
    {
        IChangeBarColor targetBar = Target.GetComponentInChildren<IChangeBarColor>();
        if(TargetBar != null)
            TargetBar.ChangeColor(Color.red);

        TargetBar = targetBar;
        TargetBar.ChangeColor(Color.yellow);
    }
}