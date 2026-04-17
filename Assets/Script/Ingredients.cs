using UnityEngine;
using UnityEngine.UI;

public enum IngredientType {
    None,
    Chees,
    Vegitable,
    Meet
}

[System.Serializable]
public class IngredientData {
    public IngredientType IngredientType;
    public float Score;

    public Sprite BeforeReady, AfterReady;

    public IngredientData(IngredientData data) {
        IngredientType = data.IngredientType;
        Score = data.Score;
        BeforeReady = data.BeforeReady;
        AfterReady = data.AfterReady;
    }

}

public class Ingredients : MonoBehaviour {

    [SerializeField] Image IngImage;

    IngredientData ingredientData;

    public IngredientData GetIngredientData => ingredientData;

    void Start() {
        
    }

    public void SetIngredientData(IngredientData data) { 
        ingredientData = data;
    }

    public void IngBeforeTask() {
        IngImage.sprite = ingredientData.BeforeReady;
    }

    public void IngAfterTask() {
        IngImage.sprite = ingredientData.AfterReady;
    }

}
