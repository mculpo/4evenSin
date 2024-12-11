using System;
using UnityEditor.Animations;
using UnityEngine;
using Random = UnityEngine.Random;

public class ComboController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ComboGroupData comboGroupData;

    public event Action<string> OnComboStepExecuted; 
    public event Action OnComboEnded; 

    private ComboData currentComboData;
    private int currentComboIndex;

    // Configurable time thresholds
    [SerializeField] private float COMBO_FINISH_TIME = 0.5f;
    [SerializeField] private float COMBO_OFFSET_TIME = 0.3f;
    private float timeAnimation = 0f;
    private float currentExecutionTime = 0f;

    private void Update()
    {
        if (currentComboData == null) return;

        VerifyComboTimeout();
        currentExecutionTime += Time.deltaTime;
    }

    public void TryExecuteLightCombo()
    {
        if (comboGroupData == null || comboGroupData.lightCombos.Count == 0) return;

        if (currentComboData == null)
        {
            StartNewCombo();
        }
        else
        {
            ContinueCombo();
        }
    }

    public bool IsComboExecuting()
    {
        return currentExecutionTime < timeAnimation;
    }

    private void StartNewCombo()
    {
        currentComboData = comboGroupData.lightCombos[Random.Range(0, comboGroupData.lightCombos.Count)];
        currentComboIndex = 0;
        currentExecutionTime = 0;
        timeAnimation = currentComboData.animationClips[currentComboIndex].length / GetCurrentAnimationSpeed(0);

        OnComboStepExecuted?.Invoke(currentComboData.animationClips[currentComboIndex].name);
        Debug.Log($"Starting combo: '{currentComboData.animationClips[currentComboIndex].name}'");
    }

    private void ContinueCombo()
    {
        if (currentExecutionTime > (timeAnimation - COMBO_OFFSET_TIME))
        {
            if (currentComboIndex < currentComboData.animationClips.Count - 1)
            {
                currentComboIndex++;
                timeAnimation = currentComboData.animationClips[currentComboIndex].length / GetCurrentAnimationSpeed(0);

                OnComboStepExecuted?.Invoke(currentComboData.animationClips[currentComboIndex].name);
                Debug.Log($"Executing combo: '{currentComboData.animationClips[currentComboIndex].name}'");
            }
            else
            {
                Debug.Log("Combo sequence completed!");
                OnComboEnded?.Invoke();
                ResetComboController();
            }

            currentExecutionTime = 0;
        }
    }
    private float GetCurrentAnimationSpeed(int layer)
    {
        float speed = 1f;
        AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;

        foreach (var layers in animatorController.layers)
        {
            foreach (var state in layers.stateMachine.states)
            {
                if (state.state.motion == currentComboData.animationClips[currentComboIndex])
                {
                    speed = state.state.speed;
                    break;
                }
            }
        }
        return speed;
    }
    private void VerifyComboTimeout()
    {
        if (currentExecutionTime > (timeAnimation + COMBO_FINISH_TIME))
        {
            Debug.Log("Combo timed out. Resetting controller.");
            OnComboEnded?.Invoke();
            ResetComboController();
        }
    }

    private void ResetComboController()
    {
        currentComboData = null;
        currentComboIndex = 0;
        currentExecutionTime = 0;
        timeAnimation = 0;
    }
}
