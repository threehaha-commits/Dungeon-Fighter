using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class AbilityInfo : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    private Ability abilityMain;
    public AbilityEffects Effects;
    public Ability AbilityMain
    {
        get
        {
            if (abilityMain == null)
                abilityMain = new Ability(this);
            return abilityMain;
        }
    }
    public float MaxRange;
    public float MinRange;
    public float Cooldown;
    public float ManaCost;
    
}