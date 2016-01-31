using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoneGrower : MonoBehaviourCo {

    [System.NonSerialized]
    public StoneGrower next;

    [System.NonSerialized]
    public StoneGrower prev;

    public GameObject colliderAdd0;
    public GameObject colliderAdd1;

    void Start()
    {
        if(Random.value < 0.5f)
        {
            GameObject go = colliderAdd0;
            colliderAdd0 = colliderAdd1;
            colliderAdd1 = go;
        }
    }

    public static void Clean()
    {
        foreach(StoneGrower grower in allGrowers)
        {
            grower.DoClean();
        }
    }
    public void DoClean()
    {
        colliderAdd0.SetActive(false);
        colliderAdd1.SetActive(false);

        IGrowAndShrinkAnimation[] children = GetComponentsInChildren<IGrowAndShrinkAnimation>();
        foreach (IGrowAndShrinkAnimation anim in children)
        {
            anim.Shrink();
        }
    }

    private static List<StoneGrower> allGrowers = new List<StoneGrower>();
    void OnEnable()
    {
        StoneGrower.allGrowers.Add(this);
    }

    void OnDisable()
    {
        StoneGrower.allGrowers.Remove(this);
    }

    public bool Grow(SpiritBase.SpiritType type)
    {
        if (colliderAdd0.activeSelf)
        {
            if (colliderAdd1.activeSelf)
            {

                // Already grown, grow next or prev
                StoneGrower canGrow = NearestCanGrow();

                if (canGrow)
                {
                    canGrow.Grow(type);
                    return true;
                }
                return false;
            }

            colliderAdd1.SetActive(true);
        }
        else
        {
            colliderAdd0.SetActive(true);
            colliderAdd1.SetActive(true);

            IGrowAndShrinkAnimation[] children = GetComponentsInChildren<IGrowAndShrinkAnimation>();
            foreach(IGrowAndShrinkAnimation anim in children)
            {
                anim.Grow(type);
            }
        }

        return true;
    }

    public bool CanGrow()
    {
        return (!colliderAdd0.activeSelf || !colliderAdd1.activeSelf);
    }
    public StoneGrower NearestCanGrow()
    {
        // if(CanGrow())
        //     return this;
        // 
        // if (next.CanGrow())
        //     return next;
        // 
        // StoneGrower p = prev;
        // 
        // while(!p.CanGrow())
        // {
        //     p = p.next;
        //     if (p == this)
        //         return null;
        // }
        // 
        // return p;
        List<StoneGrower> list = new List<StoneGrower>();
        StoneGrower[] all = transform.parent.GetComponentsInChildren<StoneGrower>();

        foreach(StoneGrower sg in all)
        {
            if (sg.CanGrow())
                list.Add(sg);
        }

        Vector3 position = transform.position;
        list.Sort((a,b) =>
        {
            float m0 = (position - a.transform.position).magnitude;
            float m1 = (position - b.transform.position).magnitude;

            if (m0 < m1) return -1;
            return 1;
        });

        if (list.Count > 0)
            return list[0];

        return null;
    }
}
