using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] Transform[] diceFaces;
    [SerializeField] float checkSphereRad = .5f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] public int numberRolled;

    [Header("Dice Settings")]
    [SerializeField] float maxAxisForce;
    [SerializeField] float upwardForce;
    [NonReorderable]
    [SerializeField] FacesOppositeNumbers[] facesOppositeNumber;
    float forceX, forceY, forceZ;
    Rigidbody rb;
    bool diceHasRolled;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && rb.velocity.magnitude <= 0f)
        {
            RollDice();
        }
        GetTheRolledNumber();
    }
    private void RollDice()
    {
        rb.AddForce(Vector3.up * upwardForce, ForceMode.Force);

        forceX = UnityEngine.Random.Range(0, maxAxisForce);
        forceY = UnityEngine.Random.Range(0, maxAxisForce);
        forceZ = UnityEngine.Random.Range(0, maxAxisForce);

        rb.AddTorque(forceX, forceY, forceZ);

        //Adding delay before checking the rolledNumber
        StartCoroutine(EnableDiceRoll());
    }
    private void GetTheRolledNumber()
    {
        if (diceHasRolled && rb.velocity.magnitude <= 0f)
        {
            print("Velocity is Zero");
            foreach (Transform face in diceFaces)
            {
                if (Physics.CheckSphere(face.position, checkSphereRad, groundLayer))
                {
                    //Getting the name/number of the collided transform
                    //Matching it in the FaceOpposite number array
                    foreach (FacesOppositeNumbers faceNumber in facesOppositeNumber)
                    {
                        if (int.Parse(face.transform.name) == faceNumber.faceNumber)
                        {
                            print($"faceName: {face.transform.name}... Face Number: {faceNumber.faceNumber}... OppositeFace: {faceNumber.faceOppositeNumber}");
                            numberRolled = faceNumber.faceOppositeNumber;
                            diceHasRolled = false;
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }

    IEnumerator EnableDiceRoll()
    {
        yield return new WaitForSeconds(1f);
        diceHasRolled = true;
    }

    private void OnDrawGizmos()
    {
        if (diceFaces.Length <= 0) { return; }
        Gizmos.color = Color.red;
        foreach (Transform face in diceFaces)
        {
            Gizmos.DrawWireSphere(face.position, checkSphereRad);
        }
    }
}

[System.Serializable]
public class FacesOppositeNumbers
{
    public int faceNumber;
    public int faceOppositeNumber;
}
