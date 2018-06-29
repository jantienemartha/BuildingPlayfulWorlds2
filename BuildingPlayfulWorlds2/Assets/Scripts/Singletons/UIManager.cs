using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager manager;
    public GameObject cursor;
    public Text notificationText;
    public GameObject recipeNote;
    public Text ingredientTextPrefab;
    Text cursorText;

    public AudioClip recipeToggleClip;

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
        cursorText = cursor.GetComponentInChildren<Text>();
        ClearCursorText();
        ClearNotification();
        SetNotification("I should check the magic ball for potion tasks");
    }

    public void SetNotification(string text)
    {
        notificationText.text = text;
    }

    public void SetCursorText(string text)
    {
        cursorText.text = text;
    }

    public void ClearCursorText()
    {
        SetCursorText("");
    }

    public void ClearNotification()
    {
        SetNotification("");
    }

    public void SetRecipeNote()
    {
        ClearRecipeNote();
        Text recipeTitle = Instantiate(ingredientTextPrefab);
        recipeTitle.text = GameManager.manager.currentRecipe.name + ":";
        recipeTitle.gameObject.transform.SetParent(recipeNote.transform);

        foreach (IngredientName ingredient in GameManager.manager.currentRecipe.recipe)
        {
            Text ingredientText = Instantiate(ingredientTextPrefab);
            ingredientText.text = ingredient.ToString();
            ingredientText.gameObject.transform.SetParent(recipeNote.transform);
        }
        Text newLine = Instantiate(ingredientTextPrefab);
        newLine.text = "";
        newLine.gameObject.transform.SetParent(recipeNote.transform);

        Text preparationText = Instantiate(ingredientTextPrefab);
        preparationText.text = "Preparation:";
        preparationText.gameObject.transform.SetParent(recipeNote.transform);

        foreach (PrepAction action in GameManager.manager.currentRecipe.preparation)
        {
            Text actionText = Instantiate(ingredientTextPrefab);
            actionText.text = action.ToString();
            actionText.gameObject.transform.SetParent(recipeNote.transform);
        }
    }

    public void ClearRecipeNote()
    {
        int textCount = recipeNote.transform.childCount;
        if (textCount == 0)
        {
            return;
        }
        for (int i = textCount-1; i >= 0; i--)
        {
            Destroy(recipeNote.transform.GetChild(i).gameObject);
        }
    }

    public void ToggleRecipeNote()
    {
        SoundManager.manager.PlayClip(recipeToggleClip);
        recipeNote.SetActive(!recipeNote.activeSelf);
    }
}
