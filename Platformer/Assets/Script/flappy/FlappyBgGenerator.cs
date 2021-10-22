using UnityEngine;
using System.Collections.Generic;

public class FlappyBgGenerator : Ticker
{
    public List<FlappyBg> list = new List<FlappyBg>();
    public FlappyBg prefab;
    public static FlappyBgGenerator instance;
    public float bgZ;
    public int xCountHalf = 6;
    public int yCountHalf = 3;
    public int dist = 3;

    private void Start()
    {
        instance = this;
    }

    protected override void Tick()
    {
        var posX = Mathf.FloorToInt(FlappyChar.instance.transform.position.x);
        var posY = Mathf.FloorToInt(FlappyChar.instance.transform.position.y);
        posX -= (posX % dist);
        posY -= (posY % dist);

        foreach (var bg in list)
        {
            bg.unChecked = true;
        }

        for (int x = 0; x < xCountHalf * 2; x++)
        {
            int pX = posX + x * dist - xCountHalf * dist;
            for (int y = 0; y < yCountHalf * 2; y++)
            {
                int pY = posY + y * dist - yCountHalf * dist;
                var has = false;
                foreach (var bg in list)
                {
                    if (bg.x == pX && bg.y == pY)
                    {
                        has = true;
                        bg.unChecked = false;
                        break;
                    }
                }

                if (!has)
                {
                    var newBg = Instantiate(prefab);
                    newBg.transform.SetParent(prefab.transform.parent);
                    newBg.transform.position = new Vector3(pX, pY, bgZ);
                    newBg.x = pX;
                    newBg.y = pY;
                    newBg.unChecked = false;
                    newBg.gameObject.SetActive(true);
                    list.Add(newBg);
                }
            }
        }

        for (var i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].unChecked)
            {
                Destroy(list[i].gameObject);
                list.Remove(list[i]);
            }
        }
    }
}
