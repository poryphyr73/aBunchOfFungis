using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Transform playerTransform;
    private Rigidbody2D playerBodyPhys;

    private Animator playerAnimator;
    public float animationProduct;
    public float animationProductSaved;
    private bool animationSprint;

    private float universalConstant = 0.0625f;
    public float speedModifier;
    private float speed;
    public playerCombat playerCombat;

    public Animator weaponAnimator;
    public SpriteRenderer weaponSprite;
    public bool attackDouble;

    void Start()
    {
        playerTransform = GetComponent<Transform>();
        playerBodyPhys = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponentInChildren<Animator>();
        playerCombat = GetComponent<playerCombat>();
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        speed = universalConstant * speedModifier;

        var movementX = Input.GetAxisRaw("Horizontal");
        var movementY = Input.GetAxisRaw("Vertical");

        animationProduct = movementX * 2 + movementY * 3;

        if(animationProduct != 0)
        {
            animationProductSaved = animationProduct;
        }

        playerAnimator.SetFloat("AnimationProduct", animationProduct);
        playerAnimator.SetBool("Sprint", animationSprint);

        weaponAnimator.SetFloat("AnimationProduct", animationProductSaved);
        weaponRenderSet();

        //Debug.Log(animationProduct);
        if (playerCombat.nextAttackTime <= Time.time - 1)
        {
            playerTransform.position += new Vector3(movementX, movementY, 0) * Time.deltaTime * speed;
            weaponAnimator.ResetTrigger("Attacking");
        }
    }

    private void weaponRenderSet()
    {
        if (animationProductSaved == -3)
        {
            weaponSprite.sortingLayerName = "WeaponFront";
        }
        else if (animationProductSaved == 1)
        {
            weaponSprite.sortingLayerName = "WeaponFront";
        }
        else if (animationProductSaved == 5)
        {
            weaponSprite.sortingLayerName = "WeaponFront";
        }
        else if(animationProductSaved != 0)
        {
            weaponSprite.sortingLayerName = "WeaponRear";
        }
    }
}
