using UnityEngine;
using System.Collections;

public class GameOverForPusher : MonoBehaviour
{
    private bool toldGameManager = false;
    private Vector3 startPos;
    public int numberOfLifes = 10;
    // Use this for initialization
    void Start()
    {
        toldGameManager = false;
        startPos = transform.position;
        if (GameManager.instance != null)
            GameManager.instance.PusherLostLife(numberOfLifes);
    }

    // Update is called once per frame
    void Update()
    {
        return;
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (toldGameManager == false)
        {
            if ((viewPos.x < 0.0f || viewPos.x > 1.0f) || (viewPos.y < 0.0f || viewPos.y > 1.0f))
            {
                if (numberOfLifes-- <= 1)
                {
                    GameManager.instance.PusherOut();
                    toldGameManager = true;
                    GetComponent<Rigidbody2D>().isKinematic = true;
                    transform.position = new Vector2(-22.0f, -15f);

                }
                else
                {
                    GameManager.instance.PusherLostLife(numberOfLifes);
                    transform.position = startPos;
                    transform.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
                    //transform.gameObject.GetComponent<Collider2D> ().isTrigger = false;
                }


                //GetComponent<Rigidbody2D> ().rotation = 0;
                //gameObject.SetActive (false);
            }
        }
    }
}
