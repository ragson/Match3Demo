using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour {
   
    public int speed;
    public bool collide;

    public GameObject particles;
    public GameObject m_colider;




    private void Start()
    {
       
    }
    private void Update()
    {
        // GetComponent<Rigidbody2D>().AddForce(Vector2.down * speed * Time.deltaTime);

      
    }


    
   






    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

   


    private void OnCollisionEnter2D(Collision2D collision)
    {
        collide = true;

        //GameObject par = Instantiate(particles);
        //par.transform.position = collision.transform.position;
        //Destroy(par, 0.1f);
        //GetComponent<Rigidbody2D>().isKinematic = true;
      
        print("collision calling");

        //print("Colliding");
    }
    
}
