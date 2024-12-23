using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                {
                    Debug.Log("This is fine");
                    break;
                }
            case "Finish":
                {
                    break;
                }
            default:
                {
                    Debug.Log("Collided with something");
                    break;
                }
        }
    }
}
