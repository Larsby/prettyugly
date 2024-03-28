using UnityEngine;
using System.Collections;

public class ScaleCamera : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		// Adjust the camera to show world position 'centeredAt' - (0,0,0) or other - with
		// the display being at least 480 units wide and 360 units high.
		Camera camera = Camera.main;
		Vector3 minimumDisplaySize = new Vector3 (480, 360, 0);

		float pixelsWide = camera.pixelWidth;
		float pixelsHigh = camera.pixelHeight;

		// Calculate the per-axis scaling factor necessary to fill the view with
		// the desired minimum size (in arbitrary units).
		float scaleX = pixelsWide / minimumDisplaySize.x;
		float scaleY = pixelsHigh / minimumDisplaySize.y;

		// Select the smaller of the two scale factors to use.
		// The corresponding axis will have the exact size specified and the other 
		// will be *at least* the required size and probably larger.
		float scale = (scaleX < scaleY) ? scaleX : scaleY;

		Vector3 displaySize = new Vector3 (pixelsWide / scale, pixelsHigh / scale, 0);

		// Use some magic code to get the required distance 'z' from the camera to the content to display
		// at the correct size.
		float z = displaySize.y /
		          (2 * Mathf.Tan ((float)camera.fieldOfView / 2 * Mathf.Deg2Rad));

		// Set the camera back 'z' from the content.  This assumes that the camera
		// is already oriented towards the content.
		camera.transform.position = new Vector3 (0, 0, -z);

		// The display is showing the region between coordinates 
		// "centeredAt - displaySize/2" and "centeredAt + displaySize/2".

		// After running this code with minimumDisplaySize 480x360, displaySize will
		// have the following values on different devices (and content will be full-screen
		// on all of them):
		//    iPad Air 2 - 480x360
		//    iPhone 1 - 540x360
		//    iPhone 5 - 639x360
		//    Nexus 6 - 640x360

		// As another example, after running this code with minimumDisplaySize 15x11
		// (tile dimensions for a tile-based game), displaySize will end up with the following 
		// actual tile dimensions on different devices (every device will have a display
		// 11 tiles high and 15+ tiles wide):
		//    iPad Air 2 - 14.667x11
		//    iPhone 1 - 16.5x11
		//    iPhone 5 - 19.525x11
		//    Nexus 6 - 19.556x11
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
