using System.Collections;
using UnityEngine;

public class Duplicator : MonoBehaviour
{
	public GameObject prefab;

	public int xNum = 10, yNum = 5;
	public float xDist = 2, yDist = -2;
	public float xStart = 1.3f, yStart = 9.71f;

	public float timeBetweenCreates = 0f;

    void Start()
    {
		StartCoroutine(Distribute());
		Debug.Log("Starting duplicatior distrubution");
	}

	public IEnumerator Distribute()
	{
		for (int i = 0; i < yNum; i++)
			for (int j = 0; j < xNum; j++)
			{
				Instantiate(prefab, new Vector3(xStart + xDist * j, yStart + yDist * i, 1), Quaternion.identity);
				if (timeBetweenCreates > 0)
					yield return new WaitForSeconds(timeBetweenCreates);
			}
	}

}
