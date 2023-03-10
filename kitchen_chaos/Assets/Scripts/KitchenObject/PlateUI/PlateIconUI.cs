using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private IconTemplate iconTemplate;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += plateKitchenObject_OnIngredientAdded;
        iconTemplate.gameObject.SetActive(false);
    }

    private void plateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual(e.addedIngredientKitchenObjectSO);
    }

    private void UpdateVisual(KitchenObjectSO addedIngredientSO)
    {
        IconTemplate plateIconTemplate = Instantiate<IconTemplate>(iconTemplate, this.transform);
        plateIconTemplate.gameObject.SetActive(true);
        plateIconTemplate.SetKitchenObjectSO(addedIngredientSO);
    }
}
