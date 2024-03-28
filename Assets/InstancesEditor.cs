using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstancesEditor : MonoBehaviour
{

	public int MaxChildren = 50;
	private int _oldVal;

    // Start is called before the first frame update
    void Start()
    {
		_oldVal = MaxChildren;
		CreateUniqueBaby._MaxCount = MaxChildren;
    }

    // Update is called once per frame
    void Update()
    {
		if (_oldVal != MaxChildren)
		{
			CreateUniqueBaby._MaxCount = MaxChildren;
			_oldVal = MaxChildren;
		}
        
    }
}
