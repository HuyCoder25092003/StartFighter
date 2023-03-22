using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject player;
    Rigidbody2D rigi;
    [SerializeField] float speed;
    [SerializeField] LayerMask layerMask;

    float countTime;
    [SerializeField] float maxTimeShoot=3;

    [SerializeField] GameObject _bullet;

    List<GameObject>shootBullets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        rigi=this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        countTime-=Time.deltaTime;

        Vector2 direction = player.transform.position - this.transform.position;
        RaycastHit2D hits=Physics2D.Raycast(this.transform.position, direction, direction.sqrMagnitude,layerMask);
        if (hits.collider != null)
        {
            rigi.velocity = Vector2.zero;
            return;
        }

        ChasePlayer();
        ShootPlayer();
    }
    void ChasePlayer()
    {
        Vector2 direction = player.transform.position - this.transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        Quaternion q = this.transform.rotation;
        q.eulerAngles = new Vector3(0, 0, angle);
        this.transform.rotation = q;

        rigi.velocity = this.transform.up * speed;
    }

    void ShootPlayer()
    {
        if (countTime > 0)
            return;

        GameObject bullet = ObjectPooling.Instant.GetObject(_bullet);   //GetEnemyShootsBullet();
        bullet.transform.position = this.transform.position;
        bullet.transform.rotation = this.transform.rotation;
        bullet.SetActive(true);

        countTime = maxTimeShoot;
    }

    //GameObject GetEnemyShootsBullet()
    //{
    //    foreach(GameObject g in shootBullets)
    //    {
    //        if (g.gameObject.activeSelf)
    //            continue;
    //        return g;
    //    }
    //    GameObject g2 = Instantiate(_bullet, this.transform.position, this.transform.rotation);
    //    shootBullets.Add(g2);
    //    return g2;
    //}
}
