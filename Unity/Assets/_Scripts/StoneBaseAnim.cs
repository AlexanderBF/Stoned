using UnityEngine;
using System.Collections;

public class StoneBaseAnim : MonoBehaviourCo, IGrowAndShrinkAnimation
{
    public float wait = 0.3f;
    public void Grow(SpiritBase.SpiritType type)
    {
        float startTime = Time.time;
        Do(() =>
        {
            if (Time.time - startTime < wait)
                return true;

            GetComponent<SpriteRenderer>().color = Color.clear;
            return false;
        });
    }
	
    public void Shrink()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
