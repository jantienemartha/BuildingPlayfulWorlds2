using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Interactable {

    public IngredientName ingredientName;
    

    private void Start()
    {
        interactableName = ingredientName.ToString();
        interactNotification = "Picked up " + ingredientName;
        interactStates = new List<GameState>();
        interactStates.Add(GameState.IngredientSearch);
    }


    public override void Interaction()
    {
        Player.player.inventory.AddToInventory(ingredientName);
        if (Player.player.inventory.HasAllRecipeItems())
        {
            UIManager.manager.SetNotification("I should head home to create my potion");
            GameManager.manager.currentGameState = GameState.SearchFinished;
        }
        Destroy(gameObject);
        
    }
}
