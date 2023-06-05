using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Header("����������")]
    public float timeDestroy = 3f;
    public float speed = 3f;

    //����������
    public Rigidbody2D rb;
    public Vector3 tp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = tp * speed;
        Destroy(this.gameObject, timeDestroy);
    }
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Enemy"))//����
        {
            Destroy(collider2D.gameObject);
            Destroy(this.gameObject);
        }
    }
}
