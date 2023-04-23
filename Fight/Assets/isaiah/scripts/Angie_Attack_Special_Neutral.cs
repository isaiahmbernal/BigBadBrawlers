using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angie_Attack_Special_Neutral : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private bool canCast;
    [SerializeField] private float chargeTime;
    [SerializeField] private float lifeTime;
    [SerializeField] private float recoverTime;
    [SerializeField] private bool pressedSpecial;
    [SerializeField] private Angie_Attack_Special_Neutral_Collider rFlurry;
    [SerializeField] private Angie_Attack_Special_Neutral_Collider lFlurry;

    private void Awake() {
        anim = gameObject.GetComponentInParent<Animator>();
        canCast = true;
        chargeTime = .9f;
        lifeTime = 1f;
        recoverTime = .4f;
        pressedSpecial = false;
        rFlurry = gameObject.transform.Find("R-Flurry").GetComponent<Angie_Attack_Special_Neutral_Collider>();
        lFlurry = gameObject.transform.Find("L-Flurry").GetComponent<Angie_Attack_Special_Neutral_Collider>();
    }

    // Check for player input
    private void Update() {

        if (gameObject.name == "Player_One")
        {
            if (Input.GetButtonDown("Player_One_Heavy") 
            && !anim.GetBool("isHurt")
            && !anim.GetBool("isAttacking")
            && !anim.GetBool("isBlocking")
            && !anim.GetBool("isDodging")
            && !anim.GetBool("fireSpecial")
            && canCast)
            {
                pressedSpecial = true;
            }
        }

        if (gameObject.name == "Player_Two")
        {
            if (Input.GetButtonDown("Player_Two_Heavy") 
            && !anim.GetBool("isHurt")
            && !anim.GetBool("isAttacking")
            && !anim.GetBool("isBlocking")
            && !anim.GetBool("isDodging")
            && !anim.GetBool("fireSpecial")
            && canCast)
            {
                pressedSpecial = true;
            }
        }
    }

    private void FixedUpdate() {
        if (pressedSpecial){
            pressedSpecial = false;
            StartCoroutine(FistFlurry());
        }
    }

    // IEnumerator to start the fist flurry
    // attack, and waits to time the charge
    // time, the duration of the actual attack,
    // and the recovery time (also checks before
    // each one whether they've been attacked so
    // we can cancel it)
    private IEnumerator FistFlurry() {

        // Start Special
        if (anim.GetBool("isHurt")) {
            anim.SetBool("fireSpecial", false);
            yield return new WaitForSeconds(recoverTime);
            canCast = true;
            yield break;
        }
        canCast = false;
        anim.SetBool("fireSpecial", true);
        anim.SetBool("canMove", false);
        anim.SetBool("canTurn", false);

        // After Charging
        yield return new WaitForSeconds(chargeTime);
        if (anim.GetBool("isHurt")) {
            anim.SetBool("fireSpecial", false);
            yield return new WaitForSeconds(recoverTime);
            canCast = true;
            yield break;
        }
        if (anim.GetInteger("Direction") > 0) {
            rFlurry.StartFlurry();
        } else {
            lFlurry.StartFlurry();
        }

        // After Finishing
        yield return new WaitForSeconds(lifeTime);
        if (anim.GetBool("isHurt")) {
            anim.SetBool("fireSpecial", false);
            yield return new WaitForSeconds(recoverTime);
            canCast = true;
            yield break;
        }
        // if (anim.GetInteger("Direction") > 0) {
        //     rFlurry.StopFlurry();
        // } else {
        //     lFlurry.StopFlurry();
        // }
        
        // After Recovering
        yield return new WaitForSeconds(recoverTime);
        if (anim.GetBool("isHurt")) {
            anim.SetBool("fireSpecial", false);
            yield return new WaitForSeconds(recoverTime);
            canCast = true;
            yield break;
        }
        anim.SetBool("fireSpecial", false);
        anim.SetBool("canMove", true);
        anim.SetBool("canTurn", true);
        canCast = true;
    }
}
