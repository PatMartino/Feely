using Enums;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.MathFundamental
{
    public class MathFundamentalManager : MonoBehaviour
    {
        #region Private Field

        private int _levelID;
        private int _section;
        private float _timeDuration;

        private int _num1;
        private int _num2;
        private readonly string[] _operationList = {"+","-","x","÷"};
        private string _sign;

        private int _result;
        private int _correctAnswer;
        private int[] _answers = new int[3];
        private int _counter;
        private int _tick;

        private LevelStatus _levelStatus;
        
        #endregion

        #region OnEnable, Start, OnDisable

        private void OnEnable()
        {
            SubscribeEvents();
            LoadLevel();
        }

        private void Start()
        {
            QuestionGenerator();
            CoreGameSignals.Instance.OnStartTimer?.Invoke(60f);
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        #region Function

        private void SubscribeEvents()
        {
            MathFundamentalSignals.Instance.OnCheckIsCorrect += OnCheckIsCorrect;
            MathFundamentalSignals.Instance.OnGetLevelStatus += OnGetLevelStatus;
            MathFundamentalSignals.Instance.OnNextLevel += OnNextLevel;
            MathFundamentalSignals.Instance.OnLevelFailed += OnLevelFailed;
            MathFundamentalSignals.Instance.OnGetLevelID += OnGetLevelID;
        }

        private void QuestionGenerator()
        {
            DetermineSign();
            DetermineNumbers();
            DetermineAnswers();
            MathFundamentalSignals.Instance.OnSetTexts?.Invoke(_num1,_num2,_sign,_answers[0],_answers[1],_answers[2], _correctAnswer);
        }

        private void DetermineSign()
        {
            int num = Random.Range(0, 4);
            _sign = _operationList[num];
        }

        private void DetermineNumbers()
        {
            switch (_sign)
            {
                case "+":
                    _num1 = Random.Range(1, 100);
                    _num2 = Random.Range(1, 100);
                    _result = _num1 + _num2;
                    break;
                case "-":
                    _num1 = Random.Range(1, 100);
                    _num2 = Random.Range(1, _num1);
                    _result = _num1 - _num2;
                    break;
                case "x":
                    _num1 = Random.Range(1, 15);
                    _num2 = Random.Range(1, 15);
                    _result = _num1 * _num2;
                    break;
                case "÷":
                    _num1 = Random.Range(2, 15);
                    _num2 = Random.Range(1, 15);
                    _num1 *= _num2;
                    _result = _num1 / _num2;
                    break;
            }
        }

        private void DetermineAnswers()
        {
            _correctAnswer = Random.Range(0, 3);
            _counter = _correctAnswer;
            switch (_sign)
            {
                case "+":
                    _answers[_counter] = _result;
                    for (int i = 0; i < 2; i++)
                    {
                        IncreaseNum();
                        int num = Random.Range(1, 21);
                        int sign = Random.Range(0, 2);
                        if (sign == 0)
                        {
                            _answers[_counter] = _result + num;
                        }
                        else
                        {
                            _answers[_counter] = _result - num;
                            if (_answers[_counter]<0)
                            {
                                _answers[_counter] *= -1;
                            }
                        }
                    }
                    break;
                case "-":
                    _answers[_counter] = _result;
                    for (int i = 0; i < 2; i++)
                    {
                        IncreaseNum();
                        int num = Random.Range(1, 21);
                        int sign = Random.Range(0, 2);
                        if (sign == 0)
                        {
                            _answers[_counter] = _result + num;
                        }
                        else
                        {
                            _answers[_counter] = _result - num;
                            if (_answers[_counter]<0)
                            {
                                _answers[_counter] *= -1;
                            }
                        }
                    }
                    break;
                case "x":
                    _answers[_counter] = _result;
                    for (int i = 0; i < 2; i++)
                    {
                        IncreaseNum();
                        int num = Random.Range(1, 21);
                        int sign = Random.Range(0, 2);
                        if (sign == 0)
                        {
                            _answers[_counter] = _result + num;
                        }
                        else
                        {
                            _answers[_counter] = _result - num;
                            if (_answers[_counter]<0)
                            {
                                _answers[_counter] *= -1;
                            }
                        }
                    }
                    break;
                case "÷":
                    _answers[_counter] = _result;
                    for (int i = 0; i < 2; i++)
                    {
                        IncreaseNum();
                        int num = Random.Range(1, 4);
                        int sign = Random.Range(0, 2);
                        if (sign == 0)
                        {
                            _answers[_counter] = _result + num;
                        }
                        else
                        {
                            _answers[_counter] = _result - num;
                            if (_answers[_counter]<0)
                            {
                                _answers[_counter] *= -1;
                            }
                        }
                    }
                    break;
            }
            
        }

        private void IncreaseNum()
        {
            _counter++;
            if (_counter >2)
            {
                _counter = 0;
            }
        }

        private void OnCheckIsCorrect(int num)
        {
            if (num == _correctAnswer)
            {
                _section++;
                Debug.Log(_section);
            }
            IsLevelComplete();
        }

        private void IsLevelComplete()
        {
            if (_section >7)
            {
                _section = 0;
                _levelID++;
                ES3.Save("MathFundamentalLevelID", _levelID);
                _levelStatus = LevelStatus.Complete;
                CoreGameSignals.Instance.OnStopTimer?.Invoke();
                MathFundamentalSignals.Instance.OnNextLevelUI?.Invoke();
            }
            else
            {
                QuestionGenerator();
            }
        }
        
        private void LoadLevel()
        {
            _levelID = ES3.KeyExists("MathFundamentalLevelID") ? ES3.Load<int>("MathFundamentalLevelID") : 1;
        }

        private void OnNextLevel()
        {
            MathFundamentalSignals.Instance.OnGameUI?.Invoke();
            if (_levelID<10)
            {
                float time = 60 - _levelID*5;
                CoreGameSignals.Instance.OnStartTimer?.Invoke(time);
            }
            else
            {
                CoreGameSignals.Instance.OnStartTimer?.Invoke(12f);
            }
            
            QuestionGenerator(); 
        }

        private void OnLevelFailed()
        {
            _section = 0;
            _levelStatus = LevelStatus.Failed;
            CoreGameSignals.Instance.OnStopTimer?.Invoke();
            MathFundamentalSignals.Instance.OnNextLevelUI?.Invoke();
        }

        private LevelStatus OnGetLevelStatus()
        {
            return _levelStatus;
        }

        private int OnGetLevelID()
        {
            return _levelID;
        }
        
        private void UnSubscribeEvents()
        {
            MathFundamentalSignals.Instance.OnCheckIsCorrect -= OnCheckIsCorrect;
            MathFundamentalSignals.Instance.OnGetLevelStatus -= OnGetLevelStatus;
            MathFundamentalSignals.Instance.OnNextLevel -= OnNextLevel;
            MathFundamentalSignals.Instance.OnLevelFailed -= OnLevelFailed;
            MathFundamentalSignals.Instance.OnGetLevelID += OnGetLevelID;
        }

        #endregion
    }
}