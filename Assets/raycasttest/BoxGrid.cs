using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BoxGrid : MonoBehaviour {
    public GameObject GridPrefab;
    public Vector2 GridSize;
    public float Spacing = 0.15f;
    public List<GameObject> Items;

    [ContextMenu("Clear Grid")]
    void ClearGrid()
    {
        Items.RemoveAll(x => x == null);
        Items.ForEach(x => DestroyImmediate(x));

    }
    [ContextMenu("Create Grid")]
	void CreateGrid()
    {
        GridPrefab.SetActive(false);
        Items.RemoveAll(x => x == null);
        Items.ForEach(x => x.SetActive(false));
        for(int ix = 0; ix < GridSize.x; ix++)
        {
            for(int iy = 0; iy < GridSize.y; iy++)
            {
                var gridSprite = GetFromPool();
                gridSprite.transform.localPosition = new Vector3(ix, iy, 0) * Spacing; 
            }
        }
    }
    GameObject GetFromPool()
    {
        var item = Items.FirstOrDefault(x => x.activeSelf == false);
        if(item == null)
        {
            item = GameObject.Instantiate(GridPrefab);
            item.transform.SetParent(this.transform);
            Items.Add(item);
        }
        item.SetActive(true);
        return item;
    }
}
