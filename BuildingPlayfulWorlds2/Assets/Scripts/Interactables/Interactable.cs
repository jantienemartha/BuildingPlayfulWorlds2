using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {

    public float interactRange;
    public string interactableName;
    public string interactNotification;
    public AudioClip interactClip;
    public List<GameState> interactStates;

    public void HandleInteraction()
    {
        if (interactStates.Contains(GameManager.manager.currentGameState))
        {
            PlayInteractionClip();
        }

        Notify();

        if (interactStates.Contains(GameManager.manager.currentGameState))
        {
            Interaction();
        }

    }

    public virtual void PlayInteractionClip()
    {
        SoundManager.manager.PlayClip(interactClip);
    }

    public virtual void Interaction()
    {
        
    }

    public virtual void Notify()
    {
        UIManager.manager.SetNotification(interactNotification);
    }

    public bool IsInRange(Vector3 position)
    {
        return Vector3.Distance(position, transform.position) <= interactRange;
    }

    public bool IsInRange(Vector3 interactorPosition, Vector3 hitPosition)
    {
        return Vector3.Distance(interactorPosition, hitPosition) <= interactRange;
    }
}
