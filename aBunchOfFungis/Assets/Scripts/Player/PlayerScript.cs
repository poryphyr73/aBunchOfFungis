using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Transform playerTransform;
    private Rigidbody2D playerBodyPhys;

    private Animator playerAnimator;
    public float animationProduct;
    private bool animationSprint;

    private float universalConstant = 0.0625f;
    public float speedModifier;
    [SerializeField] private float sprintMod;
    private float speed;
    public playerCombat playerCombat;

    public Animator weaponAnimator;
    public SpriteRenderer weaponSprite;

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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= sprintMod;
            animationSprint = true;
        }
        else
        {
            animationSprint = false;
        }

        playerAnimator.SetFloat("AnimationProduct", animationProduct);
        playerAnimator.SetBool("Sprint", animationSprint);

        weaponAnimator.SetFloat("AnimationProduct", animationProduct);
        weaponAnimator.SetBool("Sprint", animationSprint);

        Debug.Log(animationProduct);
        if (playerCombat.nextAttackTime <= Time.time)
        {
            playerTransform.position += new Vector3(movementX, movementY, 0) * Time.deltaTime * speed;
        }
    }

    private void weaponRenderSet()
    {
        if(animationProduct == 0)
        {
            weaponSprite.rendererPriority = 1;
        }else if(animationProduct != 0)
        {
            weaponSprite.rendererPriority = -1;
        }
    }
}
