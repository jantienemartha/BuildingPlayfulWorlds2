using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
Preparation, IngredientSearch, Taskless, PreSearch, SearchFinished, Minigame
}

public class GameManager : MonoBehaviour {


    public static GameManager manager;

    public GameState currentGameState;

    public List<Recipe> allRecipes;
    public Recipe currentRecipe;


    public GameObject spawnpointsParent;
    List<GameObject> ingredientSpawnPoints;
    public List<GameObject> ingredientPrefabs;

    private void Awake()
    {
        if (manager != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            manager = this;
        }
    }

    private void Start()
    {
        currentGameState = GameState.Taskless;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            FindSpawnPoints();
            ResetSpawnpoints();
            SpawnIngredients();
        }
    }

    public void SelectNewRecipe()
    {
        int recipeCount = allRecipes.Count;
        int randomIndex = Random.Range(0, recipeCount);
        currentRecipe = allRecipes[randomIndex];
        UIManager.manager.SetRecipeNote();
        PrepInteractable.currentPrepStep = 0;
        Player.player.inventory.ClearInventory();
    }

    public void SpawnIngredients()
    {
        foreach (IngredientName ingredient in currentRecipe.recipe)
        {
            GameObject prefab = SelectIngredientPrefab(ingredient);
            GameObject spawnpoint = SelectRandomAvailableSpawnPoint();
            GameObject spawnedObject = Instantiate(prefab, spawnpoint.transform.position, prefab.transform.rotation);
        }
    }

    public GameObject SelectRandomAvailableSpawnPoint()
    {
        int spawnCount = ingredientSpawnPoints.Count;
        for(int i = 0; i<100; i++)
        {
            int randomIndex = Random.Range(0, spawnCount-1); ;

            GameObject randomSpawnpoint = ingredientSpawnPoints[randomIndex];
            Spawnpoint spawnScript = randomSpawnpoint.GetComponent<Spawnpoint>();
            if (spawnScript.isAvailable)
            {
                spawnScript.isAvailable = false;
                return randomSpawnpoint;
            }
        }
        Debug.Log("no spawnpoint found");
        return null;
    }

    public void FindSpawnPoints()
    {
        ingredientSpawnPoints = new List<GameObject>();
        for (int i = 0; i < spawnpointsParent.transform.childCount; i++)
        {
            ingredientSpawnPoints.Add(spawnpointsParent.transform.GetChild(i).gameObject);
        }
    }

    public void ResetSpawnpoints()
    {
        foreach (GameObject spawnpoint in ingredientSpawnPoints)
        {
            Spawnpoint spawnScript = spawnpoint.GetComponent<Spawnpoint>();
            spawnScript.isAvailable = true;
            if (spawnpoint.transform.childCount > 0)
            {
                for (int i = spawnpoint.transform.childCount; i >= 0; i--)
                {
                    Destroy(spawnpoint.transform.GetChild(i).gameObject);
                }
            }
        }
    }

    public GameObject SelectIngredientPrefab(IngredientName ingredient)
    {
        foreach (GameObject prefab in ingredientPrefabs)
        {
            Ingredient ingredientScript = prefab.GetComponent<Ingredient>();
            if (ingredientScript == null)
            {
                ingredientScript = prefab.GetComponentInChildren<Ingredient>();
            }
            if (ingredientScript.ingredientName == ingredient)
            {
                return prefab;
            }
        }
        Debug.Log("Prefab not found");
        return null;
    }

    public void CheckPotionFinished()
    {
        if (currentGameState != GameState.Preparation || !PotionFinished())
        {
            return;
        }
        UIManager.manager.SetNotification("Potion complete, I should check for a new one");
        currentGameState = GameState.Taskless;
    }

    public bool PotionFinished()
    {
        return PrepInteractable.currentPrepStep >= currentRecipe.preparation.Count;
    }
}
