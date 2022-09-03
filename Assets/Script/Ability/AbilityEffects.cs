using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public interface IInt{}

[CreateAssetMenu(fileName = "New Ability Effect", menuName = "Effect")]
public class AbilityEffects : ScriptableObject
{
    public ParticleSystem[] Effect;
    public TrailRenderer[] Trail;
    public LineRenderer[] Line;

    public Object GetEffect(Object effect, Transform parent = null)
    {
        return Instantiate(effect, parent);
    }
    
    public T GetEffect<T>(Object effect, Transform parent = null)
    {
        var newEffect = Instantiate(effect, parent);
        var type = newEffect.GetComponent<T>();
        return type;
    }
}