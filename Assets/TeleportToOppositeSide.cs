using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToOppositeSide : MonoBehaviour
{
	public Transform teleportTransform;
	public bool x = false;
	public bool y = false;

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
		
		GameObject root = col.gameObject;
	//	Debug.Log("tag" + root.tag + "name" + root.name);
		if (root.CompareTag("Blob") == false)
		{
			while (root.transform.parent)
			{
				root = root.transform.parent.gameObject;

			}
		}
		//col.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
		if(x) {
			root.transform.position = new Vector3(teleportTransform.position.x, root.transform.position.y,1.0f);
		}
		if(y){
			root.transform.position = new Vector3( root.transform.position.x,teleportTransform.position.y,1.0f);
		}
	}
}
