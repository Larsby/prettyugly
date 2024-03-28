using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceBlobSpeedWhenCollide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	void OnTriggerEnter2D(Collider2D col)
	{
		return;
		
		if(col.gameObject.CompareTag("Blob")) {
			Rigidbody2D rigidbody = col.gameObject.GetComponent<Rigidbody2D>();
			rigidbody.velocity = new Vector2(rigidbody.velocity.x * 0.2f, rigidbody.velocity.y * 0.2f);
		}
	}


}
