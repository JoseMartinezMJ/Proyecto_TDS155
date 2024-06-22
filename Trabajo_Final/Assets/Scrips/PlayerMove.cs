using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed = 2;
    public float rotationSpeed = 250;

    public Animator animator;

    private float x, y;
  

    //controla lo del salto
    public Rigidbody rb;
    public float jumpHeight = 3;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        //Obtiene la entrada del jugador en los ejes X y Y
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        //Rotar el jugador en el eje Y
        transform.Rotate(0, x*Time.deltaTime * rotationSpeed, 0);
        //Mover el jugador en el eje Z
        transform.Translate(0,0,y * Time.deltaTime* walkSpeed);

        //Actualizar los parametros del Animator con la entrada del jugador
        animator.SetFloat("VelX",x);
        animator.SetFloat("VelY", y);

        //Encargado de botones para animacion de baile
        if (Input.GetKey("1"))
        {
            animator.Play("Dance");
            animator.SetBool("Other", false);

        }

        if (Input.GetKey("2"))
        {
            animator.Play("Dance2");
            animator.SetBool("Other", false);
        }

        if (Input.GetKey("3"))
        {
            animator.Play("Dance3");
            animator.SetBool("Other", false);
        }
         //Verifica si el jugador se esta moviendo
        if(x>0 || x<0 || y>0 || y < 0)
        {
            animator.SetBool("Other", true);

        }
         //Encargado del salto y la validacion de cuando estamos en el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if(Input.GetKey("space") && isGrounded)
        {
            animator.Play("Jump");
            Invoke("Jump", 1f); //Llama al metodo jump despues de un segundo
            
        }


    }

    public void Jump()
    {
        //Tenga fuerza el rb hacia arriba como impulso
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }
}
