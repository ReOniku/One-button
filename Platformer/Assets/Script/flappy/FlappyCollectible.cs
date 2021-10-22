using UnityEngine;
using System.Collections;

public class FlappyCollectible : MonoBehaviour
{
    public enum Category
    {
        SpeedMultiple,//value is SpeedMultiple ratio 2
        SpeedUp,//value is SpeedUp amount 1
        SpeedDown,//value is SpeedDown amount 1
        EnergyBonus,//value is EnergyBonus amount 2,5
        BreakableWall,//value is Speed reduce amount
        Portal,//target is out place
        Push,//value is push up speed amount 5
    }
    public Category category;

    public enum VFX
    {
        None,
        Normal,
        Big,
        Bad,
    }

    public VFX vfx;

    public int value;
    public Transform target;


    public void WallBreak()
    {
        //var wall = target.GetComponent<>();
    }

    public void OnCollected()
    {
        Destroy(gameObject);
    }
}
