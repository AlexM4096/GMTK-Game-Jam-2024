using AlexTools;
using AlexTools.Extensions;
using Flyweight;
using System.Collections;
using UnityEngine;

namespace Alex
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float spawnCooldown = 5f;
        [SerializeField] private FlyweightSettings settings;
        [SerializeField] private Vector2 offset;

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
                yield return Waiters.GetWaitForSeconds(spawnCooldown);
            }
        }

        private void Spawn()
        {
            var entity = FlyweightFactory.Instance.Get(settings);
            entity.transform.position = GetRandomPosition();
        }

        private Vector3 GetRandomPosition()
        {
            float addX = Random.Range(0, offset.x);
            float addY = Random.Range(0, offset.y);

            float height = _mainCamera.orthographicSize;
            float width = _mainCamera.aspect * height;

            var position = new Vector2(width + addX, height + addY);

            var flipHotizontal = Random.value < 0.5f;
            if (flipHotizontal) position = position.With(y: -position.y);

            var flipVertical = Random.value < 0.5f;
            if (flipVertical) position = position.With(x: -position.x);

            return position;
        }
    }
}