using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum TypeSeletedAction
{
    Defualt,
    Go,
    Attack,
    Patrol,
    CollectionUp,
}
public class Main : MonoBehaviour
{
    public static Main instance; //Создаётся публичная статичная переменнная типа Main. Static 
    public static bool isAudioCurrentFrame;
    public static float volume;

    public Vector3 startMouse;
    public Vector3 stopMouse;
    public Image image1;
    public List<Unit> allUnits = new List<Unit>();
    public static int perenosUnits;
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
    public GameObject unitsPanel;
    public GameObject funcionalUnitPanel;
    public GameObject escMenu;
    public GameObject defualtPanel;
    public Transform parentEnemy, parentUnit, parentBuild;
    public Animation anim;
    public OneUnit oneUnit;
    public bool animStart;
    bool isFrameSelected;
    public float audioTimeGo, audioTimeAttack, audioTimeCollectionUp, audioTimeDrop, audioTimeColletionUpBlock;
    public TypeSeletedAction selectedAction;
    public Character character;
    public float cdBuff;


    void Start()
    {
        instance = this; //Переменной типа Main мы задаём здачения самого себя 
        allUnits.AddRange(parentUnit.GetComponentsInChildren<Unit>());
        allBuild.AddRange(parentBuild.GetComponentsInChildren<Build>());
        unitsEnemies.AddRange(parentEnemy.GetComponentsInChildren<UnitsEnemy>());
        allSelectebleObjects.AddRange(allUnits); //В список всех построек и юнитов записываются все юниты
        allSelectebleObjects.AddRange(allBuild); //В список всех построке и юнитов записываются все построек
        //Debug.Log(Resources.LoadAll<AudioClip>("Sound/Shot").Length);
        //Resources.Load<Unit>("Prefabs/PlayerUnit"). = perenosUnits;
        //Resources.Load<Unit>("Prefabs/PlayerUnit").transform.position = new Vector3(291, 20, -324);
    }

    void Update()
    {
        //switch (prof)
        //{
        //    case Prof.Prog:

        //        break;
        //    case Prof.Dis:

        //        break;
        //    case Prof.Reck:

        //        break;
        //}

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
        if (Input.GetKeyDown(KeyCode.Escape) && !animStart)
        {
            escMenu.SetActive(true);
            animStart = true;
            anim.Play("OpenMenu");
            AudioManager.AddAudio(Camera.current.transform, "OpenMenuSound", "", false, true, SoundMusicVoice.Sound);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && animStart)
        {
            animStart = false;
            anim.Play("CloseMenu");

        }

        if (allUnits.Count == 0)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        if (audioTimeGo > 0)
            audioTimeGo -= Time.deltaTime;
        if (audioTimeAttack > 0)
            audioTimeAttack -= Time.deltaTime;
        if (audioTimeCollectionUp > 0)
            audioTimeCollectionUp -= Time.deltaTime;
        if (audioTimeDrop > 0)
            audioTimeDrop -= Time.deltaTime;
        if (audioTimeColletionUpBlock > 0)
            audioTimeColletionUpBlock -= Time.deltaTime;
        if (cdBuff > 0)
            cdBuff -= Time.deltaTime;



        storage.Update();
        isAudioCurrentFrame = false;
    }

