using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntBodyAnimator : MonoBehaviour
{
    // Components
    [SerializeField] private Transform[] legsL;
    [SerializeField] private Transform[] legsR;
    // Properties
    [SerializeField] private float LegOscSpeed = 10; // HIGHER is FASTER leg pumping.
    private float legOscLoc; // increments, and is passed into Sin functions to wave-up the legs' rotations.
    private float prevAnglesY;
    private Vector3 prevPos;
    private float legAmplitude; // eases to 0 when stationary! Eases to 30 when moving!


    void Update() {
        float currVel = Vector3.Distance(transform.position, prevPos);
        float angleVel = Mathf.Abs(transform.localEulerAngles.y - prevAnglesY);

        // Update legAmplitude
        bool isMoving = currVel > 0.005f || angleVel>0.5f;
        float targetAmplitude = isMoving ? 36 : 0;
        legAmplitude += (targetAmplitude-legAmplitude) * 0.1f;

        // Update legOscLoc
        legOscLoc += currVel * LegOscSpeed;
        legOscLoc += angleVel * LegOscSpeed*0.01f;

        // Apply legOscLoc
        for (int i = 0; i<legsL.Length; i++) {
            float angleZ = Mathf.Abs(Mathf.Sin(legOscLoc+0.6f*i)) * legAmplitude;
            legsL[i].transform.localEulerAngles = new Vector3(legsL[i].transform.localEulerAngles.x, legsL[i].transform.localEulerAngles.y, angleZ);
        }
        for (int i = 0; i<legsL.Length; i++) {
            float angleZ = Mathf.Abs(Mathf.Sin(legOscLoc+0.6f*i+1)) * legAmplitude;
            legsR[i].transform.localEulerAngles = new Vector3(legsR[i].transform.localEulerAngles.x, legsR[i].transform.localEulerAngles.y, angleZ);
        }

        prevPos = transform.position;
        prevAnglesY = transform.localEulerAngles.y;
    }
}
