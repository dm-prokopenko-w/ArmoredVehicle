using Game;
using System;
using System.Collections;
using UnityEngine;

namespace PlayerSystem
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;

        private Action _onDestroy;
        private Coroutine _coroutine;

        public void Move(Vector3 dir, Action onDestroy)
        {
            _rb.AddForce(new Vector3(dir.x, 0, dir.z) * Constants.SpeedBullet);
            _onDestroy = onDestroy;
            _coroutine = StartCoroutine(DestroyTimer());
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(10f);
            DestroyBullet();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Enemy"))
            {
                DestroyBullet();
                StopCoroutine(_coroutine);
            }
        }

        private void DestroyBullet()
        {
            _rb.velocity = Vector3.zero;
            _onDestroy?.Invoke();
            _onDestroy = null;
        }
    }
}
