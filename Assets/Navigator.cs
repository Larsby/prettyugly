using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
	public GameObject levelClear;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
<<<<<<< HEAD
    }

    public void pausepause()
    {
        Time.timeScale = 0;
=======

		if (levelClear)
			Invoke("CheckLevelFinished", 1f);
>>>>>>> af2e45cedf8a769e3490f7793330a0c132fa6ffe
    }


    public void gogogo()
    {
        Time.timeScale = 1;
    }


    public void Next()
    {
        if (GameManager.instance)
        {
            GameManager.instance.LoadNextLevel();
        }
    }
    public void Prev()
    {
        if (GameManager.instance)
        {
            GameManager.instance.LoadPrevLevel();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

	void CheckLevelFinished()
	{
		if (FindObjectsOfType<SlimeStretchChecker2>().Length == 0)
		{
			levelClear.SetActive(true);
		}
		else
		{
			Invoke("CheckLevelFinished", 1f);
		}
	}

}
