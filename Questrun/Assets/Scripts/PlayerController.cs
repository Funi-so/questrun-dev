using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    enum PlayerPos { Left, Right, Middle };

    PlayerPos currentPos;
    private Vector3 jump;
    private Vector2 touchStartPosition, touchEndPosition;
    public float jumpForce = 2.0f, groundPos;
    private Touch toque;
    private bool movable=true;
    Rigidbody rb;

    public bool isGrounded()
    {
        if (transform.position.y <= groundPos)
            return true;
        else
            return false;
    }
    
    void Start()
    {
        //currentPos = PlayerPos.Middle;
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        groundPos = transform.position.y;
    }

    void Update()
    {
       if (GameManager.Instance.CurrentGameState() == GameManager.GameState.Jogando)
       {
            if(Input.touchCount > 0)
            {
                toque = Input.GetTouch(0);
                print("tocou");
                if (toque.phase == TouchPhase.Began)
                {

                    touchStartPosition = toque.position;
                } else if (toque.phase == TouchPhase.Moved || toque.phase == TouchPhase.Ended)
                    {
                    touchEndPosition = toque.position;
                    float x = touchEndPosition.x - touchStartPosition.x;
                    float y = touchEndPosition.y - touchStartPosition.y;
                    
                    if (Mathf.Abs(x) > Mathf.Abs(y))
                    {
                        if (x > 0 && currentPos != PlayerPos.Right)
                        {
                            if (movable) 
                            { 
                                if (currentPos == PlayerPos.Left)
                                {
                                    this.gameObject.transform.position = new Vector3(0, transform.position.y, transform.position.z);
                                    currentPos = PlayerPos.Middle;
                                    movable = false;
                                    Invoke("Cooldown", 0.3f);
                                }
                                else
                                {
                                    this.gameObject.transform.position = new Vector3(1, transform.position.y, transform.position.z);
                                    currentPos = PlayerPos.Right;
                                    movable = false;
                                    Invoke("Cooldown", 0.3f);
                                }
                            }
                        }
                        else 
                        {
                            if (currentPos != PlayerPos.Left && movable)
                            {
                                if (currentPos == PlayerPos.Right)
                                {
                                    this.gameObject.transform.position = new Vector3(0, transform.position.y, transform.position.z);
                                    currentPos = PlayerPos.Middle;
                                    movable = false;
                                    Invoke("Cooldown", 0.3f);
                                }
                                else
                                {
                                    this.gameObject.transform.position = new Vector3(-1, transform.position.y, transform.position.z);
                                    currentPos = PlayerPos.Left;
                                    movable = false;
                                    Invoke("Cooldown", 0.3f);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (y > 0 && isGrounded() && movable)
                        {
                            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                            movable = false;
                            Invoke("Cooldown", 0.3f);
                        } 
                    }
                }
            }
        }
    }

    void Cooldown()
    {
        movable = true;
    }


}
