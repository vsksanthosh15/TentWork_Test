using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoustomerWindow : MonoBehaviour {

    [SerializeField] Image OrderImage;
    [SerializeField] Slider Slider;
    [SerializeField] TextMeshProUGUI PointsText;

    IngredientData ingredientData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PointsText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStart() {
        OnNewCoustomer();
    }

    public void OnNewCoustomer() {
        OrderImage.gameObject.SetActive(true);
        PointsText.gameObject.SetActive(false);
        ingredientData = GameManager.Instance.GetIngredientData(GameManager.Instance.GetRandomIngredientType());
        OrderImage.sprite = ingredientData.AfterReady != null ? ingredientData.AfterReady : ingredientData.BeforeReady;
        StopAllCoroutines();
        StartCoroutine(SliderWaitTime(30f));
    }

    public void OnCoustomerServed(Ingredients ingredient, float score) {

        if (ingredient.GetIngredientData.IngredientType != ingredientData.IngredientType) return;

        OrderImage.gameObject.SetActive(false);
        PointsText.text = score.ToString();
        GameManager.Instance.DestroyPlayerIng();
        StopAllCoroutines();
        StartCoroutine(NewCoustomerWaitTime());
        StartCoroutine(CoustomerWaitTime(5f));
    }

    public float GetTimePassed(float time = 30f) {
        return Slider.value * time;
    }

    public float GetTimeLeft(float time = 30f) {
        return (1f - Slider.value) * time;
    }

    IEnumerator NewCoustomerWaitTime() {
        PointsText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        PointsText.gameObject.SetActive(false);
        OnNewCoustomer();
    }

    IEnumerator CoustomerWaitTime(float time) {
        Slider.gameObject.SetActive(true);
        Slider.value = 0;

        while (Slider.value < 1) {
            Slider.value += Time.deltaTime / time;
            yield return null;
        }

        Slider.value = 0;
        Slider.gameObject.SetActive(false);
    }

    IEnumerator SliderWaitTime(float time) {
        Slider.gameObject.SetActive(true);
        Slider.value = 1f;

        while (Slider.value > 0) {
            Slider.value -= Time.deltaTime / time;
            yield return null;
        }

        Slider.value = 0;
        Slider.gameObject.SetActive(false);
        //GameManager.Instance.OnUpdateScore(-GetTimeLeft());
        OnNewCoustomer();
    }

}
