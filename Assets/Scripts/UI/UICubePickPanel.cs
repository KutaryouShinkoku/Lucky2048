using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICubePickPanel : MonoBehaviour
{
    [SerializeField] CombatManager combatManager;
    [SerializeField] GameObject uiRaritySelect;
    [SerializeField] GameObject uiCubeSelect;
    

    private void Update()
    {
        if(combatManager.state==CombatState.selectR||combatManager .state == CombatState.selectC)
        {
            gameObject.SetActive(true);
            if (combatManager.state == CombatState.selectR)
            {
                uiRaritySelect.SetActive(true);
                uiCubeSelect.SetActive(false);
            }
            else if (combatManager.state == CombatState.selectC)
            {
                uiRaritySelect.SetActive(false);
                uiCubeSelect.SetActive(true);

            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
