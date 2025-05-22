using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.Video;


namespace SpotTheDifference
{
    public class UIController : MonoBehaviour
    {
        #region Private Variables
        [Header("UI Panels")]
        [SerializeField] private GameObject _mainScreen;
        [SerializeField] private GameObject _mapScreen;
        [SerializeField] private GameObject _modeSelectionScreen;
        [SerializeField] private GameObject _instructionsScreen;
        [SerializeField] private GameObject _gameScreen;
        [SerializeField] private GameObject _congraulationsScreen;

        [Header("History Screen UI")]
        [SerializeField] private GameObject _historyScreen;
        [SerializeField] private DataModel.HistoryScreenImageDataModel[] historyScreenImageDataModels;

        [Header("History Panel UI Panel")]
        [SerializeField] private Image historyPanelBG;
        [SerializeField] private Image historyPanelMilestoneImage;
        [SerializeField] private Image historyContentImage;
        [SerializeField] private Image historyInstructionsImage;

        [Header("VideoPlayer List")]
        [SerializeField] private List<DataModel.MounMentVideoClip> mounMentVideoClipList = new List<DataModel.MounMentVideoClip> ();

        [Header("SpawnPosition")]
        [SerializeField] private Transform _spawnPoint;

        [Header("Video Player Component Reference")]
        [SerializeField] private VideoPlayer _videoPlayerRF;

        [Header("Score UI")]
        [SerializeField] private Text _scoreTMP;

        [Header("Chances UI")]
        [SerializeField] private List<Image> _chancesImages;

        [Header("Chances Colors")]
        [SerializeField] private Color _successColor;
        [SerializeField] private Color _failureColor;

        [Header("Congraulations Text")]
        [SerializeField] private Text _congraulationsText;

        [Header("PopUp Panel")]
        [SerializeField] private GameObject _popupPanel;

        [Header("ScriptableObject Reference")]
        [SerializeField] private MonumentData _monuMentData;


        [Header("AudioClips")]
        [SerializeField] private AudioClip _gameBG;
        [SerializeField] private AudioClip _historyBG;
        [SerializeField] private AudioSource _audioSource;

        private DataModel.MounMentType mounMentType;
        private DataModel.ModeType modeType;
        private GameObject _spotDifferenceObject;
        
        [SerializeField] private DataModel.ScreenType _currentScreenType = DataModel.ScreenType.None;

        public DataModel.ScreenType ScreenType { get { return _currentScreenType; }  set { _currentScreenType = value; } }
        private int ChanceNumber = 0;
        private int score = 0;
        #endregion

        #region Singleton Instance
        public static UIController Instance { get; private set; }
        #endregion

        #region MonoBehaviour Method
        // Awake is called when the script instance is being loaded
        private void Awake()
        {          
            // Check if an instance already exists and destroy the new one if it does
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);  // Avoid duplicates
                return;
            }

            // Set this instance as the Singleton instance
            Instance = this;

            // Optional: Make this object persistent across scenes
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _audioSource.GetComponent<AudioSource>().clip = _historyBG;
            _audioSource.GetComponent<AudioSource>().Play();

