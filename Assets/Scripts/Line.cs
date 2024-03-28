using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour {

    public GameObject gameObject1;          // Reference to the first GameObject
    public GameObject gameObject2;          // Reference to the second GameObject
	public GameObject sprite;
    private LineRenderer line;  						 // Line Renderer

	// Use this for initialization
	void Start () {
        // Add a Line Renderer to the GameObject
		line = this.gameObject.GetComponent<LineRenderer>();
		if (line == null) {
			line = this.gameObject.AddComponent<LineRenderer> ();
			// Set the width of the Line Renderer
		
		}
		line.SetWidth (0.05F, 0.05F);
        // Set the number of vertex fo the Line Renderer
    //    line.SetVertexCount(2);
	}
	
	// Update is called once per frame
	void Update () {
        // Check if the GameObjects are not null
        if (gameObject1 != null && gameObject2 != null)
        {
            // Update position of the two vertex of the Line Renderer
            line.SetPosition(0, gameObject1.transform.position);
            line.SetPosition(1, gameObject2.transform.position);
        }
		/*
		Vector2 curPosition = gameObject1.transform.position;
		//curPosition.x = curPosition.x + 01f;
	
		float distance = (sprite.transform.position.y-sprite.transform.localScale.y) - (gameObject2.transform.position.y -gameObject2.transform.localScale.y);

		float scale = distance;

		scale = Mathf.Abs (scale);

		sprite.transform.localScale =new Vector3 (scale, scale, 1.0f);
		sprite.transform.position = curPosition;
		Vector3 mousePosition = gameObject2.transform.position;
		sprite.transform.rotation = Quaternion.LookRotation (Vector3.forward, mousePosition - sprite.transform.position);

*/

	}
}
