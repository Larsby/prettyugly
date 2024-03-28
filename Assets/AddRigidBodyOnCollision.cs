using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRigidBodyOnCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	void OnCollisionEnter2D(Collision2D col)
	{
		print("" + col.gameObject.tag);
		//	if(col.gameObject.CompareTag("Blob")){
		Rigidbody2D r = col.gameObject.GetComponent<Rigidbody2D>();
		if(r != null && r.velocity != Vector2.zero){
		if(gameObject.GetComponent<Rigidbody2D>() == null){
			Rigidbody2D rigid = gameObject.AddComponent<Rigidbody2D>();
			rigid.mass = 2.0f;
			rigid.angularDrag = 0.8f;
			rigid.drag = 5.0f;
			rigid.gravityScale = 1.0f;
			Vector2 v= col.gameObject.GetComponent<Rigidbody2D>().velocity;
			rigid.velocity = new Vector2(v.x * -1.0f, v.y * -1.0f);
			//rig.collisionDetectionMode = RigidbodyInterpolation2D.
		}
		}
	}
}
