using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instant;
    public static GameManager Instant => instant;

    [SerializeField] GameObject enemyPrefab, meteoPrefab;

    float timeCount;
    [SerializeField] float timeMax=3;

    [SerializeField] int score;
    [SerializeField] int scoreHight;

    [SerializeField] Text scoreText;

    Camera cam;
    Vector2 camBorder;


    //List<GameObject> listEnemy  = new List<GameObject>();
    //List<GameObject> listMeteor = new List<GameObject>();
    private void Awake()
    {
        if (instant == null)
            instant = this;

        else if (instant.GetInstanceID() != this.GetInstanceID())
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        timeMax = Random.Range(2, 6);
        scoreHight = PlayerPrefs.GetInt(CONSTANT.HighScoreKey,0); // lấy dữ liệu 

        scoreText.text = string.Format("Điểm của bạn {0}\n", scoreHight);

        cam = Camera.main;
        camBorder = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height))/2;
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount >= timeMax)
        {
            Vector2 randPos = new Vector2(Random.Range(-10,10), Random.Range(-10,10))
                                +(Vector2)PlayerController.Instant.transform.position;

            Vector2 currentCamBorder = (Vector2)this.transform.position + camBorder;

            while(randPos.x > -currentCamBorder.x && randPos.x  < currentCamBorder.x ||
                randPos.y   > -currentCamBorder.y && randPos.y  < currentCamBorder.y)
            {
                randPos = new Vector2(Random.Range(-20f, 20f), Random.Range(-20f, 20f))
                          + (Vector2)PlayerController.Instant.transform.position;
            }

            if (Random.Range(0, 101) >= 50)
            {
                GameObject enemy = ObjectPooling.Instant.GetObject(enemyPrefab, randPos); //GetEnemy(randPos);
                enemy.SetActive(true);
            }

            else
            {
                GameObject meteor = ObjectPooling.Instant.GetObject(meteoPrefab, randPos); //GetMeteor(randPos);
                meteor.SetActive(true);
            }

            timeCount = 0;
            timeMax=Random.Range(2, 6);
        }
    }
    public void AddScore(int score)
    {
        if (score >= 0)
            this.score += score;

        if (this.score >= scoreHight)
        {
            scoreHight = this.score;
            PlayerPrefs.SetInt(CONSTANT.HighScoreKey, scoreHight); // lưu dữ liệu 
        }

        scoreText.text = string.Format("Điểm của bạn {0}\n", scoreHight);

    }

    //GameObject GetEnemy(Vector2 randPos)
    //{
    //    foreach(GameObject g in listEnemy)
    //    {
    //        if (g.activeSelf)
    //            continue;
    //        return g;
    //    }
    //    GameObject g2= Instantiate(enemyPrefab, randPos, Quaternion.identity);
    //    listEnemy.Add(g2);
    //    return g2;
    //}

    //GameObject GetMeteor(Vector2 randPos)
    //{
    //    foreach (GameObject g in listMeteor)
    //    {
    //        if (g.activeSelf)
    //            continue;
    //        return g;
    //    }
    //    GameObject g2 = Instantiate(meteoPrefab, randPos, Quaternion.identity);
    //    listMeteor.Add(g2);
    //    return g2;
    //}
}
