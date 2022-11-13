using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;

    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        position.x = position.x + speed * Time.deltaTime;

        rigidbody2D.MovePosition(position);
    }
}
