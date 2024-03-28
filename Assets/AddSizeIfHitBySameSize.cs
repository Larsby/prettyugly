using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSizeIfHitBySameSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	Vector3 GetSizeByTag(string tag, out string newTag) {
		Vector3 newSize = Vector3.zero;
		newTag = "Size1";
		if(tag.StartsWith("Size")) {

			if(tag.Equals("Size1")) {
				newSize = new Vector3(1.1f, 1.1f, 1.0f);
				newTag = "Size2";
				
			}
			else if (tag.Equals("Size2"))
			{
				newSize = new Vector3(1.2f, 1.2f, 1.0f);
				newTag = "Size3";
			}
			else if (tag.Equals("Size3"))
			{
				newSize = new Vector3(1.3f, 1.3f, 1.0f);
				newTag = "Size4";
			}
			else if (tag.Equals("Size4"))
			{
				newSize = new Vector3(1.4f, 1.4f, 1.0f);
				newTag = "Size5";
				print("you won the internet!");
			}



		}
		return newSize;
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		GameObject obj = col.gameObject;
	
		if(gameObject.CompareTag(obj.tag)) {
			string newTag = "";
			Vector3 newSize = GetSizeByTag(gameObject.tag,out newTag);
			if (newSize != Vector3.zero)
			{
				gameObject.transform.localScale = newSize;
				gameObject.tag = newTag;
				Destroy(col.gameObject);
			}
		}
	}
}
