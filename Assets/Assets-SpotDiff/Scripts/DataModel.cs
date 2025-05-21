using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


namespace SpotTheDifference
{
    public static class DataModel
    {
        [System.Serializable]
        public enum MounMentType
        {
            //
            Amelia,
            Indira,
            Jkr,
            Marie,
            MaryKom,
            
            //# new above
            
            TajMahal,
            Petra,
            Colosseum,
            MachuPichu,
            GreatWallOfChina,
            ChichenItza,
            ChristTheRedeemer
        }

        #region HistoryScreenImageData

        [System.Serializable]
        public class HistoryScreenImageDataModel
        {
            public Sprite backGroundImage;
            public Sprite contentImage;
            public Sprite boardImage;
            public Sprite boardInstructionsImage;
            public MounMentType mounMentType;
        }

        [System.Serializable]
        public enum ModeType
        {
            Easy,
            Medium,
            Hard
        }
        #endregion

        [System.Serializable]
        public class MounMentVideoClip
        {
            public MounMentType _mounmentType;
            public List<ModeVideoModel> _modeVideoModels;
        }

        [System.Serializable]
        public class ModeVideoModel
        {
            public ModeType _modeType;
            public VideoClip _videoClip;
            public GameObject _spotDifferenceObject;
        }

        [System.Serializable]
        public enum ScreenType
        {
            None,
            GameScreen,
            MainScreen,
            MonumentSelectionScreen,
            LevelSelectionScreen,
            HistoryScreen,
            InstructionScreen,
            CongraulationsScreen
        }
    }
}
