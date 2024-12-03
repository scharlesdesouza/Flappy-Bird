using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody thisRigidbody;
    public float jumpPower = 30f;  // Ajuste a força de pulo para um valor maior
    public float jumpInterval = 0.2f;
    private float jumpCooldown = 0;

    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();

        Physics.gravity = new Vector3(0, -25f, 0);  // Aumenta a força de gravidade para uma queda mais rápida
    }

    void Update()
    {
        //cooldown de pulo
        jumpCooldown -= Time.deltaTime;
        bool isGameActive = GameManager.Instance.IsGameActive();
        bool canJump = jumpCooldown <= 0 && isGameActive;

        // Pulo
        if (canJump)
        {
            bool jumpInput = Input.GetKey(KeyCode.Space);  
            if (jumpInput)
            {
                Jump();  
            }
        }

        // Desliga a gravidade quando o jogo não estiver ativo
        thisRigidbody.useGravity = isGameActive;
    }

    void OnCollisionEnter(Collision other)
    {
        onCustomCollisionEnter(other.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        onCustomCollisionEnter(other.gameObject);
    }

    private void onCustomCollisionEnter(GameObject other)
    {
        bool isSensor = other.CompareTag("Sensor");
        if (isSensor)
        {
            GameManager.Instance.score++;
            Debug.Log("Score " + GameManager.Instance.score);
        }
        else
        {
            GameManager.Instance.EndGame();
        }
    }

    private void Jump()
    {
        jumpCooldown = jumpInterval;

        // Aplica uma força de impulso
        thisRigidbody.linearVelocity = Vector3.zero;  
        thisRigidbody.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse); 
    }
}
