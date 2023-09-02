using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed = 40f;

    public GameObject explosionVFX;

    private bool canMove = false;

    public int whatTypeOfThisCar = 0;

    private Vector3 startPos;

    private void Awake()
    {

        startPos = transform.position;
    }

    private void OnMouseDown()
    {
        canMove = true;
    }

    private void FixedUpdate()
    {
        //rb.velocity = new Vector2(moveSpeed, 0f);
        if (!canMove) return;

        if (whatTypeOfThisCar == 0 || whatTypeOfThisCar == 1)
        {
            transform.Translate(new Vector2(0, -1) * speed * Time.deltaTime);
        }
        else if (whatTypeOfThisCar == 2)
        {
            transform.Translate(new Vector2(1, 0) * speed * Time.deltaTime);
        }
        else if (whatTypeOfThisCar == 3)
        {
            transform.Translate(new Vector2(-1, 0) * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].cars.Remove(this);
            GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
            Destroy(explosion, .75f);
            Destroy(gameObject);
        } 
        else if (collision.gameObject.tag == "Car")
        {
            canMove = false;

            transform.position = startPos;
        }
    }
}
