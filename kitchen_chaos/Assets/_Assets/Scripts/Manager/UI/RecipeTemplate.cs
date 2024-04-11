using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static DeliveryManager;

public class RecipeTemplate : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconHolder;
    [SerializeField] private IconTemplate iconTemplate;
    [SerializeField] private Slider waitingSlider;
    private List<IconTemplate> iconTemplateList= new List<IconTemplate>();
    private RecipeSO recipeSO;
    #endregion

    #region Unity functions
    private void Start()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    #endregion

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        this.recipeSO = recipeSO;

        for(int i = 0; i < recipeSO.kitchenObjectSOList.Count; i++)
        {
            if(i == iconTemplateList.Count)
            {
                // i over the final index
                IconTemplate iconTemplateClone = Instantiate(iconTemplate, iconHolder);
                iconTemplateClone.gameObject.SetActive(true);
                iconTemplateClone.SetKitchenObjectSO(recipeSO.kitchenObjectSOList[i]);
                iconTemplateList.Add(iconTemplateClone);
                continue;
            }
            // Update new KitchenObjectSO
            iconTemplateList[i].SetKitchenObjectSO(recipeSO.kitchenObjectSOList[i]);
            if(i == recipeSO.kitchenObjectSOList.Count - 1 && i < iconTemplateList.Count - 1)
            {
                // reach the final index of kitchenObjectSOList but not reach the final index of iconTemplates
                for(int j = i; j < iconTemplateList.Count; j++)
                {
                    Destroy(iconTemplateList[j].gameObject);
                    iconTemplateList.RemoveAt(j);
                }
            }
        }
    }

    public void UpdateTimer(TimerClass timerClass)
    {
        waitingSlider.maxValue = timerClass.maxTimer;
        waitingSlider.value = timerClass.timer;
    }
}
