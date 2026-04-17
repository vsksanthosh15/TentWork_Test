using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChoppingTable : MonoBehaviour {
    [SerializeField] Slider Slider;
    [SerializeField] GameObject PlaceHolder;
    [SerializeField] Image IndicateImage;

    [SerializeField] Sprite BeforeTask, AfterTask;

    bool isAvailable;
    Ingredients currentIngredient;

    public bool GetIsAvailable => isAvailable;

    public void OnStart() {
        isAvailable = true;
        IndicateImage.sprite = BeforeTask;
        Slider.gameObject.SetActive(false);
    }


    public void OnChopping(Ingredients ingredient) {

        if (ingredient == null || ingredient.GetIngredientData.IngredientType != IngredientType.Vegitable || currentIngredient != null) return;

        isAvailable = false;
        currentIngredient = ingredient;
        ingredient.gameObject.transform.position = PlaceHolder.transform.position;
        IndicateImage.sprite = BeforeTask;
        ingredient.transform.SetParent(this.transform);

        StopAllCoroutines();
        StartCoroutine(StartCooking(GameManager.Instance.GetChoppingTime));
    }

    public void OnTakeDish() {
        isAvailable = true;
        GameManager.Instance.SetIngredientToPlayer(currentIngredient);
        IndicateImage.sprite = BeforeTask;
        currentIngredient = null;
    }

    void OnChoppingCompleted() {
        IndicateImage.sprite = AfterTask;
        currentIngredient.IngAfterTask();
        Slider.gameObject.SetActive(false);
    }

    IEnumerator StartCooking(float time) {
        Slider.gameObject.SetActive(true);
        Slider.value = 0f;

        while (Slider.value < 1f) {
            Slider.value += Time.deltaTime / time;
            yield return null;
        }

        Slider.value = 1f;
        OnChoppingCompleted();
    }
}
