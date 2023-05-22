using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    private float platformSpeed = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlatformMovement();   
    }


    private void PlatformMovement()
    {
        transform.Translate(Vector3.left * Time.deltaTime * platformSpeed);
        if (transform.position.x <= -12f)
        {
           Destroy(gameObject);
        }
    }
}
