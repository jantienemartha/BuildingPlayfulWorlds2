using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepInteractable : Interactable {

    public PrepAction action;
    public static int currentPrepStep = 0;

    private void Start()
    {
        interactStates = new List<GameState>();
        interactStates.Add(GameState.Preparation);
    }

    public override void Interaction()
    {
        if (GameManager.manager.currentGameState != GameState.Preparation)
        {
            return;
        }
        if (IsNextStep())
        {
            currentPrepStep++;
        }
        GameManager.manager.CheckPotionFinished();
    }

    public override void Notify()
    {
        if (GameManager.manager.currentGameState != GameState.Preparation)
        {
            return;
        }
        if (GameManager.manager.PotionFinished())
        {
            UIManager.manager.SetNotification("My potion is already finished");
        }

        if (!IsNextStep())
        {
            UIManager.manager.SetNotification("I shouldn't be doing this right now");
        }
    }

    public override void PlayInteractionClip()
    {
        if (!IsNextStep())
        {
            return;
        }
        base.PlayInteractionClip();
    }

    public bool IsNextStep()
    {
        return GameManager.manager.currentRecipe.preparation[currentPrepStep] == action;
    }

}
