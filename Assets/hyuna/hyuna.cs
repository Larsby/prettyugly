using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hyuna : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject slime;
    public GameObject ball1;
    public GameObject ball2;

    public Color slimeBallAColor;
    public Color slimeBallBColor;

    public float startX = 8;
    public float startY = 10;

    public float increaseX = 1;
    public float increaseY = -1;


    int[,] Levela = new int[,] { { 1, 1, 1, 1, 2, 2, 2 },
                                 { 0, 1, 1, 2, 2, 2, 0 },
                                 { 0, 1, 2, 2, 2, 2, 0 } };
    int count = 1;
    GameObject[,] ballsList;
    public bool enableSlime = false;
    public Material slimeMaterial;
    public GameObject lineRendererTemplate;
    public bool enableJoints = true;
    public bool enableFromJoints = true;
    public bool enableToJoints = false;
	public bool realisticRope = false;
    void AddSlime(GameObject from, GameObject to)
    {

        if (from == null)
            return;

        if (to == null)
            return;


        GameObject r = Instantiate(lineRendererTemplate.gameObject);
        LineRenderer lineRenderer = r.GetComponent<LineRenderer>();
        lineRenderer.material = slimeMaterial;
		if (realisticRope == false)
		{
			AttachLineRenderer ar = r.AddComponent<AttachLineRenderer>();
			ar.from = from.transform;
			ar.to = to.transform;
		} else {
			RopeControllerSimple ropeController = r.AddComponent<RopeControllerSimple>();
			ropeController.whatIsHangingFromTheRope = from.transform;
			ropeController.whatTheRopeIsConnectedTo = to.transform;
			Rigidbody2D rl = from.GetComponent<Rigidbody2D>();
			Rigidbody2D rr = to.GetComponent<Rigidbody2D>();
			rl.velocity = rr.velocity = Vector3.zero;
		//	ropeController.isPlayer = false;
		//	ropeController.gravityForce = Vector3.zero;
		//	ropeController.useCollider = false;
		}

        if (enableJoints)
        {
            if (enableFromJoints)
            {
                SpringJoint2D springFrom = from.AddComponent<SpringJoint2D>();
                springFrom.connectedBody = to.GetComponent<Rigidbody2D>();
                springFrom.distance = 2.5f;
            }
            if (enableToJoints)
            {
                SpringJoint2D springTo = to.AddComponent<SpringJoint2D>();
                springTo.connectedBody = from.GetComponent<Rigidbody2D>();
                springTo.distance = 2.5f;
            }

        }
    }
    void CalculateAndAddSlime()
    {
        int MAX_X = 7;
        int MAX_Y = Levela.Rank;

        for (int y = 0; y <= MAX_Y; y++)
        {


            for (int x = 0; x < 7; x++)
            {

                if (y + 1 <= MAX_Y)
                {
                    GameObject o1 = ballsList[y + 1, x];
                    AddSlime(ballsList[y, x], o1);
                    if (x + 1 < MAX_X)
                    {

                        GameObject oo = ballsList[y + 1, x + 1];
                        AddSlime(ballsList[y, x], oo);
                    }
                    if (x >= 0)
                    {
                        GameObject oo = ballsList[y + 1, x];
                        AddSlime(ballsList[y, x], oo);
                    }

                }
                if (x == MAX_X - 1 && y + 1 <= MAX_Y)
                {
                    GameObject oo = ballsList[y + 1, x];
                    AddSlime(ballsList[y, x], oo);
                }
                if (x < MAX_X - 1)
                {
                    GameObject oo = ballsList[y, x + 1];
                    AddSlime(ballsList[y, x], oo);
                }
                if (y == MAX_Y - 1 && x + 1 < MAX_X)
                {
                    //      GameObject oo = ballsList[y - 1, x + 1];
                    //      AddSlime(ballsList[y, x], oo);
                }
                if (y == MAX_Y - 1)
                {
                    GameObject oo = ballsList[y - 1, x];
                    AddSlime(ballsList[y, x], oo);
                }
                //prevX = ballsList[y, x] = temp;

                if (y - 1 > 0 && x + 1 < MAX_X)
                {
                    //   GameObject oo = ballsList[y - 1, x + 1];
                    //  AddSlime(oo, ballsList[y, x]);
                }


            }

        }

    }
    void Start()
    {
        ballsList = new GameObject[Levela.Rank + 1, 7];
        GameObject prevX = null;
        //		GameObject prevY;
        int count = 0;
        for (int y = 0; y <= Levela.Rank; y++)
        {
            bool add = true;

            for (int x = 0; x < 7; x++)
            {
                float shift = 0;

                if (y == 0)
                    shift = .5f;
                if (y == 2)
                    shift = .5f;

                GameObject objTemplate = ball1;
                // += Levela[x, y] + ",";


                if (Levela[y, x] == 2)
                {
                    objTemplate = ball2;
                }

                if (Levela[y, x] != 0)
                {
                    GameObject temp = Instantiate(objTemplate);
                    temp.name = "Object " + count++;



                    //	temp.AddComponent<MyDebug>().index = count++;
                    if (x == 6) add = false;
                    if (enableSlime && prevX && add)
                    {

                        //	AddSlime(temp, prevX);


                    }

                    prevX = ballsList[y, x] = temp;

                    ballsList[y, x].transform.position = new Vector3(startX + (x * increaseX) + shift, startY + (y * increaseY), 1);

                }
            }

        }


        //        ballsList[2, 2].GetComponent<connector>().connect(ballsList[2, 1], ballsList[2, 3], ballsList[3, 2], ballsList[3, 3]);
        if (enableSlime)
            CalculateAndAddSlime();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
