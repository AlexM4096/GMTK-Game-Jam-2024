using AlexTools;
using AlexTools.Extensions;
using FlyweightSystem;
using System.Collections;
using UnityEngine;

namespace Alex
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float baseCooldown = 5f;
        [SerializeField] private float minCooldown = 2.5f;
        [SerializeField] private float speedOfDecreaseCooldownPerSecond = 0.005f;
        [SerializeField] private FlyweightSettings settings;
        [SerializeField] private Vector2 offset;
        [SerializeField] private Timer timer;

        private Camera _mainCamera;
        private Coroutine _coroutine;

        private void Start()
        {
            _mainCamera = Camera.main;
            _coroutine = StartCoroutine(SpawningRoutine());
        }

        private IEnumerator SpawningRoutine()
        {
            while (true)
            {
                Spawn();
                var decreaseCooldown = Mathf.Abs(timer.CurrentTime - timer.InitialTime) * speedOfDecreaseCooldownPerSecond;
                var cd = (baseCooldown - decreaseCooldown).AtLeast(minCooldown);
                yield return Waiters.GetWaitForSeconds(cd);
            }
        }

        private void Spawn()
        {
            var entity = FlyweightFactory.Instance.Get(settings);
            entity.transform.position = GetRandomPosition();
        }

        private Vector3 GetRandomPosition()
        {
            float height = _mainCamera.orthographicSize;
            float width = _mainCamera.aspect * height;

            float x, y;

            if (Random.value < 0.5f)
            {
                x = Random.Range(0, width + offset.x);
                y = height + offset.y;
            }
            else
            {
                x = width + offset.x;
                y = Random.Range(0, height + offset.y);
            }

            var position = new Vector2(x, y);

            var flipHotizontal = Random.value < 0.5f;
            if (flipHotizontal) position = position.With(y: -position.y);

            var flipVertical = Random.value < 0.5f;
            if (flipVertical) position = position.With(x: -position.x);

            return position;
        }
    }
}