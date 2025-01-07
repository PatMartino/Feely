using Enums;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Games.CalculationResult
{
    public class CalculationResultManager : MonoBehaviour
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
        private int _questionResult;

        //private int[] _answers = new int[3];
        private int _counter;
        private int _tick;
        private bool _isCorrect;

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
            CalculationResultSignals.Instance.OnCheckIsCorrect += OnCheckIsCorrect;
            CalculationResultSignals.Instance.OnGetLevelStatus += OnGetLevelStatus;
            CalculationResultSignals.Instance.OnNextLevel += OnNextLevel;
            CalculationResultSignals.Instance.OnLevelFailed += OnLevelFailed;
            CalculationResultSignals.Instance.OnGetLevelID += OnGetLevelID;
        }

        private void QuestionGenerator()
        {
            DetermineCorrection();
            DetermineSign();
            DetermineNumbers();
            DetermineAnswers();
            CalculationResultSignals.Instance.OnSetTexts?.Invoke(_num1,_num2,_sign,_questionResult);
        }

        private void DetermineCorrection()
        {
            int randomNumber = Random.Range(0, 2);
            Debug.Log(randomNumber);
            if (randomNumber == 0)
            {
                _isCorrect = false;
            }
            else
            {
                _isCorrect = true;
            }
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
            if (!_isCorrect)
            {
                switch (_sign)
            
                {
                case "+":
                    int number = Random.Range(1, 11);
                    int signn = Random.Range(0, 2);
                    if (signn == 0)
                    {
                        _questionResult = _result + number;
                    }
                    else
                    {
                        _questionResult = _result - number;
                        if (_questionResult < 0)
                        {
                            _questionResult *= -1;
                        }
                    }
                    break;
                
                case "-":
                    int numbe = Random.Range(1, 11);
                    int signnn = Random.Range(0, 2);
                        if (signnn == 0)
                        {
                            _questionResult = _result + numbe;
                        }
                        else
                        {
                            _questionResult = _result - numbe;
                            if (_questionResult<0)
                            {
                                _questionResult *= -1;
                            }
                        }
                    break;
                case "x":
                    int numb = Random.Range(1, 6);
                    int signnnn = Random.Range(0, 2);
                        if (signnnn == 0)
                        {
                            _questionResult = _result + numb;
                        }
                        else
                        {
                            _questionResult = _result - numb;
                            if (_questionResult<0)
                            {
                                _questionResult *= -1;
                            }
                        }
                        break;
                case "÷":
                {
                        int num = Random.Range(1, 4);
                        int sign = Random.Range(0, 2);
                        if (sign == 0)
                        {
                            _questionResult = _result + num;
                        }
                        else
                        {
                            _questionResult = _result - num;
                            if (_questionResult<0)
                            {
                                _questionResult *= -1;
                            }
                        }
                }
                    break;
                
                } 
            }
            else
            {
                _questionResult = _result;
            }

        }

        private void OnCheckIsCorrect(bool num)
        {
            if (num == _isCorrect)
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
                //ES3.Save("MathFundamentalLevelID", _levelID);
                _levelStatus = LevelStatus.Complete;
                CoreGameSignals.Instance.OnStopTimer?.Invoke();
                CalculationResultSignals.Instance.OnNextLevelUI?.Invoke();
            }
            else
            {
                QuestionGenerator();
            }
        }
        
        private void LoadLevel()
        {
            //_levelID = ES3.KeyExists("MathFundamentalLevelID") ? ES3.Load<int>("MathFundamentalLevelID") : 1;
        }

        private void OnNextLevel()
        {
            CalculationResultSignals.Instance.OnGameUI?.Invoke();
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
            CalculationResultSignals.Instance.OnNextLevelUI?.Invoke();
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
            CalculationResultSignals.Instance.OnCheckIsCorrect += OnCheckIsCorrect;
            CalculationResultSignals.Instance.OnGetLevelStatus += OnGetLevelStatus;
            CalculationResultSignals.Instance.OnNextLevel += OnNextLevel;
            CalculationResultSignals.Instance.OnLevelFailed += OnLevelFailed;
            CalculationResultSignals.Instance.OnGetLevelID += OnGetLevelID;
        }

        #endregion
    }
}