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
    public bool redDice;
    float forceX, forceY, forceZ;
    Rigidbody rb;
    [HideInInspector] public bool diceHasRolled;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GetTheRolledNumber();
    }
    public void RollDice()
    {
        //reset number to 0
        numberRolled = 0;

        rb.AddForce(Vector3.up * upwardForce, ForceMode.Force);

        forceX = UnityEngine.Random.Range(0, maxAxisForce);
        forceY = UnityEngine.Random.Range(0, maxAxisForce);
        forceZ = UnityEngine.Random.Range(0, maxAxisForce);

        rb.AddTorque(forceX, forceY, forceZ);

        //Adding delay before checking the rolledNumber
        StartCoroutine(EnableDiceRoll());
    }
    public void GetTheRolledNumber()
    {
        if (diceHasRolled && rb.velocity.magnitude <= 0f)
        {
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
