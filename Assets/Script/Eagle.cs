using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    [SerializeField]
    Transform Player;

    [SerializeField]
    float eagleHieght = 2;

    [SerializeField]
    float speed = 1;
    SpriteRenderer sr;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        startPos = transform.position;
        StartCoroutine(EagleAnimation());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.position.x > transform.position.x)
            sr.flipX = true;
        else
            sr.flipX = false;
    }
    IEnumerator EagleAnimation() // Return yield 
    {
        Vector3 endPos = new Vector3(startPos.x, startPos.y + eagleHieght, startPos.z);
        bool isFlight = true;
        float value = 0;
        while (true)
        {
            yield return null;
            //Debug.Log("Move!");
            if (isFlight)
                transform.position = Vector3.Lerp(startPos, endPos, value);
            else
                transform.position = Vector3.Lerp(endPos, startPos, value);

            value = value + Time.deltaTime * speed;
            if(value > 1)
            {
                value = 0;
                isFlight = !isFlight;

            }

        }
    
    
    
    }



}
