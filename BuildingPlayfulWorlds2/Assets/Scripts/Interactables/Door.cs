using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable {

    private void Start()
    {
        interactStates = new List<GameState>();
        interactStates.Add(GameState.SearchFinished);
        interactStates.Add(GameState.PreSearch);
    }

    public override void Notify()
    {
        switch (GameManager.manager.currentGameState)
        {
            case GameState.Taskless:
                UIManager.manager.SetNotification("I should get a new task first");
                break;
            case GameState.Preparation:
                UIManager.manager.SetNotification("I should finish my current potion first");
                break;
            case GameState.IngredientSearch:
                UIManager.manager.SetNotification("I should gather ingredients first");
                break;

        }
        if (GameManager.manager.currentGameState == GameState.Taskless)
        {
            return;
        }
    }
    public override void Interaction()
    {
        if (GameManager.manager.currentGameState != GameState.SearchFinished && GameManager.manager.currentGameState != GameState.PreSearch)
        {
            return;
        }
        if (SceneManager.GetActiveScene().name != "Room")
        {
            UIManager.manager.ClearNotification();
            SceneManager.LoadScene("Room");
            if (GameManager.manager.currentGameState == GameState.SearchFinished)
            {
                GameManager.manager.currentGameState = GameState.Preparation;
            }
        }
        else if (SceneManager.GetActiveScene().name != "Outdoors")
        {
            UIManager.manager.ClearNotification();
            SceneManager.LoadScene("Outdoors");
            if (GameManager.manager.currentGameState == GameState.PreSearch)
            {
                GameManager.manager.currentGameState = GameState.IngredientSearch;
            }

        }
    }

}
