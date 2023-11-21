using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float speed = 10f; // the movement speed is 10m/s
    public float fireRate = 0.3f; // seconds/shot (unused)
    public float health = 10; // damage needed to destroy this enemy
    public int score = 100; // points earned for destroying this

    private BoundsCheck bndCheck;

    void Awake() {
        bndCheck = GetComponent<BoundsCheck>();
    }
    
    // This is a Property: A method that acts like a field
    public Vector3 pos {
        get {
            return this.transform.position;
        }
        set {
            this.transform.position = value;
        }
    }

    public virtual void Move() {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    void OnCollisionEnter(Collision coll) {
        GameObject otherGO = coll.gameObject;
        if (otherGO.GetComponent<ProjectileHero>() != null) {
            Destroy(otherGO); // Destroy the Projectile
            Destroy(gameObject); // Destroy this Enemy GameObject
        } else {
            Debug.Log("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        // Check whether this Enemy has gone off the bottom of the screen
        if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offDown)) {
            Destroy( gameObject );
        }


/*         if (!bndCheck.isOnScreen) {
            if (pos.y < bndCheck.camHeight - bndCheck.radius) {
                // off the bottom, so destroy this GameObject
                Destroy(gameObject);
            }
        } */
    }
}
