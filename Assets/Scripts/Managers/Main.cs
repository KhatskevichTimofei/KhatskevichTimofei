using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Main : MonoBehaviour
{
    public static Main instance; //Создаётся публичная статичная переменнная типа Main. Static 
    public static bool isAudioCurrentFrame;

    public Vector3 startMouse;
    public Vector3 stopMouse;
    public Image image1;
    public List<Unit> allUnits = new List<Unit>();
    public List<Build> allBuild = new List<Build>();
    public List<ISelected> selected = new List<ISelected>();
    public List<ISelected> allSelectebleObjects = new List<ISelected>();
    public List<UnitsEnemy> unitsEnemies = new List<UnitsEnemy>();
    public Dictionary<KeyCode, List<ISelected>> saveSelectedObject = new Dictionary<KeyCode, List<ISelected>>();
    public Button continueButton;
    public Printer3D mainBuild;
    public Smelter smelter;
    public LegoBox legoBox;
    public Storage storage = new Storage();
    public Transform parentEnemy, parentUnit, parentBuild;
    public Animation anim;
    public bool animStart;
    bool isFrameSelected;

    void Start()
    {
        instance = this; //Переменной типа Main мы задаём здачения самого себя 
        allUnits.AddRange(parentUnit.GetComponentsInChildren<Unit>());
        allBuild.AddRange(parentBuild.GetComponentsInChildren<Build>());
        unitsEnemies.AddRange(parentEnemy.GetComponentsInChildren<UnitsEnemy>());
        allSelectebleObjects.AddRange(allUnits); //В список всех построек и юнитов записываются все юниты
        allSelectebleObjects.AddRange(allBuild); //В список всех построке и юнитов записываются все построек
        Debug.Log(Resources.LoadAll<AudioClip>("Sound/Shot").Length);
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))//При одном нажатии кнопки P выполняется условие
        //{

        //    selected.Clear(); //Проходит очистка списка выбранных юнитов
        //    selected.Add(allUnits[Random.Range(0, allUnits.Count)]); //Выбранные юниты берутся из списка всех существующих юнитов
        //}
        //if (Input.GetKeyDown(KeyCode.Y))//При одном нажатии кнопки Y выполняется условие
        //    mainBuild.CreateUnit(1);//MainBuild создаёт юнита с индексом 1 при нажатии кнопки Y 
        RightMouseClick();
        Select();
        Binds();
        if (Input.GetKeyDown(KeyCode.T))
        {
            storage.AddPlastic(50);
            storage.AddBabin(30);
        }
        storage.Update();
        isAudioCurrentFrame = false;
    }

    private void Select()
    {
        if (Input.GetMouseButtonDown(0)) //При нажатии левой кнопки мыши выполняется условие
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                startMouse = Input.mousePosition; // В пеменную типа Vector3 записывается позиция мыши
                for (int i = 0; i < selected.Count; i++)
                {
                    selected[i].IsSelected = false;
                }
                selected.Clear();//Отчищается список выбранных юнитов 
                image1.enabled = true; //Включает картинку для выделения
                isFrameSelected = true;
            }
        }
        if (Input.GetMouseButton(0) && isFrameSelected) //При отжатии левой кнопки мыши выполняется условие
        {
            stopMouse = Input.mousePosition; //В переменную типа Vector3 записывается конечное значение координат нахождения мыши 
            image1.rectTransform.anchoredPosition = startMouse; //В переменную startMouse записывается значение нахождения объекта image1 на сцене. 
            Vector2 delta = stopMouse - startMouse; //В переменную записывается координаты от разности конечной точки положения мыши на экране и начальной точки положения мыши на экране 
            if (delta.x < 0) //Блок который делает координату x положительной и смещает её по ширине 
            {
                image1.rectTransform.anchoredPosition += new Vector2(delta.x, 0); //
                delta.x *= -1;
            }
            if (delta.y < 0) //Блок который делает координату y  положительной и смещает её по высоте
            {
                image1.rectTransform.anchoredPosition += new Vector2(0, delta.y);
                delta.y *= -1;
            }
            image1.rectTransform.sizeDelta = delta;// Картинке передаётся размер находящийся в переменной delta
        }
        if (Input.GetMouseButtonUp(0) && isFrameSelected) //Выделяет юнитов попавших в картинку
        {
            bool isUnitsSelect = false;
            for (int i = 0; i < allSelectebleObjects.Count; i++) //Создаёт новое изображение и записываает его в перменную area. 
            {
                Rect area = new Rect(image1.rectTransform.anchoredPosition, image1.rectTransform.sizeDelta);
                if (area.Contains(Camera.main.WorldToScreenPoint((allSelectebleObjects[i] as MonoBehaviour).transform.position))) //Если в созданную картинку area попадает какой либо юнит, выполняется условие  и записывает из массива allUnits юнитов в массив selectedUnits 
                {
                    if (allSelectebleObjects[i] as Unit != null)
                    {
                        isUnitsSelect = true;
                        break;
                    }
                }
                image1.enabled = false;//Выключает картинку

            }
            for (int i = 0; i < allSelectebleObjects.Count; i++) //Создаёт новое изображение и записываает его в перменную area. 
            {
                Rect area = new Rect(image1.rectTransform.anchoredPosition, image1.rectTransform.sizeDelta);
                if (!isUnitsSelect || isUnitsSelect && allSelectebleObjects[i] as Unit != null)
                {
                    if (area.Contains(Camera.main.WorldToScreenPoint((allSelectebleObjects[i] as MonoBehaviour).transform.position))) //Если в созданную картинку area попадает какой либо юнит, выполняется условие и записывает из массива allUnits юнитов в массив selectedUnits 
                    {
                        selected.Add(allSelectebleObjects[i]);
                        allSelectebleObjects[i].IsSelected = true;
                    }
                    image1.enabled = false;//Выключает картинку
                }
            }
            isFrameSelected = false;
        }
    }

    private void Binds()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            for (int i = 48; i < 58; i++)
                if (Input.GetKeyDown((KeyCode)i))
                {
                    if (saveSelectedObject.ContainsKey((KeyCode)i))
                        saveSelectedObject.Remove((KeyCode)i);

                    saveSelectedObject.Add((KeyCode)i, new List<ISelected>(selected));
                }
        }
        else
            for (int i = 48; i < 58; i++)
                if (Input.GetKeyDown((KeyCode)i))
                    if (saveSelectedObject.ContainsKey((KeyCode)i))
                    {
                        for (int j = 0; j < selected.Count; j++)
                        {
                            selected[j].IsSelected = false;
                        }
                        selected.Clear();

                        selected.AddRange(saveSelectedObject[(KeyCode)i]);
                        for (int j = 0; j < selected.Count; j++)
                        {
                            selected[j].IsSelected = true;
                        }

                    }
    }

    private void RightMouseClick()
    {
        if (Input.GetMouseButtonDown(1)) //При нажатии правой кнопки выполняется условие 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Создаётся переменнная типа Ray(луч) ??????????
            RaycastHit casthit; // Создаётся переменнная типа RaycastHit(???)

            if (Physics.Raycast(ray, out casthit)) // ????
            {
                IActivity target = casthit.transform.GetComponent<IDestroyed>();
                if (target != null && (target as Build != null && (target as Build).sideConflict == SideConflict.Enemy || target as Character != null && (target as Character).sideConflict == SideConflict.Enemy || target as Interactive != null))
                {
                    for (int i = 0; i < selected.Count; i++)
                    {
                        if (selected[i] as Unit != null)
                        {
                            (selected[i] as Unit).SetTarget(target);
                            (selected[i] as Unit).typeTarget = TypeTarget.Set;
                            AudioManager.AddAudio(selected[i] as Character, "Attack");
                            //AudioManager.AddAudio(transform, "TargetEnemy");
                        }
                    }
                }
                else
                {
                    target = casthit.transform.GetComponent<CollectionUP>();
                    if (target != null)
                    {
                        for (int i = 0; i < selected.Count; i++)
                        {
                            if ((selected[i] as Unit).collectionUP == null)
                            {
                                (selected[i] as Unit).SetTarget(target);
                                (selected[i] as Unit).typeTarget = TypeTarget.Set;
                                AudioManager.AddAudio(selected[i] as Character, "Collect");
                                break;
                            }
                        }
                    }
                    else
                    {
                        target = casthit.transform.GetComponent<LegoBox>();
                        if (target == null)
                            target = casthit.transform.GetComponent<Smelter>();
                        if (target != null)
                        {
                            for (int i = 0; i < selected.Count; i++)
                                if (selected[i] as Unit)
                                {
                                    (selected[i] as Unit).SetTarget(target);
                                    (selected[i] as Unit).typeTarget = TypeTarget.Set;
                                    if (target as Smelter)
                                        AudioManager.AddAudio(selected[i] as Character, "Collect");
                                    else AudioManager.AddAudio(selected[i] as Character, "Drop");
                                }
                        }
                        else
                            for (int i = 0; i < selected.Count; i++)//Цикл который проходится по всем выбранным юнитам
                            {
                                if (selected[i] as Unit != null)
                                {
                                    AudioManager.AddAudio(selected[i] as Character, "Go");
                                    (selected[i] as Unit).SetTarget(null);
                                    (selected[i] as Unit).typeTarget = TypeTarget.Set;
                                    (selected[i] as Unit).SetTargetPosition(casthit.point);//Каждый выбранный юнит обращается к выбранной позиции, которая задаётся с помощью луча 
                                }
                            }
                    }
                }
            }
            //    if (agent.CalculatePath(casthit.point, path))
            //    {
            //        agent.SetPath(path);
            //    }

        }
    }
}
