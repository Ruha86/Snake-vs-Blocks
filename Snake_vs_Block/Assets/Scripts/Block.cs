using Random = System.Random;
using UnityEngine;

public class Block : MonoBehaviour
{
    public TextMesh BlockValue;
    public int Value;
    Color lerpedColor = Color.white;

    // public Material BlockMat;

    Random random = new Random();

    private int minBlockValue = 1;
    private int maxBlockValue = 31;

    
    void Start()
    {
        Value = random.Next(minBlockValue, maxBlockValue);
        BlockValue.text = Value.ToString();
        lerpedColor = Color.Lerp(Color.red, Color.black, (float)Value / 21f);
        this.GetComponent<Renderer>().material.color = lerpedColor;


    }
    
}
