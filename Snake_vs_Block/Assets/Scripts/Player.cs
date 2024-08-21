using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public GameState GameState; // ��������� ����
    public Transform SnakeHead; // ��������� ������ ����
    public TextMesh Health;


    private SnakeTail snakeTail;
    public Rigidbody snakeBodyRB;

    private Vector3 _previousMousePosition; // ���������� ������� ����
    public float moveSpeed; // �������� �������� ������ ������
    public float Sensitivity; // ���������������� ����
    private float LeftBorder = -2.3f; // ����� �������
    private float RightBorder = 2.3f;    // ������ �������

    private List<Transform> BodyParts = new List<Transform>();

    private int foodValue; // �������� ���
    private int blockValue; // �������� ������

    [SerializeField]
    public int snakeHealth = 4; // �������� ������
    [SerializeField]
    public int snakeLength = 1;  // ����� ������

    public Text FoodCounter;
    public int foodCounter;


    void Start()
    {
        foodCounter = 0;
        FoodCounter.text = foodCounter.ToString();
        snakeBodyRB = GetComponent<Rigidbody>();
        snakeTail = GetComponent<SnakeTail>();
        Health.text = snakeHealth.ToString(); // ����������� ����� �������� (����� � ������)
        IncreaseSnake(snakeHealth);
        Debug.Log($"Health = {snakeHealth}, Length = {snakeLength}");
    }

    void Update()
    {
        MoveHead();
        FoodCounter.text = foodCounter.ToString();
    }

    private void MoveHead()
    {
        // ���������� �������� ������
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;

        //��� ������� ������� ��� ��������� ������ � ����� �� ��� � 
        if (Input.GetMouseButton(0))
        {
            // ������� ����� � ������
            if (transform.position.x <= LeftBorder)
            {
                transform.position = new Vector3(LeftBorder, transform.position.y, transform.position.z);
            }
            if (transform.position.x >= RightBorder)
            {
                transform.position = new Vector3(RightBorder, transform.position.y, transform.position.z);
            }
            else
            {
                Vector3 delta = Input.mousePosition - _previousMousePosition;
                transform.position = new Vector3(transform.position.x + delta.x * Sensitivity * Time.deltaTime,
                    transform.position.y,
                    transform.position.z);
            }
        }
        _previousMousePosition = Input.mousePosition;
    }

    // ��������� ������������ � ������ ��� ����
    public void OnCollisionEnter(Collision collision)
    {
        // ��� ������������ � ����
        if (collision.gameObject.tag == "Food")
        {
            foodValue = collision.gameObject.GetComponent<Food>().Value;

            foodCounter += foodValue;

            snakeHealth += foodValue; // �������� �������� � ��������
            Health.text = snakeHealth.ToString();
            Destroy(collision.gameObject);
            IncreaseSnake(foodValue);

            Debug.Log($"FOOD value = {foodValue}: Health = {snakeHealth}, Length = {snakeLength}");
            
        }

        // ��� ������������ � ������
        if (collision.gameObject.tag == "Block")
        {
            // �������� ��������� �����
            blockValue = collision.gameObject.GetComponent<Block>().Value;

            // ����� ���������� � ������������ � �������:
            ShowBlockHitInfo();

            // ���� �������� ������ ������ ��������� �����, ��  GameOver!
            if (snakeHealth <= blockValue)
            {
                // ����� ���������� � ������������ � �������:
                ShowBlockHitInfo();

                snakeHealth -= blockValue;
                // GameOver!
                GameState.OnPlayerDead();
                // ������������� ������
                snakeBodyRB.velocity = Vector3.zero;
                Debug.Log($"Health remained = {snakeHealth}, Length remained = {snakeLength}");
            }

            // ���� �������� ������ ������ ��������� �����, ��
            else
            {
                //Debug.LogWarning($"Hit Block!");
                ShowBlockHitInfo();

                // ���������� ����
                Destroy(collision.gameObject);
                // �������� �� �������� � ������ ��������� �����
                snakeHealth -= blockValue;
                // ��������� ����� ������ �� ��������� �����
                DecreaseSnake(blockValue);
                // ����������� ������� ������ � �����
                Health.text = snakeHealth.ToString();
                Debug.Log($"Health remained = {snakeHealth}, Length remained = {snakeLength}");
            }
        }
    }

    public void IncreaseSnake(int Value)
    {
        for (int i = 0; i < Value; i++)
        {
            snakeLength++;
            snakeTail.AddBody();
        }
    }

    public void DecreaseSnake(int Value)
    {
        for (int i = 0; i < Value; i++)
        {
            snakeLength--;
            snakeTail.RemoveBody();
        }
    }

    public void ShowBlockHitInfo() 
    {
        Debug.LogWarning($"Hit Block! {blockValue}");
        Debug.Log($"Health current: {snakeHealth}, Length current: {snakeLength}");
    }
}
