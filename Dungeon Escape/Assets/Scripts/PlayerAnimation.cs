using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator _playerAnim;
    [SerializeField] Animator _swordAnim;

    public void MoveAnim(float input)
    {
        _playerAnim.SetFloat("Running", input);
    }

    public void JumpAnim(bool input)
    {
        _playerAnim.SetBool("Jumping", input);
    }

    public void AttackAnim()
    {
        _playerAnim.SetTrigger("Attack");
        _swordAnim.SetTrigger("SwordAnimation");

    }

}
