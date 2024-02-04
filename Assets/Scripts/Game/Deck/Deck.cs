using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck:MonoBehaviour
{
    public List<Cube> cubeDeck;
    [SerializeField] CombatManager combatManager;

    private void Update()
    {
        if(combatManager.addedCube.Count != 0)
        {
            AddCubeToDeck(combatManager);
        }
    }
    public void AddCubeToDeck(CombatManager manager)
    {
        while (manager.addedCube.Count > 0)
        {
            var message = combatManager.addedCube.Dequeue();
            Debug.Log($"¿¨×é´«Èë{message.Base.CubeName}");
            cubeDeck.Add (message);
        }
    }
}
