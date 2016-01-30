using UnityEngine;
using System.Collections;

public class MonoBehaviourCo : MonoBehaviour {

    /// <summary>
    /// Does the specified action until it returns false
    /// </summary>
    /// <param name="action">The action. Return false to indicate the action has completed</param>
    public void Do(System.Func<bool> action)
    {
        StartCoroutine(CoDo(action));
    }

    /// <summary>
    /// Does the specified action every frame until it returns false
    /// </summary>
    /// <param name="action">The action. Return false to indicate the action has completed</param>
    /// <returns></returns>
    protected IEnumerator CoDo(System.Func<bool> action)
    {
        while (action()) yield return null;
    }
}
