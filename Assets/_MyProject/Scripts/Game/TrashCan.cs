using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField] private float trashCanSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TrashCanMovement();
    }

    private void TrashCanMovement()
    {
        transform.Translate(Vector3.left * Time.deltaTime * trashCanSpeed);
        if (transform.position.x <= -12f)
        {
            Destroy(gameObject);
        }
    }
}
