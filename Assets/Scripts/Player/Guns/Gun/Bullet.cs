    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody Rigidbody;
        [field: SerializeField]

        public Vector3 SpawnLocation
        {
        get;
        private set;
        }
        [SerializeField]
        private float DelayedDisableTime = 5f;

        public delegate void CollisionEventI(Bullet Bullet, Collision Collision);
        public event CollisionEventI OnCollision;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        public void Spawn(Vector3 SpawnForce)
        {
            SpawnLocation = transform.position;
            transform.forward = SpawnForce.normalized;
            Rigidbody.AddForce(SpawnForce);
            StartCoroutine(DelayedDisable(DelayedDisableTime));
        }

        private IEnumerator DelayedDisable(float Time)
        {
            yield return new WaitForSeconds(Time);
            OnCollisionEnter(null);
        }

        private void OnCollisionEnter(Collision Collision)
        {
            OnCollision?.Invoke(this, Collision);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            Rigidbody.velocity = Vector3.zero;
            Rigidbody.angularVelocity = Vector3.zero;
            OnCollision = null;
        }

    }
