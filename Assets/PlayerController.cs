using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance = null;
    public GameObject startPoint;
    public float outerRadius = 0.2f;
    public float innerRadius = 0.1f;
    public bool immediate = false;
    private bool stretch = false;
    private Vector2 tapPosition;
    private Vector3 savePosition;
    float distanceFromTap;
    float prevDistanceFromTap;
    public float mass = 2f;
    public bool isAiming = false;
    GameObject pusherPosition = null;
    Collider2D collider = null;
    CountMoves moves = null;
    bool reverse = false;

    public bool connectionWithMouseClick = false;
    public static bool connectWithMouseClick = false;


    void Awake()
    {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad (gameObject);

        connectWithMouseClick = connectionWithMouseClick;
    }
    void SetRadius(float currentDistance, float previousDistance)
    {

        //	Debug.Log ("current " + currentDistance + " prev " + previousDistance + " res " + (currentDistance - previousDistance));
        if (currentDistance - previousDistance > 0)
        {
            if (innerRadius > 0.2f)
            {
                innerRadius = innerRadius - 0.03f;
            }
        }
        else if (currentDistance - previousDistance < 0)
        {

            if (innerRadius <= 1.03f)
            {
                innerRadius = innerRadius + 0.03f;
            }
        }

    }
    Vector2 getPosition()
    {

        Vector2 curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        return curPosition;
    }

    Vector2 getPosition2()
    {
        Vector3 gameObjectSreenPoint = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
        //Sets the mouse pointers vector3
        Vector3 mousePreviousLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z);

        return new Vector2(gameObjectSreenPoint.x, gameObjectSreenPoint.y);
    }
    void Start()
    {
        pusherPosition = (GameObject)Instantiate(Resources.Load("PusherPoint"));
        pusherPosition.transform.position = transform.parent.transform.position;
        collider = gameObject.GetComponent<Collider2D>();
        stretch = true;
        immediate = true;
        moves = GetComponent<CountMoves>();
        bool move = false;
        if (moves != null)
        {
            move = true;
        }
        if (GameManager.instance != null)
            GameManager.instance.UseMoves(move);

    }
    void OnMouseUp()
    {
#if UNITY_IOS
		// allow unity player to work in iphone mode without using a remote but real ios wont do doulbe calls.
		if (Input.touchCount > 0)
		return;
#endif
        Vector2 currentPosition = getPosition();



        OnUp(currentPosition);
    }
    public bool isStretchMode()
    {
        return stretch;
    }
    public Vector3 getTargetPosition()
    {
        return new Vector3(tapPosition.x, tapPosition.y, 0.0f);
    }
    public void ResetArms()
    {

        OnDown(transform.position);
        OnUp(transform.position);
    }
    void OnMouseDown()
    {
        print("  mousedown");
#if UNITY_IOS
		if (Input.touchCount > 0)
		return;
#endif
        Vector2 currentPosition = getPosition();

        OnDown(currentPosition);
    }
    void OnDown(Vector2 position)
    {
        stretch = true;
        //		started = true;
        tapPosition = position;
        savePosition = tapPosition;
        debug = 0;
        if (pusherPosition)
        {
            var col = pusherPosition.GetComponent<Collider2D>();
            if (col)
            {
                col.enabled = false;
            }
        }
        if (collider)
            collider.isTrigger = false;


        //	tap = GetComponent<Renderer> ().bounds.center;
        if (pusherPosition)
        {
            pusherPosition.transform.position = transform.position;
        }
    }
    public bool IsTrigger()
    {
        return collider.isTrigger;
    }
    void AimOver()
    {
        isAiming = false;
    }
    void OnUp(Vector2 inputpos)
    {
        stretch = false;
        immediate = false;
        if (moves != null)
        {
            GameManager.instance.PusherMovesLeft(moves.GetMoves());
        }
        if (moves != null && moves.CountDown() == false)
        {
            GameManager.instance.PusherOut();

        }
        //	isAiming = false;
        //	stretch = false;
        if (pusherPosition)
        {
            var col = pusherPosition.GetComponent<Collider2D>();
            if (col)
            {
                col.enabled = true;
            }
        }
        int i = 0;
        //gameObject.GetComponent<PlacePusherLegs> ().Place ();
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        Vector3 position = transform.position;
        //Debug.Log("fg" + tapPosition.x + "" + tapPosition.y);
        float distance = Vector2.Distance(transform.position, tapPosition);




        float forceFactor = distance * 85;
        Vector2 velocity;
        if (reverse)
        {
            velocity = new Vector2(((transform.position.x * 1.0f) - tapPosition.x) * 1, ((transform.position.y * 1.0f) - tapPosition.y) * 1);
        }
        else
        {
            velocity = new Vector2(((tapPosition.x * 1.0f) - transform.position.x) * 1, ((tapPosition.y * 1.0f) - transform.position.y) * 1);
        }
        body.velocity = velocity;
        Vector2 forceVec = body.velocity.normalized * forceFactor;
        //Debug.Log ("!!Force" + forceVec.x + " " + forceVec.y+ "  b "+body.velocity.normalized);
        body.AddForce(forceVec, ForceMode2D.Impulse);
        //Debug.Log ("vec" + forceVec.x + " " + forceVec.y);
        body.mass = mass;
        Invoke("AimOver", 0.05f);


        //outerRadius = 1.0f;

    }

    static int debug = 0;
    public bool IsAiming()
    {

        return isAiming;
    }
    void OnDrag(Vector2 inputPosition)
    {
        immediate = true;
        isAiming = true;
        Vector3 curPosition = new Vector3(inputPosition.x, inputPosition.y, 0.0f);
        transform.position = curPosition;
        isAiming = true;


        float distance = Vector2.Distance(tapPosition, curPosition) * 100;

        prevDistanceFromTap = distanceFromTap;
        distanceFromTap = Vector2.Distance(tapPosition, curPosition);
        SetRadius(distanceFromTap, prevDistanceFromTap);


        if (distance > 300.0f)
        {
            float ex = 300.0f / distance;

            curPosition.x = tapPosition.x - ((tapPosition.x - curPosition.x) * ex);
            curPosition.y = tapPosition.y - ((tapPosition.y - curPosition.y) * ex);

        }
        savePosition = curPosition;

        // here we need code to limit the pusher so it cant go of screen

        transform.position = curPosition;


    }


    void OnMouseDrag()
    {
#if UNITY_IOS
		if (Input.touchCount > 0)
		return;
#endif


        Vector2 currentPosition = getPosition();


        OnDrag(currentPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (connectWithMouseClick)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
        else if (Input.GetMouseButton(0))
        {

            OnMouseDrag();

        }


        if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp();
        }

    }

}
