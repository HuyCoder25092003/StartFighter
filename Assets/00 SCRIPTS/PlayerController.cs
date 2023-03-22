using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instant;
    public  static PlayerController Instant => instant; 

    Rigidbody2D rigi;
    [SerializeField] float speed,speedRoat;
    [SerializeField] GameObject _bullet;
    [SerializeField] float timeMax;
    [SerializeField] DataConfig data;

    //List<GameObject> bulletList = new List<GameObject>();

    float timeCount;

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
        rigi = GetComponent<Rigidbody2D>();
        speed = data.speed;
        speedRoat = data.speedRotate;
    }

    // Update is called once per frame
    void Update()
    {
        Moving();

        timeCount+=Time.deltaTime; 

        FireBullet();

    }

    void Moving()
    {
        Quaternion q = this.transform.rotation;

        float angle = q.eulerAngles.z + (-Input.GetAxisRaw("Horizontal") * speedRoat);

        q.eulerAngles = new Vector3(0, 0, angle);

        this.transform.rotation = q;


        rigi.velocity = this.transform.up * Input.GetAxisRaw("Vertical") * speed;
    }
    void FireBullet()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
            return;
        
        if(timeCount < timeMax)
            return;

        GameObject bullet = ObjectPooling.Instant.GetObject(_bullet); //this.GetBullet();
        bullet.transform.position = this.transform.position;
        bullet.transform.rotation = this.transform.rotation;
        bullet.SetActive(true);

        timeCount = 0;
    }
    //GameObject GetBullet()
    //{
    //    foreach(GameObject g in bulletList)
    //    {
    //        if (g.gameObject.activeSelf)
    //            continue;
    //        return g;
    //    }
    //    GameObject g2 = Instantiate(bullet, this.transform.position, this.transform.rotation);
    //    bulletList.Add(g2);
    //    return g2;
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
