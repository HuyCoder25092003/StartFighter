using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    private static MeteorController instant;
    public static MeteorController Instant => instant;

    [SerializeField] float angle;

    [SerializeField] float speed;

    [SerializeField] GameObject meteoSmall;

    Rigidbody2D rigi;

    List<GameObject> listMeteor = new List<GameObject>();

    private void Awake()
    {
        if (instant == null)
            instant = this;
        else if (!instant.GetInstanceID().Equals(this.GetInstanceID()))
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        angle = Random.Range(0,360);
        Quaternion quaternion = this.transform.rotation;
        quaternion.eulerAngles = new Vector3(0,0,angle);
        this.transform.rotation = quaternion;

        rigi = this.GetComponent<Rigidbody2D>();
        this.GetComponent<Collider2D>().enabled = false;

        StartCoroutine(WaitTime());
    }

    // Update is called once per frame
    void Update()
    {
        this.rigi.velocity = this.transform.up * speed;
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(1);
        this.GetComponent<Collider2D>().enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.gameObject.SetActive(false);
        if(meteoSmall)
            for (int i = 0; i < Random.Range(2, 4); i++)
            {
                GameObject meteor = ObjectPooling.Instant.GetObject(meteoSmall); //BornMeteorite();
                meteor.transform.position = this.transform.position;
                meteor.transform.rotation = this.transform.rotation;
                meteor.SetActive(true);
            }

        if (collision.gameObject.GetComponent<Bullet>())
            return;

        collision.gameObject.SetActive(false);
    }

    //GameObject BornMeteorite()
    //{
    //    foreach(GameObject g in listMeteor)
    //    {
    //        if (g.gameObject.activeSelf)
    //            continue;
    //        return g;
    //    }
    //    GameObject g2 = Instantiate(meteoSmall, this.transform.position, Quaternion.identity);
    //    listMeteor.Add(g2);
    //    return g2;
    //}

}
