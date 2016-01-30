using UnityEngine;
using System.Collections;

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

    public bool Grow()
    {
        if (colliderAdd0.activeSelf)
        {
            if (colliderAdd1.activeSelf)
            {
                // Already grown, grow next or prev
                StoneGrower canGrow = NearestCanGrow();

                if(canGrow)
                {
                    canGrow.Grow();
                    return true;
                }
                return false;
            }

            colliderAdd1.SetActive(true);
        }
        else colliderAdd0.SetActive(true);

        return true;
    }

    public bool CanGrow()
    {
        return (!colliderAdd0.activeSelf || !colliderAdd1);
    }
    public StoneGrower NearestCanGrow()
    {
        if(CanGrow())
            return this;

        if (next.CanGrow())
            return next;

        StoneGrower p = prev;

        while(!p.CanGrow())
        {
            p = p.next;
            if (p == this)
                return null;
        }

        return p;
    }
}
