using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 5f;
    [SerializeField] GameObject IngPlaceHolder;

    private Rigidbody rb;
    private Vector3 moveInput;



    void Awake() {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation
                       | RigidbodyConstraints.FreezePositionY;
        rb.useGravity = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    void OnStart() {


    }

    // Update is called once per frame
    void Update() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveInput = new Vector3(h, 0f, v).normalized;
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    public void SetIngredientToPlayer(GameObject obj) {
        obj.transform.position = IngPlaceHolder.transform.position;
        obj.transform.SetParent(this.transform);
    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("ChoppingTable")) {
            GameManager.Instance.OnChopping(GameManager.Instance.GetPlayerCurrentIngredient);
        } else if (other.gameObject.CompareTag("Stove")) {
            GameManager.Instance.OnCooking(GameManager.Instance.GetPlayerCurrentIngredient);
        } else if (other.gameObject.CompareTag("CoustomerTable")) {
            GameManager.Instance.OnserveCoustomer(other.gameObject.GetComponent<CoustomerWindow>());
        } else if (other.gameObject.CompareTag("Fridge")) {
            GameManager.Instance.OnPickIngredient();
        } else if (other.gameObject.CompareTag("Trash")) {
            GameManager.Instance.OnTrash();
        }
    }

}
