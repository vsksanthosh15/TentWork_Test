using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    [SerializeField] float GameTimer = 180f;
    [SerializeField] float CookingTime = 5;
    [SerializeField] float ChoppingTime = 2;


    [SerializeField] Player Player;
    [SerializeField] Fridge Fridge;
    [SerializeField] Stove Stove;
    [SerializeField] ChoppingTable ChoppingTable;
    [SerializeField] CoustomerWindow[] CoustomerWindows;
    [SerializeField] IngredientData[] IngredientData;
    [SerializeField] Ingredients IngredientPrefab;



    float gameTimer;
    float gameScore;
    Ingredients playerCurrentIngredient;
    float highScore;

    public float GetCookingTime => CookingTime;
    public float GetChoppingTime => ChoppingTime;

    public Ingredients GetIngredientPrefab => IngredientPrefab;
    public Player GetPlayer => Player;
    public Ingredients GetPlayerCurrentIngredient => playerCurrentIngredient;

    void Awake() {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start() {
        UiManager.Instance.ActiveStartLayer(true);
        UiManager.Instance.ActiveEndLayer(false);

        //OnStartGame();
    }

    // Update is called once per frame
    void Update() {

        if (gameTimer > 0) {
            gameTimer -= Time.deltaTime;
            UiManager.Instance.UpdateTimerText(gameTimer);
            if (gameTimer <= 0) {
                gameTimer = 0;
                UiManager.Instance.UpdateTimerText(gameTimer);
                OnGameEnd();
            }
        }
    }

    public void OnStartGame() {
        gameTimer = GameTimer;
        gameScore = 0;

        Fridge.OnStart();
        Stove.OnStart();
        ChoppingTable.OnStart();
        for (int i = 0; i < CoustomerWindows.Length; i++) {
            CoustomerWindows[i].OnStart();
        }

        UiManager.Instance.UpdateTimerText(gameTimer);
        UiManager.Instance.UpdateScoreText(gameScore);
        UiManager.Instance.ActiveStartLayer(false);

        if (playerCurrentIngredient != null) DestroyPlayerIng();
    }

    public void OnPickIngredient() {
        if(playerCurrentIngredient == null) Fridge.OnSpawnIngredient();
    }

    public void SetIngredientToPlayer(Ingredients ingredient) {
        playerCurrentIngredient = ingredient;
        Player.SetIngredientToPlayer(ingredient.gameObject);
    }

    public void OnChopping(Ingredients ingredient) {
        if (ChoppingTable.GetIsAvailable) ChoppingTable.OnChopping(ingredient);
        else ChoppingTable.OnTakeDish();
    }

    public void OnCooking(Ingredients ingredient) { 
        if(Stove.GetIsAvailable) Stove.OnCooking(ingredient);
        else Stove.OnTakeDish();
    }

    public void OnserveCoustomer(CoustomerWindow coustomer) {
        //float timePassed = coustomer.GetTimePassed();
        float timeleft = coustomer.GetTimeLeft();
        float score = Mathf.Round(playerCurrentIngredient.GetIngredientData.Score - timeleft);
        /*Debug.Log("Time Left : " + timeleft);
        Debug.Log(" Left Passed : " + timePassed);
        Debug.Log("Score : " + score);
*/
        OnUpdateScore(score);
        coustomer.OnCoustomerServed(playerCurrentIngredient, score);
    }

    public void OnUpdateScore(float points) {
        gameScore += points;
        UiManager.Instance.UpdateScoreText(gameScore);
    }

    public void OnTrash() {
        DestroyPlayerIng();
    }

    public void DestroyPlayerIng() {
        if(playerCurrentIngredient != null) Destroy(playerCurrentIngredient.gameObject);
        playerCurrentIngredient = null;
    }

    public void OnGameEnd() {
        UiManager.Instance.ActiveEndLayer(true);
        if(gameScore > highScore) {
            highScore = gameScore;
            UiManager.Instance.UpdateHighScoreText(gameScore);
        }
    }

    public void OnRestartGame() {
        OnStartGame();
        UiManager.Instance.ActiveEndLayer(false);
    }

    public IngredientData GetIngredientData(IngredientType type) {
        for (int i = 0; i < IngredientData.Length; i++) {
            if (IngredientData[i].IngredientType == type) return new IngredientData(IngredientData[i]);
        }
        return null;
    }

    public IngredientType GetRandomIngredientType() {
        return IngredientData[Random.Range(0, IngredientData.Length)].IngredientType;
    }

}
