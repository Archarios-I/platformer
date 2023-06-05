using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 3.5f;
    public bool ableToJump = false;
    [SerializeField] private int jumpForce = 7;
    public Rigidbody2D rb;
    public Vector2 moveVector;
    private float DirectionX;
    private float DirectionY = 0;
    public SpriteRenderer sr;
    [SerializeField] private float RayDistance = 1.2f;
    public int score = 0;
    public TMP_Text textScore;
    public Animator animator;
    [SerializeField] private GameObject CanvasInventory;
    private inventoryScript inventory;
    public GameObject fireball;
    public bool isDied = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        textScore.text = $"Score: {score}";
        inventory = CanvasInventory.GetComponent<inventoryScript>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        IsGround();
        IsDied();
        Flip();
        Animation();
        Attack();
    }

    private void PlayerMove()//движение
    {
        DirectionX = Input.GetAxis("Horizontal");

        Vector2 movement = new(DirectionX, DirectionY);
        transform.Translate(speed * Time.deltaTime * movement);

        if (ableToJump == true && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void IsGround()//Проверка на землю
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, RayDistance, LayerMask.GetMask("Ground"));

        if (hit.collider != null) ableToJump = true;
        else ableToJump = false;
    }

    //void IsLadders()//Проверка на лестницу
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, 0.1f, LayerMask.GetMask("Ladders"));

    //    if (hit.collider != null)
    //    {
    //        ableToJump = false;
    //        DirectionY = Input.GetAxis("Vertical");
    //        if (DirectionY > 0)
    //        {
    //            rb.AddForce(Vector2.up * 0.05f, ForceMode2D.Impulse);
    //        }
    //    }
    //    //else
    //    //{
    //    //    ableToJump = true;
    //    //    DirectionY = 0;
    //    //}
    //}

    void IsDied()
    {
        if (rb.position.y < -20)
        {
            SceneManager.LoadScene(1);
        }
        if (isDied == true)
        {
            SceneManager.LoadScene(1);
        }
    }

    void Flip() //Передвижение влево с поворотом персонажа
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            sr.flipX = false;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            sr.flipX = true;
        }
    }

    void Animation() //Анимация ходьбы
    {
        animator.SetBool("isWalking", false);
        if (Input.GetAxis("Horizontal") > 0)
        {
            animator.SetBool("isWalking", true);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            animator.SetBool("isWalking", true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "coinSilver":
                Destroy(collision.gameObject);
                score += 5;
                textScore.text = $"Score: {score}";
                break;

            case "emerald":
                Destroy(collision.gameObject);
                score += 33;
                textScore.text = $"Score: {score}";
                break;

            default:
                break;
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (sr.flipX == true)
            {
                fireball.GetComponent<SpriteRenderer>().flipX = true;
                fireball.GetComponent<Fireball>().tp = new Vector3(-2.5f, 0, 0);
            }
            if (sr.flipX == false)
            {
                fireball.GetComponent<SpriteRenderer>().flipX = false;
                fireball.GetComponent<Fireball>().tp = new Vector3(2.5f, 0, 0);
            }
            Instantiate(fireball, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("key"))
        {
            collision.gameObject.SetActive(false);
            inventory.GetKey();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isDied = true;
        }
    }
}
