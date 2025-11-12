using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float replenishValue = 0.25f;

    public void Interact(Sim inSim)
    {
        if (inSim.IsNeedLow(NeedData.NeedType.hunger))
        {
            inSim.IncreaseNeed(NeedData.NeedType.hunger, replenishValue);
        }
    }
}
