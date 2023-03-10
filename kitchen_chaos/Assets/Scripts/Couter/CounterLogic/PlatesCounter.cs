using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemove;

    [SerializeReference] private KitchenObjectSO plateKitchenObjectSO;
    private float plateTimer = 0f;
    private float plateTimerMax = 4f;

    private int plateNumberMax = 4;
    private int plateNumber = 0;

    private void Update()
    {
        plateTimer += Time.deltaTime;
        if(plateTimer >= plateTimerMax)
        {
            plateTimer = 0;

            if(plateNumber < plateNumberMax)
            {
                //Spawn new Plate on visual only
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
                plateNumber++;
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //Player is hold nothing
            if(plateNumber > 0)
            {
                //PlateCounter has plates
                plateNumber--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemove?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
