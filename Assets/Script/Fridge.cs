using UnityEngine;
using UnityEngine.UI;

public class Fridge : MonoBehaviour {

    [SerializeField] Image IngImage;

    IngredientType currentIngredient;
    
    public void OnStart() {
        ChangeIngredient(GameManager.Instance.GetRandomIngredientType());
    }

    public void ChangeIngredient(IngredientType type) {
        currentIngredient = type;
        OnShowIngUi();
    }

    public void OnShowIngUi() {
        IngImage.sprite = GameManager.Instance.GetIngredientData(currentIngredient).BeforeReady;
    }

    public void OnSpawnIngredient() {

        IngredientData data = GameManager.Instance.GetIngredientData(currentIngredient);
        Ingredients ingredient = Instantiate(GameManager.Instance.GetIngredientPrefab);
        ingredient.SetIngredientData(data);
        ingredient.IngBeforeTask();

        Player player = GameManager.Instance.GetPlayer;

        GameManager.Instance.SetIngredientToPlayer(ingredient);
        ChangeIngredient(GameManager.Instance.GetRandomIngredientType());
    }

    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.CompareTag("Player")) {
            

        }
    }

}
