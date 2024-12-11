using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Dictionary<string, int> animationHashes;

    [Tooltip("List of available animation names")]
    public List<string> animationNames;

    private int currentHash;

    void Awake()
    {
        InitializeAnimationHashes();
    }

    private void InitializeAnimationHashes()
    {
        animationHashes = new Dictionary<string, int>();

        foreach (var animationName in animationNames)
        {
            int hash = Animator.StringToHash(animationName);
            animationHashes[animationName] = hash;
        }
    }

    public void PlayAnimation(string animationName = "", float transitionDuration = 0.25f, float timeToWait = 0, int layer = 0)
    {

        if (timeToWait > 0) StartCoroutine(Wait());
        else Validate();

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(timeToWait - transitionDuration);
            Validate();
        }

        void Validate()
        {
            if(animationName == "") return;

            if (!animationHashes.TryGetValue(animationName, out int hash))
            {
                Debug.LogWarning($"The animation '{animationName}' was not found. Ensure it is in the animationNames list.");
                return;
            }

            if (currentHash != hash)
            {
                currentHash = hash;
                animator.CrossFade(hash, transitionDuration, layer);
            }
        }
    }

    public bool IsPlaying(string animationName)
    {
        if (!animationHashes.TryGetValue(animationName, out int hash))
        {
            Debug.LogWarning($"The animation '{animationName}' was not found.");
            return false;
        }

        var currentState = animator.GetCurrentAnimatorStateInfo(0);
        return currentState.fullPathHash == hash;
    }

    public bool IsPlaying(int hash)
    {
        var currentState = animator.GetCurrentAnimatorStateInfo(0);
        return currentState.fullPathHash == hash;
    }
}
