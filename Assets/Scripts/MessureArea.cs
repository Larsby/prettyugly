using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;

public class MessureArea : MonoBehaviour, IWeight
{
	
	public class Land
	{
		public	Land ()
		{
			eaten = false;	
		}

		public bool eaten;
		public Vector2 position;
		public GameObject sprite;
	};

	public class EatPoint
	{
		public EatPoint ()
		{
		}

		public IWeight weight;
		public Vector2 point;
		public bool processed = false;
	};

	public static MessureArea instance = null;
	Dictionary <Vector2, Land> availableLand;
	Land[,] landList;
	float xstart = 0f;
	float xend = 0f;
	float ystart = 0f;
	float yend = 0f;
	public float radius = 0.1f;
	float x;
	float y;
	bool messure = true;
	int sprites = 0;
	int blocks = 0;
	public float percentage = 0.7f;
	public string prefabName = "MeshCube";
	public bool inverse = false;
	public List<Land> avail;
	public List<EatPoint> listToGoThrough;
	private CombineMeshes meshCombine;
	public GameObject originalObject;
	private bool busy;
	//System.Tuple
	private bool calculatedOnce = false;
	GameObject CreateObject (Vector2 pos)
	{
		PooledObject po = originalObject.GetComponent<PooledObject> ().GetPooledInstance<PooledObject> ();

		GameObject spriteObject = po.gameObject;
		//DontDestroyOnLoad (spriteObject);
		po.Pool.gameObject.transform.parent = transform;
	//	originalObject.GetComponent<PooledObject> ().GetPooledInstance<PooledObject> ().Pool.gameObject.transform.parent = transform;
		spriteObject.GetComponent<FadeShaderColor> ().Init ();
	//	GameObject spriteObject = (GameObject)Instantiate (originalObject);
		spriteObject.transform.position = new Vector3 (pos.x, pos.y, 1f);
		spriteObject.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);
		spriteObject.transform.parent = po.transform;
		return spriteObject;
		//CombineAndRemove ();
	}

	GameObject CreateLineObject(Vector3 start, Vector3 end) {
	
		GameObject spriteObject = (GameObject)Instantiate (Resources.Load ("BackgroundLine"));
		spriteObject.transform.position = new Vector3 (start.x, start.y, 1f);
		//spriteObject.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);
		spriteObject.transform.parent = transform;
		LineRenderer ren = spriteObject.GetComponent<LineRenderer> ();
		ren.positionCount = 2;
		ren.SetPosition(0,start);
		ren.SetPosition(1,end);
		return spriteObject;
	}

	void CombineAndRemove ()
	{
		//meshCombine.Combine ();

	
	}
	void Start() {
		originalObject = (GameObject)Instantiate (Resources.Load (prefabName));
		calculatedOnce = false;
	}
	void IWeight.ChangeWeight ()
	{
		
	}

	void IWeight.AlreadyEaten ()
	{
	}

	void Awake ()
	{

		if (instance == null)
			instance = this;
		else if (instance != this) {
			//Destroy (gameObject); 
			Destroy(this);
			instance.Calculate ();
			return;
		}
	 



		//DontDestroyOnLoad (gameObject);
		//Clear ();
		Start ();

	}


	int getIndexX (float start, float coord)
	{
		float reminder = coord - start;
		float res = reminder / radius;
		//	return res; // xindex

		return   System.Convert.ToInt32 (res);
	}

	int getIndexY (float start, float coord)
	{
		float reminder = coord + start;
		float res = reminder / radius;
		//	return res; // xindex

		return   System.Convert.ToInt32 (res);
	}


	// Use this for initialization
	public void Calculate ()
	{
	//	meshCombine = GetComponent<CombineMeshes> ();
		//	meshCombine.Clear ();
		busy = false;
	

		avail = new List<Land> ();
		listToGoThrough = new List<EatPoint> ();
		Vector3 screenPoint = Camera.main.ViewportToScreenPoint (new Vector3 (0.0f, 1.0f, 5.0f));
		Vector3 start = Camera.main.ScreenToWorldPoint (new Vector3 (0, Camera.main.pixelHeight, Camera.main.nearClipPlane));
		Vector3 end = Camera.main.ScreenToWorldPoint (new Vector3 (Camera.main.pixelWidth, 0, Camera.main.nearClipPlane));
		ystart = start.y;
		xstart = start.x;
		yend = end.y;
		xend = end.x + (radius);
		x = xstart;
		y = ystart;
		sprites = 0;
		blocks = 0;
		messure = true;
	}

	public void Clear ()
	{

		//GetComponent<CombineMeshes> ().Clear ();
	
	}

	void ReturnChildrenToPool ()
	{
		for (int i = transform.childCount - 1; i >= 0; i--) {
			PooledObject p = transform.GetChild (i).gameObject.GetComponent<PooledObject>();
			if(p != null) {
				if (gameObject.active) {
					p.ReturnToPool ();
				}
			}
	}
	}

	void OnLevelWasLoaded (int level)
	{
		Start ();
		Calculate ();
			ReturnChildrenToPool();
	}

	public void Messure ()
	{
		
		Calculate ();
		int totalArea = 0;
		for (float x = xstart; x < xend; x += radius) {
			for (float y = ystart; y > yend; y -= radius) {

				Vector2 position = new Vector2 (x, y);
				Vector3 viewPos = Camera.main.WorldToViewportPoint (position);
				if ((viewPos.x < -0.9f || viewPos.x > 1.1f) || (viewPos.y < -0.9f || viewPos.y > 1.1f)) {
					// outside of view so don't do nothin
				} else {
					//Vector2 worldPos = Camera.main.WorldToViewportPoint (position);
					Collider2D collider = Physics2D.OverlapCircle (position, radius);
					if (collider == null || !collider.gameObject.CompareTag ("Obstacle")) {
						if (inverse) {
							CreateObject (position);
						}
						sprites++;

				

					} else {
						if (inverse) {
							//	CreateObject (position);
						}
						blocks++;

					}
				}

			}
		}
		landList = new Land[sprites, sprites];
		int xi = 0;
		int yi = 0;
		for (float x = xstart; x < xend; x += radius) {
			yi = 0;
			for (float y = yend; y < ystart; y += radius) {

				Vector2 position = new Vector2 (x, y);
				Vector3 viewPos = Camera.main.WorldToViewportPoint (position);
				if ((viewPos.x < -0.9f || viewPos.x > 1.1f) || (viewPos.y < -0.9f || viewPos.y > 1.1f)) {
					// outside of view so don't do nothin
				} else {
					//Vector2 worldPos = Camera.main.WorldToViewportPoint (position);
					Collider2D collider = Physics2D.OverlapCircle (position, radius);
					if (collider == null || !collider.gameObject.CompareTag ("Obstacle")) {
						//	CreateObject (position);
						//sprites++;
				
						Land land = new Land ();
						land.eaten = false;
						land.position = position;
						avail.Add (land);
					    
						landList [xi, yi] = land;
			
					} 
				}

				yi++;
			}

			xi++;
		}
		totalArea = sprites + blocks;
		int max = (int)(sprites * percentage);
		GameManager.instance.setMaxFood (max);
		calculatedOnce = true;

	}

	public bool HaveCalculatedPaths() {
		return calculatedOnce;
	}
	public bool LandAvailable (Vector2 point)
	{
		if (messure) {
			Messure ();
			messure = false;
		}
		EatAt (point, this);
		return false;
	}

	public void RepairAt (Vector2 point)
	{
		EatPoint eat = new EatPoint ();
		eat.point = point;
		eat.weight = null;
		eat.processed = false;



		int xindex = getIndexX (xstart, point.x);
		int yindex = getIndexY (ystart, point.y);
		if (xindex >= 0 && yindex >= 0) {
			if (landList == null) {
				Messure ();
			}

			Land l = landList [xindex, yindex];
			if (l != null) {
				if (Vector2.Distance (l.position, eat.point) < 0.1f) {
					if (l.eaten == true) {

						GameManager.instance.removeFood ();
						l.eaten = false;
						DestroyImmediate (l.sprite);
						l.sprite = null;


					}
				}
			}
		}
	}

	public bool EatAt (Vector2 point, IWeight weight)
	{
		EatPoint eat = new EatPoint ();
		eat.point = point;
		eat.weight = weight;
		eat.processed = false;
	

	
		int xindex = getIndexX (xstart, point.x);
		int yindex = getIndexY (ystart, point.y);
		if (xindex >= 0 && yindex >= 0) {
			if (landList == null) {
				Messure ();
			}

			Land l = landList [xindex, yindex];
			if (l != null) {
				if (Vector2.Distance (l.position, eat.point) < 0.2f) {
					if (l.eaten == false) {
						weight.ChangeWeight ();
						GameManager.instance.addFood ();
						l.eaten = true;
						//Vector3 start = new Vector3 (l.position.x, l.position.y, 1.0f);
						//Vector3 end = new Vector3 (point.x, point.y, 1.0f);



						l.sprite = CreateObject (l.position);
						//CreateObject (point);
						//CreateObject (point);
						return true;
					} else {
						//weight.AlreadyEaten ();
						return false;
					}
				}
			}
		}
		return false;
	}

	void Update ()
	{
		if (GameManager.instance.IsReady ()) {
			if (messure) {
				Messure ();
				messure = false;
			}
	
			//	GoThrouhPointList ();
		}

	}
	
}
