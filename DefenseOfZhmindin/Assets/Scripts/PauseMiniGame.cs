using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PauseMiniGame : MonoBehaviour
{
    [SerializeField] private string[] TestIngredients;
    [SerializeField] private List<Item> ItemData;
    [SerializeField] private TextMeshProUGUI Output;
    private void Awake()
    {
        bool succes = TryMakeItem(TestIngredients, out string ItemName);
        if (succes)
        {
            Output.text = ($"You Have Made: {ItemName}!");
        }
        else
        {
            Output.text = ("Rats Had Stollen Your Items!");
        }
    }
    private bool TryMakeItem(string[] Ingredients, out string ItemName)
    {
        ItemName = "";
        foreach (var item in ItemData)
        {
            string[] ItemIngridients = item.Ingredients;
            if (TestIngredients.Length == ItemIngridients.Length)
            {
                for (int i = 0; i < TestIngredients.Length; i++)
                {
                    if (TestIngredients[i] == ItemIngridients[i])
                    {
                        ItemName = item.Name;
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
[Serializable]
public class Item
{
    [field:SerializeField] public string[] Ingredients {  get; private set; }
    [field: SerializeField] public string Name { get; private set; }
}