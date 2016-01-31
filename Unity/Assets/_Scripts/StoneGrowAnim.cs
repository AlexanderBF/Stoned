using UnityEngine;
using System.Collections;

public interface IGrowAndShrinkAnimation
{
    void Grow(SpiritBase.SpiritType type);
    void Shrink();
}

public class StoneGrowAnim : MonoBehaviourCo, IGrowAndShrinkAnimation
{
    public SpriteRenderer sprite;
    public float duration = 0.7f;

    public Sprite fire;
    public Sprite earth;
    public Sprite air;
    public Sprite water;

    public Sprite normal;

    public Vector3 scale;
    void Awake()
    {
        Transform t = transform;
        scale = t.localScale;
    }

    void AnimateShink()
    {
        if (normal != null)
            sprite.sprite = normal;
        else
        {
            Transform t = transform;


            float startTime = Time.time;
            Color clear = Color.white;
            clear.a = 0.0f;
            Do(() =>
            {
                if (Time.time - startTime < duration)
                {
                    // t.localScale = Vector3.Lerp(Vector3.zero, scale, (Time.time - startTime) / duration);
                    sprite.color = Color.Lerp(Color.white, clear, (Time.time - startTime) / duration);
                    return true;
                }

                t.localScale = scale;
                return false;
            });
        }
    }

    void AnimatedGrow()
    {
        Transform t = transform;
        

        float startTime = Time.time;
        Color clear = Color.white;
        clear.a = 0.0f;
        Do(() =>
        {
            if(Time.time- startTime < duration)
            {
                t.localScale = Vector3.Lerp(Vector3.zero, scale, (Time.time - startTime) / duration);
                sprite.color = Color.Lerp(clear, Color.white, (Time.time - startTime) / duration);
                return true;
            }

            t.localScale = scale;
            return false;
        });
    }

    public void Grow(SpiritBase.SpiritType type)
    {
        Sprite sprite = null;
        switch(type)
        {
            case SpiritBase.SpiritType.Fire:
                sprite = fire;
                break;
            case SpiritBase.SpiritType.Air:
                sprite = air;
                break;
            case SpiritBase.SpiritType.Water:
                sprite = water;
                break;
            case SpiritBase.SpiritType.Earth:
                sprite = earth;
                break;
            default:
                sprite = fire;
                break;
        }

        this.sprite.sprite = sprite;

        AnimatedGrow();
    }

    public void Shrink()
    {
        AnimateShink();
    }
}
