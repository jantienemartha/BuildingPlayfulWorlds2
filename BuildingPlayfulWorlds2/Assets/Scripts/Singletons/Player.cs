using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player player;
    public Inventory inventory;

    bool canMove;

    private void Awake()
    {
        if (player != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.transform.parent.gameObject);
            player = this;
        }
    }

    // Use this for initialization
    void Start () {
        inventory = new Inventory();
	}

    private void OnLevelWasLoaded(int level)
    {
        GameObject spawnPoint = GameObject.Find("PlayerSpawn");
        transform.parent.transform.position = spawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update () {
        HandleInteractables();
	}


    void HandleInteractables()
    {
        UIManager.manager.ClearCursorText();
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        bool hitInteractable = Physics.Raycast(ray, out hit);
        if (hitInteractable)
        {
            Interactable interactScript = hit.transform.GetComponent<Interactable>();
            if (interactScript == null)
            {
                return;
            }
            if (!interactScript.IsInRange(transform.position, hit.point))
            {
                return;
            }
            UIManager.manager.SetCursorText(interactScript.interactableName);
            if (Input.GetMouseButtonDown(0))
            {
                interactScript.HandleInteraction();
            }
        }
    }

    public void ToggleMovement()
    {
        Rigidbody rb = transform.parent.gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = !rb.isKinematic;
    }

}
