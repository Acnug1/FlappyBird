using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGenerator : ObjectPool
{
    [SerializeField] private GameObject _template; // шаблон
    [SerializeField] private float _secondsBetweenSpawn; // время между спавнами
    [SerializeField] private float _maxSpawnPositionY; // мин и макс расстояние спавна труб по оси Y
    [SerializeField] private float _minSpawnPositionY;

    private float _elapsedTime = 0;

    private void Start()
    {
        Initialize(_template); // инициализируем пул
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime; // считаем прошеднее время

        if (_elapsedTime > _secondsBetweenSpawn) // если прошло времени больше, чем время между спавнами
        {
            if (TryGetObject(out GameObject pipe)) // если у нас получается взять нашу трубу (если возвращается значение true), возвращаем в качестве параметра активную трубу из пула
            {
                _elapsedTime = 0; // обнуляем прошеднее время

                float spawnPositionY = Random.Range(_minSpawnPositionY, _maxSpawnPositionY); // задаем случайное значение спавна труб по Y в нашем диапазоне
                Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z); // позиция где мы спавним трубы
                pipe.SetActive(true); // включаем вернувшуюся из пула неактивную трубу
                pipe.transform.position = spawnPoint;

                DisableObjectAbroadScreen(); // при спавне следующей трубы проверяем позицию относительно области видимости камеры предыдущих труб
            }
        }
    }
}
