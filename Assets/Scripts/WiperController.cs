﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controls the wiper arm
public class WiperController : ArmController
{
    [SerializeField] private float wipeRange; // maximum distance to successfully wipe
    [SerializeField] private float passiveReachRatio; // percentage to reach towards nearest target


    protected  override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (ClosestRelativeToArm().magnitude <= targetRange && !animating)
        {
            PassiveWipe();
        }
    }

    // constantly point at nearest target
    private void PassiveWipe()
    {
        Vector3 closest = ClosestRelativeToArm();
        transform.LookAt(closest + transform.position);
        if (closest.magnitude <= maxArmLength / passiveReachRatio)
        {
            StretchArm(closest.magnitude * passiveReachRatio);
        }
        else
        {
            StretchArm(maxArmLength);
        }
    }

    // begin wiping animation
    public void AnimateWipe()
    {
        if (animating)
        { // animation cancel
            StopCoroutine(coroutine);
        }

        // aim arm at target
        Vector3 closest = ClosestRelativeToArm();
        transform.LookAt(closest + transform.position);
        if (closest.magnitude <= maxArmLength)
        {
            StretchArm(closest.magnitude);
        }
        else
        {
            StretchArm(maxArmLength);
        }

        // perform wipe
        if (closest.magnitude <= wipeRange)
        {
            FloorManager.currentFloor.smudgeManager.WipeSmudge();
        }
        coroutine = FinishWipe();
        StartCoroutine(coroutine);
    }

    // after delay, retract arm
    private IEnumerator FinishWipe()
    {
        animating = true;
        yield return new WaitForSeconds(0.5f);
        animating = false;
    }
}