            _currentScreenType = DataModel.ScreenType.MainScreen;
        }
        #endregion

        #region Private Methods
        public void PlayButtonClick()
        {
            _mainScreen.SetActive(false);
            _mapScreen.SetActive(true);

            _currentScreenType = DataModel.ScreenType.MonumentSelectionScreen;
        }

        public void MonumentButtonClick(int MonumnentNumber)
        {
            Debug.Log("MonumnentNumber" + MonumnentNumber);

            DataModel.MounMentType _monuMentType = (DataModel.MounMentType)MonumnentNumber;

            Debug.Log("MonuMent Type " + _monuMentType);

            mounMentType = _monuMentType;

            _historyScreen.SetActive(true);
            _mapScreen.SetActive(false);

            int _index = Array.FindIndex(historyScreenImageDataModels, x => x.mounMentType == mounMentType);

            Debug.Log("Index" + _index);

            historyPanelBG.sprite = historyScreenImageDataModels[_index].backGroundImage;
            historyPanelMilestoneImage.sprite = historyScreenImageDataModels[_index].boardImage;
            historyContentImage.sprite = historyScreenImageDataModels[_index].contentImage;
            historyInstructionsImage.sprite = historyScreenImageDataModels[_index].boardInstructionsImage;

            _currentScreenType = DataModel.ScreenType.HistoryScreen;
        }

        public void HistoyNextButtonClick()
        {
            _modeSelectionScreen.SetActive(true);
            _historyScreen.SetActive(false);

            _currentScreenType = DataModel.ScreenType.LevelSelectionScreen;
        }

        public void ModeSelection(int _modeValue)
        {
            DataModel.ModeType _modeType = (DataModel.ModeType)_modeValue;
            modeType = _modeType;

            if (_monuMentData.monumentModels.Find(x => x._monumentType == mounMentType)._modeClasses.Find(x => x._modeType == modeType).isPlayed == false)
            {
                _modeSelectionScreen.SetActive(false);
                _instructionsScreen.SetActive(true);

                _currentScreenType = DataModel.ScreenType.InstructionScreen;
            }
        }

        public void InstructionNextButtonClick()
        {
            int monuMentIndex = mounMentVideoClipList.FindIndex(x=>x._mounmentType == mounMentType);
            int modeIndex = mounMentVideoClipList[monuMentIndex]._modeVideoModels.FindIndex(x=> x._modeType == modeType);

            _videoPlayerRF.clip = mounMentVideoClipList[monuMentIndex]._modeVideoModels[modeIndex]._videoClip;

            if(_spotDifferenceObject == null)
            {
                _spotDifferenceObject = Instantiate(mounMentVideoClipList[monuMentIndex]._modeVideoModels[modeIndex]._spotDifferenceObject, _spawnPoint);
            }

            _instructionsScreen.SetActive(false);
            _gameScreen.SetActive(true);
            _audioSource.GetComponent<AudioSource>().clip = _gameBG;
            _audioSource.GetComponent<AudioSource>().Play();
            _currentScreenType = DataModel.ScreenType.GameScreen;
        }

        public void CongraulationsScreen()
        {
            Destroy(_spotDifferenceObject);
            _spotDifferenceObject = null;

            _congraulationsText.text = "ON COMPLETING THE " + mounMentType.ToString().ToUpper() + " EXPOLARATION!";

            _monuMentData.monumentModels.Find(x => x._monumentType == mounMentType)._modeClasses.Find(x => x._modeType == modeType)._score = score;
            //_monuMentData.monumentModels.Find(x => x._monumentType == mounMentType)._modeClasses.Find(x => x._modeType == modeType).isPlayed = true;

            var monument = _monuMentData.monumentModels.Find(x => x._monumentType == mounMentType);

            // Check if all items in _modeClasses have isPlayed set to true
            if (monument._modeClasses.All(item => item.isPlayed))
            {
                foreach (var item in monument._modeClasses)
                {
                    // Add the score of each item to the total score
                    monument._totalScore += item._score;
                }
            }

            _gameScreen.SetActive(false);
            _congraulationsScreen.SetActive(true);
            _currentScreenType = DataModel.ScreenType.None;
        }

        public void CongraulationsNextButtonClick()
        {
            _congraulationsScreen.SetActive(false);
            _modeSelectionScreen.SetActive(true);
            _audioSource.GetComponent<AudioSource>().clip = _historyBG;
            _audioSource.GetComponent<AudioSource>().Play();

            ResetMethod();
        }

        private void ResetMethod()
        {
            _scoreTMP.text = string.Empty;
            ChanceNumber = 0;
            score = 0;

            for(int i=0;i<_chancesImages.Count;i++)
            {
                _chancesImages[i].color = Color.white;
            }
            _currentScreenType = DataModel.ScreenType.None;
        }

        public void Chances()
        {
            Debug.Log("Chances UI");
            ChanceNumber++;
            Debug.Log("Chance Number" + ChanceNumber);

            if (ChanceNumber < 6)
            {
                for (int i = 0; i < ChanceNumber; i++)
                {
                    _chancesImages[i].color = _successColor;
                }
            }

            if(ChanceNumber == 5)
            {
                StartCoroutine("DelayTheGameScreen");
            }
        }

        private IEnumerator DelayTheGameScreen()
        {
            yield return new WaitForSeconds(.5f);
            Destroy(_spotDifferenceObject);
            _spotDifferenceObject = null;
            ResetMethod();

            _audioSource.clip = _historyBG;
            //_modeSelectionScreen.SetActive(true);
            _popupPanel.SetActive(true);
            _gameScreen.SetActive(false);

            _audioSource.GetComponent<AudioSource>().clip = _historyBG;
            _audioSource.Play();

            _currentScreenType = DataModel.ScreenType.GameScreen;
        }

        public void UpdateScoreUI(int _score)
        {
            score = _score;
            _scoreTMP.text = _score.ToString(); 
        }

        public void PopUpPlayButtonClick()
        {
            _popupPanel.SetActive(false);
            _modeSelectionScreen.SetActive(true);
            ResetMethod();
        }

        public void PopUpQuitButtonClick()
        {
            _popupPanel.SetActive(false);
            _mainScreen.SetActive(true);
            ResetMethod();
        }

        public void ExitButtonClick()
        {
            Application.Quit();
        }

        public void HomeButtonClick()
        {
             DeactivateAllPanels();
            _mainScreen.SetActive(true);
        }

        private void DeactivateAllPanels()
        {
            _mainScreen.SetActive(false);
            _mapScreen.SetActive(false);
            _modeSelectionScreen.SetActive(false );
            _gameScreen.SetActive(false);
            _historyScreen.SetActive(false);
            _instructionsScreen.SetActive(false);
        }

        #endregion


        public void ExitSpotTheDifferneceGame()
        {
            _gameScreen.SetActive(false);
            _modeSelectionScreen.SetActive(true);
        }
    }
}

