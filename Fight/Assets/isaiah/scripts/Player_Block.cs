using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Block : MonoBehaviour
{
    public Animator anim;
    public Transform shieldTransform;
    public SpriteRenderer shieldSprite;

    public float maxShieldHealth;
    public float shieldDegrade;
    public float shieldRecover;

    public float maxScale;
    public float degradeScale;
    public float recoverScale;

    // public float sizeScale;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        shieldTransform = transform.Find("Shield");
        shieldSprite = transform.Find("Shield").GetComponent<SpriteRenderer>();
        shieldSprite.enabled = false;

        maxShieldHealth = 200f;
        anim.SetFloat("ShieldHealth", maxShieldHealth);
        shieldDegrade = .4f;
        shieldRecover = .2f;

        maxScale = shieldTransform.localScale.x;
        // degradeScale = (shieldDegrade * maxScale) / maxShieldHealth;
        // recoverScale = (shieldRecover * maxScale) / maxShieldHealth;
    }

    void Update()
    {
        if (gameObject.name == "Player_One")
        {
            if (
                Input.GetButtonDown("Player_One_Block")
                && !anim.GetBool("isHurt")
                && !anim.GetBool("isDodging")
                && anim.GetFloat("ShieldHealth") > 0f
                && !anim.GetBool("isAttacking")
                && !anim.GetBool("isCharging")
                && !anim.GetBool("fireSpecial")
                && anim.GetBool("isGrounded")
                && !anim.GetBool("isCharging")
            )
            {
                anim.SetBool("isBlocking", true);
                anim.SetBool("canMove", false);
                shieldSprite.enabled = true;
            }

            if (anim.GetFloat("ShieldHealth") < 0f)
            {
                anim.SetFloat("ShieldHealth", 0f);
                anim.SetBool("isBlocking", false);
                // anim.SetBool("canMove", true);
                anim.SetBool("wasHit", true);
                shieldSprite.enabled = false;
            }

            if (Input.GetButtonUp("Player_One_Block")
                && anim.GetBool("isBlocking")
                && !anim.GetBool("isDodging")
                && !anim.GetBool("isAttacking")
                && !anim.GetBool("isCharging")
                && !anim.GetBool("fireSpecial"))
            {
                anim.SetBool("isBlocking", false);
                anim.SetBool("canMove", true);
                shieldSprite.enabled = false;
            }
        }
        else if (gameObject.name == "Player_Two")
        {
            if (
                Input.GetButtonDown("Player_Two_Block")
                && !anim.GetBool("isHurt")
                && !anim.GetBool("isDodging")
                && anim.GetFloat("ShieldHealth") > 0f
                && !anim.GetBool("isAttacking")
                && !anim.GetBool("isCharging")
                && !anim.GetBool("fireSpecial")
                && anim.GetBool("isGrounded")
                && !anim.GetBool("isCharging")
            )
            {
                anim.SetBool("isBlocking", true);
                anim.SetBool("canMove", false);
                shieldSprite.enabled = true;
            }

            if (anim.GetFloat("ShieldHealth") < 0f)
            {
                anim.SetFloat("ShieldHealth", 0f);
                anim.SetBool("isBlocking", false);
                // anim.SetBool("canMove", true);
                anim.SetBool("wasHit", true);
                shieldSprite.enabled = false;
            }

            if (Input.GetButtonUp("Player_Two_Block")
                && anim.GetBool("isBlocking")
                && !anim.GetBool("isDodging")
                && !anim.GetBool("isAttacking")
                && !anim.GetBool("isCharging")
                && !anim.GetBool("fireSpecial"))
            {
                anim.SetBool("isBlocking", false);
                anim.SetBool("canMove", true);
                shieldSprite.enabled = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (anim.GetBool("isBlocking") && anim.GetFloat("ShieldHealth") > 0f)
        {
            anim.SetFloat("ShieldHealth", anim.GetFloat("ShieldHealth") - shieldDegrade);
            float sizeScale = (anim.GetFloat("ShieldHealth") / maxShieldHealth) * maxScale;
            shieldTransform.localScale = new Vector3(sizeScale, sizeScale, sizeScale);
            // Debug.Log("Shield Health: " + anim.GetFloat("ShieldHealth"));
        }
        else if ((!anim.GetBool("isBlocking") && !anim.GetBool("isDodging")) && anim.GetFloat("ShieldHealth") < maxShieldHealth)
        {
            anim.SetFloat("ShieldHealth", anim.GetFloat("ShieldHealth") + shieldRecover);
            // shieldTransform.localScale += new Vector3(recoverScale, recoverScale, recoverScale);
            // shieldTransform.localScale += new Vector3(shieldRecover / 100f, shieldRecover / 100f, shieldRecover / 100f);
            // Debug.Log("Shield Health: " + anim.GetFloat("ShieldHealth"));
        }
    }
}
