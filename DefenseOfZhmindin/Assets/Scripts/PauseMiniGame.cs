using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMiniGame : MonoBehaviour
{
    [SerializeField] private List<Item> ItemData;
    [SerializeField] private TextMeshProUGUI Output;
    [SerializeField] public TMP_InputField ING1;
    [SerializeField] private Button create;
    private string[] TestIngredients = new string[4];
    private void Awake()
    {
        create.onClick.AddListener(Create);
    }
    public void Create()
    {
        string ING2 = ING1.text;
        string[] Ingredients = ING2.Split(' ');
        TestIngredients[0] = Ingredients[0];
        TestIngredients[1] = Ingredients[1];
        TestIngredients[2] = Ingredients[2];
        TestIngredients[3] = Ingredients[3];
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
                if (ItemIngridients[0] == TestIngredients[0])
                {
                    if (TestIngredients[1] == ItemIngridients[1])
                    {
                        if (TestIngredients[2] == ItemIngridients[2])
                        {
                            if (TestIngredients[3] == ItemIngridients[3])
                            {
                                ItemName = item.Name;
                                return true;
                            }
                        }
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