using UnityEngine;
using System.Collections.Generic;

public class CollectibleManager : MonoBehaviour
{
    public List<GameObject> cols;
    private void Awake()
    {
        cols = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            cols.Add(this.transform.GetChild(i).gameObject);
        }

        ResetCollectibles();
    }
    public void ResetCollectibles()
    {
        foreach (var c in cols)
        {
            c.SetActive(true);
        }
    }
}
