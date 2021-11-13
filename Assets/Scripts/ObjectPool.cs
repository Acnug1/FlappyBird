using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container; // контейнер для объектов
    [SerializeField] private int _capacity; // вместимость пула

    private Camera _camera; // для удаления понадобится камера

    private List<GameObject> _pool = new List<GameObject>(); // и пул наших объектов

    protected void Initialize(GameObject prefab) // инициализируем пул
    {
        _camera = Camera.main;

        for (int i = 0; i < _capacity; i++) // пока не заполнится пул
        {
            GameObject spawned = Instantiate(prefab, _container.transform); // создаем объект из префаба
            spawned.SetActive(false); // отключаем его

            _pool.Add(spawned); // добавляем в пул
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false); // возвращает первый отключенный компонент или null, если компонент не найден
        return result != null; // возвращаем результат (true или false)
    }

    protected void DisableObjectAbroadScreen() // выключить объекты за пределами экрана
    {
        Vector3 disablePoint = _camera.ViewportToWorldPoint(new Vector2(0, 0)); // получаем точку отключения трубы в начале области видимости камеры и передаем её в координаты мирового пространства (мы получаем мировую координату, где находится левая точка нашего экрана (камеры))
        // в параметрах передаем локальные координаты области видимости камеры (преобразование из локальных координатов камеры в глобальные координаты мирового пространства)

        foreach (var item in _pool) // перебираем пул
        {
            if (item.activeSelf == true) // если объект в пуле включен
            {
              //  Vector3 point = _camera.WorldToViewportPoint(item.transform.position); проекция местоположения трубы относительно области видимости камеры на мировых координатах 
                // (по оси Х: если значение < 0 слева от области видимости камеры, от 0 до 1 в пределах видимости камеры, если > 0 справа от области видимости камеры)
               // if (disablePoint.x < 0) если объект вышел за пределы видимости камеры

               if (item.transform.position.x < disablePoint.x) // если позиция трубы левее позиции границы камеры (точки отключения)
                    item.SetActive(false); // то мы отключаем объект
            }
        }
    }

    public void ResetPool()
    {
        foreach (var item in _pool)
        {
            item.SetActive(false);
        }
    }
}
