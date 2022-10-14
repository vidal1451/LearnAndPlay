using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed;

    Transform playerTransform;
    bool walking;
    
    [SerializeField] Animator playerAnimator;
    [SerializeField] ParticleSystem effect;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = this.transform;
        walking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            walking = false;
            playerAnimator.SetTrigger("excited");
            effect.Play();
            return;
        }
        CalculateRotation(Input.GetAxis("Horizontal"), walking);
        Move(Input.GetAxis("Horizontal"));
    }

    private void Move(float move)
    {
        if (move == 0)
        {
            playerAnimator.SetBool("walking", false);
            walking = false;
            return;
        }
        
        if (move > 0)
        {
            playerTransform.Translate(new Vector3(move * speed * Time.deltaTime, 0, 0));
            playerAnimator.SetBool("walking", true);
        }
        else
        {
            playerTransform.Translate(new Vector3(move * speed * Time.deltaTime, 0, 0));
            playerAnimator.SetBool("walking", true);
        }
    }

    private void CalculateRotation(float move, bool _walking)
    {
        if (!_walking)
        {
            bool isWalking = move > 0;
            playerTransform.Rotate(Vector3.up, (isWalking? 180f : 0f));
            walking = true;
        }
    }
}
