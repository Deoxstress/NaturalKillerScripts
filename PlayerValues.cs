using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerValues : ScriptableObject
{

    public float dashCD, lockTimerToAddEnemyToScan;  
    public int playerLevel, maxTargets, maxLockOnTargets, maxHpValue, maxXpValue;
    
    public void LevelUp(int playerCurrentLevelPlusOne)
    {
        switch(playerCurrentLevelPlusOne)
        {
            case 1: //LevelUp to level 1
                lockTimerToAddEnemyToScan = 0.3f;
                maxLockOnTargets = 3;
                maxTargets = 5;
                dashCD = 2.0f;
                maxXpValue = 100;
                playerLevel = 1;
                break;
            case 2: //LevelUp to level 2
                lockTimerToAddEnemyToScan = 0.2f;
                maxLockOnTargets = 7;
                maxTargets = 10;
                dashCD = 1.0f;
                maxXpValue = 100;
                playerLevel = 2;
                break;
            case 3: //LevelUp to level 3
                lockTimerToAddEnemyToScan = 0.1f;
                maxLockOnTargets = 15;
                maxTargets = 20;
                dashCD = 0.5f;
                maxXpValue = 100;
                playerLevel = 3;
                break;
            case 4: //LevelUpToLevel 4 ... etc
                break;
        }
    }

    /*
     * 
     * 
     * 0.35 => 0.20 => 0.10)

- nombre de lock max sur un ennemi (3 => 7 => 15)

- nombre de lock max d'ennemi en général (5 => 10 => 20)

- CD du dash (2.0 => 1.0 => 0.5)
     * 
     */
}
