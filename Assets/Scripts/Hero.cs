using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour //Класс Hero наследуется от MonoBehaviour
{
    int a;
    public float speed;
    void Start() //Функция, которая будет вызываться только один раз. Можно использовать для генерации мира.
    {
        
    }
    
    void Update() //Функция, которая будет обновляться в каждом кадре, в зависимости от fps
    {
        if (Input.GetKey(KeyCode.W)) //Пока мы зажимаем кнопку W, происходит движение вперёд 
        {
            transform.position += transform.forward * speed * Time.deltaTime; //Мы прибавляем к позиции, в которой находится персонаж, новую позицию умноженую на скорость персонажжа и на время за которое должен пройти персонаж нужное расстояние
        }
        if (Input.GetKey(KeyCode.S)) //Пока мы зажимаем кнопку S, происходит движение назад 
        {
            transform.position -= transform.forward * speed * Time.deltaTime; //Мы отнимаем от позиции, в которой находится персонаж, новую позицию умноженую на скорость персонажжа и на время за которое должен пройти персонаж нужное расстояние
        }
    }
}
