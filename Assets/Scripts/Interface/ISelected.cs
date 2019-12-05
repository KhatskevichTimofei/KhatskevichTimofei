using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelected // Публичный метод. Как правило названия интерфейса пишется с I(Больше относится к стилю программирования). Interface способен принимать
    // 1. Метод; 2. Свойства; ?? 3. Индексаторы 4. События ?? 5. статические и константы. Interface может быть родителем.
{
    bool IsSelected { get; set; } // Переменная булового типа, которая принимает значение


}
