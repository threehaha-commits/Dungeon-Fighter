using System;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] protected UIDocument Document;
    protected IDeathInspector TargetHandlerInspector;
    private ParticleSystem CastingSpellEffect;
    protected Button SkillButton;
    private Label ManaCostText;
    protected string SkillButtonName;
    protected TargetHandler TargetDealer;
    protected Transform Root;

    [Inject]
    private void Construct(Document Document)
    {
        this.Document = Document.Skill;
    }

    void Start()
    {
        SkillButton.clicked += Use;
    }

    void Use()
    {
        CastingSpellEffect.Play();
    }
}