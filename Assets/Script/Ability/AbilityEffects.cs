using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "New Ability Effect", menuName = "Effect")]
public class AbilityEffects : ScriptableObject
{
    public ParticleSystem[] Effect;
    public TrailRenderer[] Trail;
    public LineRenderer[] Line;

    public Object GetEffect(Object effect, Transform parent = null)
    {
        var position = effect.GetComponent<Transform>().position;
        var newEffect = Instantiate(effect, parent);
        newEffect.GetComponent<Transform>().localPosition = position;
        return newEffect;
    }
    
    public T GetEffect<T>(Object effect, Transform parent = null)
    {
        var position = effect.GetComponent<Transform>().position;
        var newEffect = Instantiate(effect, parent);
        newEffect.GetComponent<Transform>().localPosition = position;
        var type = newEffect.GetComponent<T>();
        return type;
    }
}