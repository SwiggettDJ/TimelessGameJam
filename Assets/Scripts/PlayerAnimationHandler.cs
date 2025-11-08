using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator playerAnimator;
    [SerializeField]private ParticleSystem sprintGroundParticles;
    [SerializeField]private ParticleSystem sprintAirParticles;

    private PlayerNetwork player;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        player = GetComponentInParent<PlayerNetwork>();

        player.OnPlayerJump += AnimateJump;
        player.OnPlayerWalk += AnimateWalk;
        player.OnPlayerSprint += AnimateSprint;
        player.OnPlayerFall += AnimateFall;
        
    }
    

    private void AnimateJump()
    {
        playerAnimator.SetTrigger("jump");
    }

    private void AnimateSprint(bool isSprinting)
    {
        if (isSprinting)
        {
            //need to make ground particles spawn on other player's screen as well
            sprintGroundParticles.Play();
            sprintAirParticles.Play();
        }
        else
        {
            sprintGroundParticles.Stop();
            sprintAirParticles.Stop();
        }
        playerAnimator.SetBool("isSprinting", isSprinting);
    }

    private void AnimateWalk(bool isWalking)
    {
        playerAnimator.SetBool("isWalking", isWalking);
    }

    private void AnimateFall(bool isFalling)
    {
        playerAnimator.SetBool("isFalling", isFalling);
    }
}
