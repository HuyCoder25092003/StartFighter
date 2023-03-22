using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]Transform playerTransform;
    [SerializeField] float speed=3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = playerTransform.position;
        pos.z = -10;

        this.transform.position = Vector3.MoveTowards(this.transform.position, pos, speed*Time.deltaTime);
    }
}
