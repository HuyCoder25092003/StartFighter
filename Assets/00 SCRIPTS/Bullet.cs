using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rigi;
    [SerializeField] float speed;
    [SerializeField] float timeDestruc = 5;

    // Start is called before the first frame update
    void Start()
    {
        rigi = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigi.velocity = this.transform.up * speed;

        StartCoroutine(autoDestruc());
    }
    IEnumerator autoDestruc()
    {
        yield return new WaitForSeconds(timeDestruc);

        this.gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.gameObject.SetActive(false);

        if (collision.gameObject.GetComponent<EnemyController>())
        {
            GameManager.Instant.AddScore(1);
            collision.gameObject.SetActive(false);
        }
    }
}
