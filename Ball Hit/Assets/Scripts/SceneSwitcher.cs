using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    //public static int x;
    public bool isNightMode;
    public QualitySettingsManager qualitySettingsManager;

    public static SceneSwitcher sceneSwitcher;
    public static int pipeNumber;
    public static int randomGenerator = 1;
    List<int> generatorIndexList;
    public static int gemGenerationProbability = 100;
    public static bool isObstacleSmashed;
    public static int smashedObstaclesCount;
    public Text smashedObstaclesCountText;

    public Player player;
    public PipeSystem pipeSystem;
    private Pipe pipe;
    BallAndRoadChoicesManager ballAndRoadChoicesManager;

    public int currentSceneInt = 0;

    public Text currentSceneIntText;
    public Text numberOfScenesText;

    GameObject pipePrefab;
    Material _material;

    public Texture texture;

    public List<SceneProperties> sceneProp = new List<SceneProperties>();

    [Header("Mode Variables")]
    public Toggle modeToggle;
    public Sprite moonSprite;
    public Sprite sunSprite;

    public Texture nightModeTexture;
    public Texture[] whiteModeTexture;

    public Color lightBlack;

    public Image[] images;
    public Text[] texts;
    public Button[] buttons;

    public Image[] inversedImages;
    public Text[] inversedTexts;
    public Button[] inversedButtons;


    [Header("Color Stuff")]
    public Material[] materials;
    public Color[] colors;
    public float speedFactor = 1;
    public float timeToChange = 30f;

    Color _color;
    int colorIndex = 0;

    [Header("Loading Panel")]
    public float timefLoading = 4f;
    public GameObject loadingPanel;

    [System.Serializable]
    public class SceneProperties
    {
        public int _I;
        public int _num;
        public Vector2[] _newVect;
    }

    void MakeGeneratorIndexList()
    {
        generatorIndexList = new List<int>();
        for (int i = 1; i < pipe.generators.Length; i++)
        {
            generatorIndexList.Add(i);
        }
    }

    public int NonRepeatedRandomIndex()
    {
        if (generatorIndexList.Count <= 0 || generatorIndexList == null)
        {
            MakeGeneratorIndexList();
        }

        int pickedListIndex = Random.Range(0, generatorIndexList.Count);
        int pickedGeneratorIndex = generatorIndexList[pickedListIndex];
        generatorIndexList.RemoveAt(pickedListIndex);
        return pickedGeneratorIndex;
    }

    public void UpdateSmashedObstaclesUI()
    {
        smashedObstaclesCountText.text = smashedObstaclesCount.ToString();
    }

    void Awake()
    {
        Invoke("TurnOffLoadingPanel", timefLoading);

        sceneSwitcher = this;

        SetVariables();

        if (DataSaveManager.IsDataExist("NM"))
        {
            modeToggle.isOn = DataSaveManager.LoadBoolean("NM");
        }
        ToggleTheMode();

        MakeGeneratorIndexList();
    }

    void Start()
    {
        InvokeRepeating("ChangeColorOverTime", timeToChange, timeToChange);
    }

    void SetVariables()
    {
        pipePrefab = pipeSystem.pipePrefab.gameObject;
        pipe = pipePrefab.GetComponent<Pipe>();
        _material = pipePrefab.GetComponent<MeshRenderer>().sharedMaterial;

        ballAndRoadChoicesManager = gameObject.GetComponent<BallAndRoadChoicesManager>();
    }

    public void ToggleTheMode()
    {
        isNightMode = !modeToggle.isOn;

        if (isNightMode)
        {
            texture = nightModeTexture;

            _material.mainTexture = texture;
            _material.SetFloat("_Metallic", 0.5f);
            _material.SetFloat("_Glossiness", 0.4f);

            foreach (Image image in images)
            {
                ChangeColorExceptAlpha(image, Color.white);
            }

            foreach (Text text in texts)
            {
                text.color = Color.white;
            }

            foreach (Button button in buttons)
            {
                ColorBlock buttonColors = button.colors;

                buttonColors.normalColor = Color.white;
                buttonColors.highlightedColor = Color.white;

                button.colors = buttonColors;
            }

            // The Opposite Color.

            foreach (Image image in inversedImages)
            {
                ChangeColorExceptAlpha(image, lightBlack);
            }

            foreach (Text text in inversedTexts)
            {
                text.color = lightBlack;
            }

            foreach (Button button in inversedButtons)
            {
                ColorBlock buttonColors = button.colors;

                buttonColors.normalColor = lightBlack;
                buttonColors.highlightedColor = lightBlack;

                button.colors = buttonColors;
            }



        }
        else
        {
            texture = whiteModeTexture[0];

            _material.mainTexture = texture;
            _material.color = Color.white;
            _material.SetFloat("_Metallic", 0.2f);
            _material.SetFloat("_Glossiness", 0.6f);

            foreach (Image image in images)
            {
                ChangeColorExceptAlpha(image, lightBlack);
            }

            foreach (Text text in texts)
            {
                text.color = lightBlack;
            }

            foreach (Button button in buttons)
            {
                ColorBlock buttonColors = button.colors;

                buttonColors.normalColor = lightBlack;
                buttonColors.highlightedColor = lightBlack;

                button.colors = buttonColors;
            }

            // The Opposite Color.

            foreach (Image image in inversedImages)
            {
                ChangeColorExceptAlpha(image, Color.white);
            }

            foreach (Text text in inversedTexts)
            {
                text.color = Color.white;
            }
            
            foreach (Button button in inversedButtons)
            {
                ColorBlock buttonColors = button.colors;

                buttonColors.normalColor = Color.white;
                buttonColors.highlightedColor = Color.white;

                button.colors = buttonColors;
            }
        }

        if (qualitySettingsManager.bloomEffectToggle.isOn)
        {
            qualitySettingsManager.BloomEffectToggle(!modeToggle.isOn);
        }

        ballAndRoadChoicesManager.UpdateButtonProperties();

        SetNightModeToggleImage();

        DataSaveManager.SaveBoolean("NM", modeToggle.isOn);
    }

    void ChangeColorExceptAlpha(Image image, Color wantedColor)
    {
        Color currentItemColor;

        currentItemColor = image.color;

        currentItemColor.r = wantedColor.r;
        currentItemColor.g = wantedColor.g;
        currentItemColor.b = wantedColor.b;

        image.color = currentItemColor;
    }

    void SetNightModeToggleImage()
    {
        if (isNightMode)
        {
            modeToggle.image.sprite = sunSprite;
        }
        else
        {
            modeToggle.image.sprite = moonSprite;
        }
    }

    void ReloadScene()
    {
        pipeSystem.InstantiatePipes();
        player.StartBackgroundScene();
    }

    public void SceneSelector(int buttonInt)
    {
        if(buttonInt == 1)
        {
            if(currentSceneInt < sceneProp.Count - 1)
            {
                currentSceneInt++;
                ApplyTheChanges(currentSceneInt);
            }

        }

        if (buttonInt == 0)
        {
            if (currentSceneInt == 0)
            {
                return;
            }
            currentSceneInt--;

            ApplyTheChanges(currentSceneInt);
        }
    }

    public void ApplyTheChanges(int sceneInt)
    {
        currentSceneInt = sceneInt;

        _material.mainTexture = texture;

        pipe.I = sceneProp[sceneInt]._I;
        pipe.num = sceneProp[sceneInt]._num;
        pipe.newVect = sceneProp[sceneInt]._newVect;

        Invoke("ReloadScene", 0.05f);

        UpdateSceneIntTextUI();
    }

    void UpdateSceneIntTextUI()
    {
        currentSceneIntText.text = string.Format("{0:00}", currentSceneInt + 1);

        numberOfScenesText.text = string.Format("{0:00}", sceneProp.Count);
    }

    void ChangeColorOverTime()
    {
        colorIndex = Random.Range(0, colors.Length);

        for (int i = 0; i < materials.Length; i++)
        {
            if (!isNightMode)
            {
                if (i != 0)
                {
                    ChangeColorOfMaterial(materials[i]);
                }

                texture = whiteModeTexture[colorIndex];
                _material.mainTexture = texture;
            }
            else
            {
            ChangeColorOfMaterial(materials[i]);
            }
        }
    }

    void ChangeColorOfMaterial(Material _material)
    {
        StartCoroutine(ChangeColorR(_material));
        StartCoroutine(ChangeColorG(_material));
        StartCoroutine(ChangeColorB(_material));

        _color.a = 1;
        _material.color = _color;
    }

    IEnumerator ChangeColorR(Material _material)
    {
        if (_material.color.r < colors[colorIndex].r)
        {
            while (_material.color.r <= colors[colorIndex].r)
            {
                _color.r += Time.deltaTime * speedFactor;
                _material.color = _color;
                yield return null;
            }
            _color.r = colors[colorIndex].r;
            _material.color = _color;
        }
        else
        {
            while (_material.color.r >= colors[colorIndex].r)
            {
                _color.r -= Time.deltaTime * speedFactor;
                _material.color = _color;
                yield return null;
            }
            _color.r = colors[colorIndex].r;
            _material.color = _color;
        }

    }

    IEnumerator ChangeColorG(Material _material)
    {
        if (_material.color.g < colors[colorIndex].g)
        {
            while (_material.color.g <= colors[colorIndex].g)
            {
                _color.g += Time.deltaTime * speedFactor;
                _material.color = _color;
                yield return null;
            }
            _color.g = colors[colorIndex].g;
            _material.color = _color;
        }
        else
        {
            while (_material.color.g >= colors[colorIndex].g)
            {
                _color.g -= Time.deltaTime * speedFactor;
                _material.color = _color;
                yield return null;
            }
            _color.g = colors[colorIndex].g;
            _material.color = _color;
        }

    }

    IEnumerator ChangeColorB(Material _material)
    {
        if (_material.color.b < colors[colorIndex].b)
        {
            while (_material.color.b <= colors[colorIndex].b)
            {
                _color.b += Time.deltaTime * speedFactor;
                _material.color = _color;
                yield return null;
            }
            _color.b = colors[colorIndex].b;
            _material.color = _color;
        }
        else
        {
            while (_material.color.b >= colors[colorIndex].b)
            {
                _color.b -= Time.deltaTime * speedFactor;
                _material.color = _color;
                yield return null;
            }
            _color.b = colors[colorIndex].b;
            _material.color = _color;
        }

    }

    void TurnOffLoadingPanel()
    {
        loadingPanel.SetActive(false);
    }

    //void AddNewSceneProp()
    //{
    //    sceneProp.Clear();
    //    for (int i = 0; i < 100; i++)
    //    {
    //        sceneProp.Add(new SceneProperties());

    //        sceneProp[i]._texture = texture;
    //        sceneProp[i]._I = _i;
    //        sceneProp[i]._num = _nu;

    //        int vLength = 4;
    //        sceneProp[i]._newVect = new Vector2[vLength];

    //        int zero = 0;
    //        int one = 0;
    //        int two = 0;
    //        int three = 0;
    //        int four = 0;
    //        int five = 0;
    //        int six = 0;
    //        int seven = 0;

    //        for (int v = 0; v < vLength; v++)
    //        {
    //            if(Random.Range(0, 100) < probability)
    //            {
    //                zero = Random.Range(-1, 2);
    //            }

    //            if (Random.Range(0, 100) < probability)
    //            {
    //                one = Random.Range(-1, 2);
    //            }

    //            if (Random.Range(0, 100) < probability)
    //            {
    //                two = Random.Range(-1, 2);
    //            }

    //            if (Random.Range(0, 100) < probability)
    //            {
    //                three = Random.Range(-1, 2);
    //            }

    //            if (Random.Range(0, 100) < probability)
    //            {
    //                four = Random.Range(-1, 2);
    //            }

    //            if (Random.Range(0, 100) < probability)
    //            {
    //                five = Random.Range(-1, 2);
    //            }

    //            if (Random.Range(0, 100) < probability)
    //            {
    //                six = Random.Range(-1, 2);
    //            }

    //            if (Random.Range(0, 100) < probability)
    //            {
    //                seven = Random.Range(-1, 2);
    //            }


    //            sceneProp[i]._newVect[0].Set(zero, one);
    //            sceneProp[i]._newVect[1].Set(two, three);
    //            sceneProp[i]._newVect[2].Set(four, five);
    //            sceneProp[i]._newVect[3].Set(six, seven);
    //        }
    //    }
    //}
}