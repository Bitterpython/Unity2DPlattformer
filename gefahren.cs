using UnityEngine;

public class Gefahren : MonoBehaviour
{
    float xStart;
    float xAenderung;

      
    void Start()
    {
        xStart = transform.position.x;
        xAenderung = Random.Range(0.4f, 0.8f) * Time.deltaTime;
    }

    void Update()
    {
        float xNeu = transform.position.x + xAenderung;
        transform.position = new Vector3(xNeu, transform.position.y, 0);

        if ((xNeu > xStart + 2) || (xNeu < xStart -2))
        {
            xAenderung = -1 * xAenderung; 
        }
    }
}