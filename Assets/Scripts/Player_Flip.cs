using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_Flip : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private float horizontalInput;
    private bool faceimRight = true;

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        SetupDirectionByScale();
        //SetupDreictionByComponent() //2e méthode pour flip
        SetupDirectionByRotation(); //Permet d'etre symétrique pour des lancers d'objets (hache)
    }

    private void SetupDirectionByScale()
    {
        if(horizontalInput < 0 && faceimRight || horizontalInput > 0 && !faceimRight)
        {
            faceimRight = !faceimRight;
            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1;
            transform.localScale = playerScale;
        }
    }

    private void SetupDreictionByComponent() //2e méthode pour flip
    {
        if(horizontalInput <0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (horizontalInput > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }

    private void SetupDirectionByRotation()
    {
        if(horizontalInput < 0 && faceimRight || horizontalInput > 0 && !faceimRight) {
            faceimRight = !faceimRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }
        
    }
}
