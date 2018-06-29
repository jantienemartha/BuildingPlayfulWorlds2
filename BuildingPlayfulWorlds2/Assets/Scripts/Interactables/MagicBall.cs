using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : Interactable {

    private void Start()
    {
        interactStates = new List<GameState>();
        interactStates.Add(GameState.Taskless);
    }

    public override void Notify()
    {
        if (GameManager.manager.currentGameState != GameState.Taskless)
        {
            UIManager.manager.SetNotification("I've given you a new task already");
        }
        else
        {
            UIManager.manager.SetNotification("I'm giving you a new task, check your recipe notes (R)");
        }
    }

    public override void Interaction()
    {
        if (GameManager.manager.currentGameState != GameState.Taskless)
        {
            return;
        }
        GameManager.manager.SelectNewRecipe();
        GameManager.manager.currentGameState = GameState.PreSearch;
    }
}
