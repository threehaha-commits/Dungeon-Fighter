using System.Collections;
using UnityEngine;

public class EnemyAbilityController : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] Abilities;
    private Transform Player;
    private IAbilityTarget[] Ability;
    private float[] Cooldown;
    
    private void Start()
    {
        GetAbilityInterfaceFromMono();
    }

    private void GetAbilityInterfaceFromMono()
    {
        Ability = new IAbilityTarget[Abilities.Length];
        Cooldown = new float[Abilities.Length];
        for (int i = 0; i < Abilities.Length; i++)
        {
            Ability[i] = Abilities[i].GetComponent<IAbilityTarget>();
            Cooldown[i] = Ability[i].InfoAbility.Cooldown;
        }
    }

    public void GetTarget(GameObject target)
    {
        if (target == null)
            return;
        Player = target.transform;
        StartCoroutine(DoCast());
    }
    
    private IEnumerator DoCast()
    {
        yield return new WaitForSeconds(Cooldown[0]);
        Ability[0].Target = Player;
        Ability[0].Use();
    }
}