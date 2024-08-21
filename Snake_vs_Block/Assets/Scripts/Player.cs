using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public GameState GameState; // состояние игры
    public Transform SnakeHead; // трансформ головы змеи
    public TextMesh Health;


    private SnakeTail snakeTail;
    public Rigidbody snakeBodyRB;

    private Vector3 _previousMousePosition; // предыдущаа позиция мыши
    public float moveSpeed; // скорость движения змейки вперед
    public float Sensitivity; // чувствительность мыши
    private float LeftBorder = -2.3f; // левая граница
    private float RightBorder = 2.3f;    // правая граница

    private List<Transform> BodyParts = new List<Transform>();

    private int foodValue; // ценность еды
    private int blockValue; // ценность блоков

    [SerializeField]
    public int snakeHealth = 4; // здоровье змейки
    [SerializeField]
    public int snakeLength = 1;  // длина змейки

    public Text FoodCounter;
    public int foodCounter;


    void Start()
    {
        foodCounter = 0;
        FoodCounter.text = foodCounter.ToString();
        snakeBodyRB = GetComponent<Rigidbody>();
        snakeTail = GetComponent<SnakeTail>();
        Health.text = snakeHealth.ToString(); // отображение числа здоровья (шаров в змейке)
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
        // постоянное движение вперед
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;

        //при зажатой клавише ЛКМ двигается вправа и влево по оси Х 
        if (Input.GetMouseButton(0))
        {
            // границы слева и справа
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

    // Обработка столкновений с блоком или едой
    public void OnCollisionEnter(Collision collision)
    {
        // При столкновении с едой
        if (collision.gameObject.tag == "Food")
        {
            foodValue = collision.gameObject.GetComponent<Food>().Value;

            foodCounter += foodValue;

            snakeHealth += foodValue; // прибавим значение к здоровью
            Health.text = snakeHealth.ToString();
            Destroy(collision.gameObject);
            IncreaseSnake(foodValue);

            Debug.Log($"FOOD value = {foodValue}: Health = {snakeHealth}, Length = {snakeLength}");
            
        }

        // При столкновении с блоком
        if (collision.gameObject.tag == "Block")
        {
            // Получаем прочность блока
            blockValue = collision.gameObject.GetComponent<Block>().Value;

            // Вывод информации о столкновении в консоль:
            ShowBlockHitInfo();

            // если здоровье змейки меньше прочности блока, то  GameOver!
            if (snakeHealth <= blockValue)
            {
                // Вывод информации о столкновении в консоль:
                ShowBlockHitInfo();

                snakeHealth -= blockValue;
                // GameOver!
                GameState.OnPlayerDead();
                // останавливаем змейку
                snakeBodyRB.velocity = Vector3.zero;
                Debug.Log($"Health remained = {snakeHealth}, Length remained = {snakeLength}");
            }

            // если здоровье змейки больше прочности блока, то
            else
            {
                //Debug.LogWarning($"Hit Block!");
                ShowBlockHitInfo();

                // Уничтожаем блок
                Destroy(collision.gameObject);
                // Отнимаем от здоровье у змейки прочность блока
                snakeHealth -= blockValue;
                // Уменьшаем длину змейки на прочность блока
                DecreaseSnake(blockValue);
                // преобразуем здровье змейки в текст
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
