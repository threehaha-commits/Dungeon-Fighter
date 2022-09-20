using System;
using UnityEngine;

public interface IExpirience
{
    void SetExp(int value);
    int GetExp();
}

public class Experience : MonoBehaviour, IExpirience
{
    private int Exp;
    private int NeedExpForLevelUp;
    private const int BaseValueExp = 1000;
    private const float Coefficient = 1.1f;
    private int Level => GetComponent<PlayerLevel>().GetLevel();
    private ExpirienceViewer ExpViewer;
    
    private void Start()
    {
        ExpViewer = GetComponent<ExpirienceViewer>();
        NeedExpForLevelUp = Convert.ToInt32(BaseValueExp * Math.Pow(Coefficient, Level));
        ExpViewer.ChangeExpBar(Exp, NeedExpForLevelUp);
    }

    void IExpirience.SetExp(int value)
    {
        var expValue = Exp + value;
        var currentExp = Exp;
        if (expValue > NeedExpForLevelUp)
        {
            var needExpForLevelUp = NeedExpForLevelUp;
            var neededExpForLevelUp = needExpForLevelUp - currentExp;
            Exp += neededExpForLevelUp;
            LevelUp();
            var lastExpAfterLevelUp = value - neededExpForLevelUp;
            Exp = 0;
            Exp = lastExpAfterLevelUp;
        }
        else
            Exp += value;
        
        ExpViewer.ChangeExpBar(Exp, NeedExpForLevelUp);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            IExpirience exp = this;
            exp.SetExp(300);
            Debug.Log(Exp);
        }
    }

    private void LevelUp()
    {
        GetComponent<PlayerLevel>().Up();
        NeedExpForLevelUp = Convert.ToInt32(BaseValueExp * Math.Pow(Coefficient, Level));
    }
    
    int IExpirience.GetExp()
    {
        return Exp;
    }
}