using UnityEngine;
using System.Collections;

/// <summary>
/// Simple abstraction for getting the layers (in case of remodelling physics colliders etc)
/// </summary>
public static class LayerManager
{
    public static LayerMask GoalCenter
    {
        get { return LayerMask.NameToLayer("GoalCenter"); }
    }
    public static LayerMask StoneWallOuter
    {
        get { return LayerMask.NameToLayer("StoneWallOuter"); }
    }
    public static LayerMask StoneWallInner
    {
        get { return LayerMask.NameToLayer("StoneWallInner"); }
    }
    public static LayerMask Spirit
    {
        get { return LayerMask.NameToLayer("Spirit"); }
    }
    public static LayerMask OuterRingTrigger
    {
        get { return LayerMask.NameToLayer("SafeFromOuterRing"); }
    }
    public static LayerMask InnerRingTrigger
    {
        get { return LayerMask.NameToLayer("SafeFromInnerRing"); }
    }
}

/// <summary>
/// Base class for flying spirits. Probably usefull to have some common behaviour
/// </summary>
public class SpiritBase : MonoBehaviourCo {

    [Tooltip("The speed the spirit moves forward in")]
    public float speed = 1.0f;

    [HideInInspector]
    public bool isSafeFromOuterRing = false;

    [HideInInspector]
    public bool isSafeFromInnerRing = false;

    [Tooltip("Prefab: The particle effect for regular motion")]
    public GameObject engine;

    [Tooltip("Prefab: The particle effect to trigger when hitting a stone")]
    public GameObject impactStone;

    [Tooltip("Prefab: The particle effect to trigger when impacting the center/goal")]
    public GameObject impactCenter;

    private HomeBase center;
    
    private float spawnTime = 0.0f;
    void Start()
    {
        spawnTime = Time.time;
        this.center = FindObjectOfType<HomeBase>();
        Debug.Log(name + " => " + this.center.name);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        StartEngine();
    }

    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce((center.transform.position - transform.position).normalized * center.gravity * Mathf.Lerp(0, 1, (Time.time - spawnTime)/20));

        Vector3 velocity = rb.velocity;
        velocity = velocity.normalized * speed;
        if (float.IsNaN(velocity.x))
            velocity = transform.forward * speed;

        transform.LookAt(transform.position + velocity);

        rb.velocity = velocity;
    }

    /// <summary>
    /// Creates the particle system and returns the game object
    /// </summary>
    /// <param name="particleSystem">The particle sysstem.</param>
    /// <returns></returns>
    protected GameObject CreateParticleSystem(GameObject particleSystem, Vector3 location, bool parentToMe = true)
    {
        if(!particleSystem)
        {
            Debug.LogWarning("You are missing a particle emitter on " + name, this);
            return null;
        }
        GameObject go = Instantiate(particleSystem, location, transform.rotation) as GameObject;

        if(parentToMe)
            go.transform.parent = transform;

        return go;
    }

    /// <summary>
    /// Call this when the spirit starts the engine
    /// </summary>
    virtual public void StartEngine()
    {
        CreateParticleSystem(engine, transform.position, true);
    }

    /// <summary>
    /// Call this when the spirit impacts a stone
    /// </summary>
    virtual public void ImpactStone(Vector3 impactLocation)
    {
        CreateParticleSystem(impactStone, impactLocation, false);

        float impactTime = Time.time;
        float originalSpeed = speed;
        speed = 0.0f;

        Do(() =>
        {
            if(Time.time - impactTime < 1.5f)
            {
                return true;
            }

            speed = originalSpeed;
            return false;
        });
    }

    /// <summary>
    /// Call this when the spirit impacts the center / goal
    /// </summary>
    virtual public void ImpactCenter()
    {
        CreateParticleSystem(impactCenter, transform.position);
    }

    public void OnCollisionEnter(Collision collision)
    {
        int layer = collision.gameObject.layer;

        if(layer == LayerManager.GoalCenter)
        {
            ImpactCenter();
        }
        else if(layer == LayerManager.StoneWallInner || layer == LayerManager.StoneWallOuter)
        {
            ImpactStone(collision.contacts[0].point);
        }
        else if(layer == LayerManager.Spirit)
        {
            // do nothing, no interaction between spirits as of now
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        int layer = other.gameObject.layer;

        if (layer == LayerManager.OuterRingTrigger)
            isSafeFromOuterRing = true;
        else if (layer == LayerManager.InnerRingTrigger)
            isSafeFromInnerRing = true;
    }
}
