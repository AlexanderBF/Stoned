using UnityEngine;
using System.Collections;

public class CleanParticleController : MonoBehaviour {

    public ParticleSystem fire;
    public ParticleSystem air;
    public ParticleSystem water;
    public ParticleSystem earth;

    public static CleanParticleController me;

    void Start()
    {
        CleanParticleController.me = this;
    }

    public static void Clean(SpiritBase.SpiritType type)
    {
        switch(type)
        {
            case SpiritBase.SpiritType.Fire:
                me.fire.Play();
                break;
            case SpiritBase.SpiritType.Air:
                me.air.Play();
                break;
            case SpiritBase.SpiritType.Water:
                me.water.Play();
                break;
            case SpiritBase.SpiritType.Earth:
                me.earth.Play();
                break;
        }
    }
}
