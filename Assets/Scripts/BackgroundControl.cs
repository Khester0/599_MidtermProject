using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundControl : MonoBehaviour
{
    private float startpos, length;
    public GameObject cam;
    public float parallaxEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startpos = transform.position.x;

        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float movement = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

        if(movement > startpos + length)
        {
            startpos += length;
        }
        else if (movement < startpos - length)
        {
            startpos -= length;
             
        }
    }
}
