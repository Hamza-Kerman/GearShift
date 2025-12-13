using UnityEngine;

public class PlayerAnim8Dir : MonoBehaviour {
    Animator anim;
    Vector2 input;
    Vector2 lastDir = Vector2.down;

    void Awake() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        bool moving = input != Vector2.zero;

        if (moving) {
            lastDir = input.normalized;
            anim.SetFloat("moveX", lastDir.x);
            anim.SetFloat("moveY", lastDir.y);
        }

        anim.SetBool("isMoving", moving);

        if (!moving) {
            anim.SetFloat("moveX", lastDir.x);
            anim.SetFloat("moveY", lastDir.y);
        }
    }
}
