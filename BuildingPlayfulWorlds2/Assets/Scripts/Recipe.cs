using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "new recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField]
    public List<IngredientName> recipe;
    public List<PrepAction> preparation;

}

public enum IngredientName
{
Tomato, Mushroom, Worm, Spider, Plant, Pumpkin
}

public enum PrepAction
{
Chop, Cook, Fill
}

