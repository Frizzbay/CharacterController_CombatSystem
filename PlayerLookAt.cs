using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
    [Range(0f, 1f)]
    public float weight;

    [Range(0f, 1f)]
    public float bodyWeight;

    [Range(0f, 1f)]
    public float headWeight;

    [Range(0f, 1f)]
    public float eyesWeight;

    [Range(0f, 1f)]
    public float clampWeight;

    Animator _animator; 
    Camera _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _mainCamera = Camera.main;  
    }


    private void OnAnimatorIK(int layerIndex)
    {   _animator.SetLookAtWeight(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
        Ray lookAtRay = new Ray(transform.position, _mainCamera.transform.forward);
        _animator.SetLookAtPosition(lookAtRay.GetPoint(25));
    }
        
}