    private void Select()
    {
        if (Input.GetMouseButtonDown(0)) //При нажатии левой кнопки мыши выполняется условие
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                unitsPanel.SetActive(false);
                funcionalUnitPanel.SetActive(false);
                startMouse = Input.mousePosition; // В пеменную типа Vector3 записывается позиция мыши
                startMouse.x *= 1920f / Screen.width;
                startMouse.y *= 1080f / Screen.height;
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
            stopMouse.x *= 1920f / Screen.width;
            stopMouse.y *= 1080f / Screen.height;
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
            Vector2 pos = image1.rectTransform.anchoredPosition;
            Vector2 size = image1.rectTransform.sizeDelta;
            pos.x /= 1920f / Screen.width;
            pos.y /= 1080f / Screen.height;
            size.x /= 1920f / Screen.width;
            size.y /= 1080f / Screen.height;
            for (int i = 0; i < allSelectebleObjects.Count; i++) //Создаёт новое изображение и записываает его в перменную area. 
            {
                Rect area = new Rect(pos, size);
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
                Rect area = new Rect(pos, size);
                if (!isUnitsSelect || isUnitsSelect && allSelectebleObjects[i] as Unit != null)
                {
                    if (area.Contains(Camera.main.WorldToScreenPoint((allSelectebleObjects[i] as UnityEngine.MonoBehaviour).transform.position))) //Если в созданную картинку area попадает какой либо юнит, выполняется условие и записывает из массива allUnits юнитов в массив selectedUnits 
                    {
                        if ((allSelectebleObjects[i] as Build) != null && !(allSelectebleObjects[i] as Build).job)
                            continue;
                        selected.Add(allSelectebleObjects[i]);
                        allSelectebleObjects[i].IsSelected = true;
                    }
                    image1.enabled = false;//Выключает картинку
                }
            }
            if (selected.Count > 1)
            {
                ShowSelectedUnits();
                oneUnit.UnitStats(selected[0] as Unit);
            }
            else if (selected.Count == 1)
            {
                if (selected[0] as Unit && (selected[0] as Unit).sideConflict == SideConflict.Player)
                {
                    funcionalUnitPanel.SetActive(true);
                }
                isFrameSelected = false;
                oneUnit.UnitStats(selected[0] as Unit);
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
            IActivity target;
            if (Physics.Raycast(ray, out casthit)) // ????
            {
                switch (selectedAction)
                {
                    case TypeSeletedAction.Defualt:
                        target = casthit.transform.GetComponent<IDestroyed>();
                        if (target != null && (target as Build != null && (target as Build).sideConflict == SideConflict.Enemy || target as Character != null && (target as Character).sideConflict == SideConflict.Enemy || target as Interactive != null))
                        {
                            for (int i = 0; i < selected.Count; i++)
                            {
                                if (selected[i] as Unit != null)
                                {
                                    (selected[i] as Unit).SetTarget(target);
                                    (selected[i] as Unit).typeTarget = TypeTarget.Set;
                                    if (audioTimeAttack <= 0)
                                    {
                                        AudioManager.AddAudio(selected[i] as Character, "Attack");
                                        audioTimeAttack = 8;
                                    }
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
                                        if (audioTimeColletionUpBlock <= 0)
                                        {
                                            AudioManager.AddAudio(selected[i] as Character, "Collect");
                                            audioTimeColletionUpBlock = 3;
                                        }
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
                                            {
                                                if (audioTimeCollectionUp <= 0)
                                                {
                                                    AudioManager.AddAudio(selected[i] as Character, "Collect");
                                                    audioTimeCollectionUp = 5;
                                                }
                                            }
                                            else
                                            {
                                                if (audioTimeDrop <= 0)
                                                {
                                                    AudioManager.AddAudio(selected[i] as Character, "Drop");
                                                    audioTimeDrop = 5;
                                                }
                                            }
                                        }
                                }
                                else
                                    for (int i = 0; i < selected.Count; i++)//Цикл который проходится по всем выбранным юнитам
                                    {
                                        if (selected[i] as Unit != null)
                                        {
                                            if (audioTimeGo <= 0)
                                            {
                                                audioTimeGo = 15;
                                                AudioManager.AddAudio(selected[i] as Character, "Go");
                                            }

                                            (selected[i] as Unit).SetTarget(null);
                                            (selected[i] as Unit).typeTarget = TypeTarget.Set;
                                            (selected[i] as Unit).SetTargetPosition(casthit.point);//Каждый выбранный юнит обращается к выбранной позиции, которая задаётся с помощью луча 
                                        }
                                    }
                            }

                        }
                        break;
                    case TypeSeletedAction.Go:
                        for (int i = 0; i < selected.Count; i++)//Цикл который проходится по всем выбранным юнитам
                        {
                            if (selected[i] as Unit != null)
                            {
                                if (audioTimeGo <= 0)
                                {
                                    audioTimeGo = 15;
                                    AudioManager.AddAudio(selected[i] as Character, "Go");
                                }

                                (selected[i] as Unit).SetTarget(null);
                                (selected[i] as Unit).typeTarget = TypeTarget.Set;
                                (selected[i] as Unit).SetTargetPosition(casthit.point);//Каждый выбранный юнит обращается к выбранной позиции, которая задаётся с помощью луча 
                            }
                        }
                        selectedAction = TypeSeletedAction.Defualt;
                        break;
                    case TypeSeletedAction.Attack:
                        target = casthit.transform.GetComponent<IDestroyed>();
                        if (target != null)
                        {
                            for (int i = 0; i < selected.Count; i++)
                            {
                                if (selected[i] as Unit != null)
                                {
                                    (selected[i] as Unit).SetTarget(target);
                                    (selected[i] as Unit).typeTarget = TypeTarget.Set;
                                    if (audioTimeAttack <= 0)
                                    {
                                        AudioManager.AddAudio(selected[i] as Character, "Attack");
                                        audioTimeAttack = 8;
                                    }
                                    //AudioManager.AddAudio(transform, "TargetEnemy");
                                }
                            }
                        }
                        selectedAction = TypeSeletedAction.Defualt;
                        break;
                    case TypeSeletedAction.Patrol:
                        selectedAction = TypeSeletedAction.Defualt;
                        break;

                }

            }
            //    if (agent.CalculatePath(casthit.point, path))
            //    {
            //        agent.SetPath(path);
            //    }

        }
    }

    public void ShowSelectedUnits()
    {
        selected.Sort(Unit.Sort);
        unitsPanel.SetActive(true);
        for (int i = 0; i < unitsPanel.transform.childCount; i++)
            Destroy(unitsPanel.transform.GetChild(i).gameObject);
        for (int i = 0; i < selected.Count; i++)
        {
            RectTransform unit = Instantiate(Resources.Load<GameObject>("Prefabs/UI/UnitSelect")).GetComponent<RectTransform>();
            unit.SetParent(unitsPanel.transform);
            unit.anchoredPosition = new Vector2(128 + (i % 8) * 80, -64 - (i / 8) * 80);
            unit.GetChild(0).GetComponent<Image>().sprite = (selected[i] as Character).Icon;
        }
        if (selected[0] as Unit && (selected[0] as Unit).sideConflict == SideConflict.Player)
        {
            funcionalUnitPanel.SetActive(true);
        }
    }

    public void SetSelectedAction(int setSelected)
    {
        selectedAction = (TypeSeletedAction)setSelected;
    }

    //public void ClickOnDoor(Unit unit)
    //{
    //    perenosUnits = allUnits.Count;
    //    UnityEngine.SceneManagement.SceneManager.LoadScene("living room");
    //}
}