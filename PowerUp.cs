using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private int _speed = 4;
    // Start is called before the first frame update
    void Start() 
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        // Screen Wrapping
        if ((transform.position.y <= -4.9)) Destroy(this.gameObject);

        // Enemy movement 
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
}
