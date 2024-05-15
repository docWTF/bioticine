using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public string currentStateName;

    public Animator animator;
    private Dictionary<string, float> animationSpeeds;
    private Dictionary<string , float> animationRealTime;

    void Awake()
    {
        animationSpeeds = new Dictionary<string, float>
        {
            { "PlayerWalk", 1.0f },
            { "PlayerIdle", 1.0f },
            { "PlayerStun", 1f },
            { "PlayerDead", 1f },
            { "PlayerAttack(0)", 0.4f },
            { "PlayerAttack(1)", 0.4f },
            { "PlayerAttack(2)", 0.4f },
            { "PlayerAttack(3)", 0.4f },

        };

        animationRealTime = new Dictionary<string, float>
        {
            { "PlayerWalk", 0.4f },
            { "PlayerIdle", 0.45f },
            { "PlayerStun", 0.19f },
            { "PlayerDead", 0.5f },
            { "PlayerAttack(0)", 0.35f },
            { "PlayerAttack(1)", 0.4f },
            { "PlayerAttack(2)", 0.3f },
            { "PlayerAttack(3)", 0.35f },
        };

        animator = GetComponent<Animator>();
        animator.speed = 1.0f;
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 0 for the base layer
        currentStateName = GetStateName(stateInfo.shortNameHash);
    }

    public float GetAnimationStateSpeed(string stateName)
    {
        if (animationSpeeds.ContainsKey(stateName))
        {
            return animator.speed * animationSpeeds[stateName];
        }
        else
        {
            Debug.LogWarning("Animation state not found: " + stateName);
            return animator.speed;
        }
    }

    public float GetAnimationRealTime(string stateName)
    {
        if (animationRealTime.ContainsKey(stateName))
        {
            return animationRealTime[stateName];
        }
        else
        {
            Debug.LogWarning("Animation state not found: " + stateName);
            return animator.speed;
        }
    }

    string GetStateName(int hash)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (Animator.StringToHash(clip.name) == hash)
                return clip.name;
        }
        return "Unknown State";
    }
}
