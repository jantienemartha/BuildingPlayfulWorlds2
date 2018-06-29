using System.Collections;
using System.Collections.Generic;

public class Inventory
{
    List<IngredientName> inventory;

    public Inventory()
    {
        inventory = new List<IngredientName>();        
    }

    public void AddToInventory(IngredientName ingredient)
    {
        inventory.Add(ingredient);
    }

    public bool HasAllRecipeItems()
    {
        foreach (IngredientName ingredient in GameManager.manager.currentRecipe.recipe)
        {
            if (!inventory.Contains(ingredient))
            {
                return false;
            }
        }
        return true;
    }

    public void ClearInventory()
    {
        inventory.Clear();
    }
}