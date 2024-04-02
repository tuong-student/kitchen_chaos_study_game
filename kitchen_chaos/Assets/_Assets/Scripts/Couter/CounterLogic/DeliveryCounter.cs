using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance;

    protected override void Awake() 
    {
        base.Awake();
        if(Instance == null) Instance = this;
    }

    public override void Interact(Player player)
    {
        if(player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlateKitchenObject(out PlateKitchenObject plateKitchenObject))
            {
                if(DeliveryManager.Instance.DeliverRecipe(plateKitchenObject))
                {
                    player.GetKitchenObject().DestroySelf();
                }
                else{
                    Debug.Log("Delivery failed");
                }
            }
            else{
                Debug.Log("Player is carrying something not Plate");
            
            }
        }
        else{
            Debug.Log("Player is not carrying anything");
        }
    }
}