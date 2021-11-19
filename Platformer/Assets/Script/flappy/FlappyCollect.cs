using UnityEngine;
using System.Collections;

public class FlappyCollect : MonoBehaviour
{
    public FlappyChar flappyChar;
    public ParticleSystem psCollectNormal;
    public ParticleSystem psCollectBig;
    public ParticleSystem psCollectBad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="killzone")
        {
            GameSystem.instance.OnDie();
            return;
        }

        var flappyCollectible = other.GetComponent<FlappyCollectible>();
        if (flappyCollectible != null)
        {
            flappyChar.OnCollect(flappyCollectible);
            flappyCollectible.OnCollected();
            switch (flappyCollectible.vfx)
            {
                case FlappyCollectible.VFX.Normal:
                    psCollectNormal.Play(true);
                    break;
                case FlappyCollectible.VFX.Big:
                    psCollectBig.Play(true);
                    break;
                case FlappyCollectible.VFX.Bad:
                    psCollectBad.Play(true);
                    break;
            }
        }
    }
}
