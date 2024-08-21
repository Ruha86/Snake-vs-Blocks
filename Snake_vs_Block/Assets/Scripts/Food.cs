using Random = System.Random;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int Value;
    public TextMesh FoodValue;

    Random random = new Random();

    private int minFoodValue = 1;
    private int maxFoodValue = 11;

    private void Start()
    {
        Value = random.Next(minFoodValue, maxFoodValue);
        FoodValue.text = Value.ToString();
    }

}
