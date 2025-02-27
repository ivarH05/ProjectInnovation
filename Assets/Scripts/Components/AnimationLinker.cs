using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimationLinker : MonoBehaviour
{
    public Transform simulated;
    public Transform animated;

    // Start is called before the first frame update
    void Start()
    {
        AddTrackersRecursively(simulated, animated);
    }

    void AddTrackersRecursively(Transform sim, Transform anim)
    {
        int childCount = sim.childCount;
        for (int i = 0; i < childCount; i++)
        {
            AddTrackersRecursively(sim.GetChild(i), anim.GetChild(i));
        }

        if (sim.GetComponent<Rigidbody>() != null)
            return;
        AnimationTracker t = sim.AddComponent<AnimationTracker>();
        t.target = anim;
    }
}
