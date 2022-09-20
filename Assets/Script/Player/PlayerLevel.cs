using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    private int Level = 1;
    private const float HpForLevelUp = 5f;
    private const float MpForLevelUp = 5f;
    private PlayerHealth Health;
    private PlayerMana Mana;
    
    private void Start()
    {
        Health = GetComponent<PlayerHealth>();
        Mana = GetComponent<PlayerMana>();
    }

    public void Up()
    {
        Level++;
        Health.ChangeMaxHealthValue(HpForLevelUp);
        Mana.ChangeMaxManaValue(MpForLevelUp);
    }

    public int GetLevel()
    {
        return Level;
    }
}