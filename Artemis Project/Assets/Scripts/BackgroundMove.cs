using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float speed = 1f; 
    public bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( paused == false )
        {
            Vector3 target = new Vector3( transform.position.x, -5000f, transform.position.z );
            transform.position = Vector3.MoveTowards( transform.position, target, speed * Time.fixedDeltaTime );
            speed += 0.00001f;            
        }
    }
}
