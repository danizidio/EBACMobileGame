using UnityEngine;

public class Cronometer : MonoBehaviour
{
	public delegate void _onBeatGame();
	public static _onBeatGame OnBeatGame;

	System.Action OnSetTimer;

	public static System.Action<bool> OnTimerRunning;

	public static System.Action OnShowTimer;

	bool _isTicTimer;


	[SerializeField] bool _isRegressiveTimer;

	[SerializeField] float _progressiveTimer;
	float _regressiveTimer;

	[SerializeField] float _progressiveTimer_Minutes;
	[SerializeField] float _progressiveTimer_Seconds;
	[SerializeField] float _progressiveTimer_Miliseconds;
	float _progressiveTimer_Microseconds;

	[SerializeField] float _regressiveTimer_Minutes;
	float _regressiveTimer_Seconds;
	float _regressiveTimer_Miliseconds;
	float _regressiveTimer_Microseconds;

	private void Start()
	{
		OnTimerRunning = TimerRunning;
		OnShowTimer = ShowCurrentTimer;

		if (_isRegressiveTimer)
		{
			OnSetTimer = RegressiveTimer;
		}
		else
		{
			OnSetTimer = ProgressiveTimer;
		}
	}

	void Update()
	{
		OnSetTimer();
	}

	void TimerRunning(bool b)
	{
		_isTicTimer = b;
	}

	void ProgressiveTimer()
	{
		if (_isTicTimer)
		{
			_progressiveTimer += Time.deltaTime;
			_progressiveTimer_Microseconds = Mathf.RoundToInt(_progressiveTimer * 600);

			if (_progressiveTimer_Microseconds > 59)
			{
				_progressiveTimer_Microseconds = 0;
				_progressiveTimer_Miliseconds++;
			}
			if (_progressiveTimer_Miliseconds > 59)
			{
				_progressiveTimer_Miliseconds = 0;
				_progressiveTimer_Seconds++;
			}
			if (_progressiveTimer_Seconds > 59)
			{
				_progressiveTimer_Seconds = 0;
				_progressiveTimer_Minutes++;
			}
		}
	}

	void RegressiveTimer()
	{
		if (_isTicTimer)
		{
			_regressiveTimer -= Time.deltaTime;
			_regressiveTimer_Microseconds = Mathf.RoundToInt(_regressiveTimer * 600);

			if (_regressiveTimer_Microseconds <= 0)
			{
				_regressiveTimer_Microseconds = 59;
				_regressiveTimer_Miliseconds--;
			}
			if (_regressiveTimer_Miliseconds <= 0)
			{
				_regressiveTimer_Miliseconds = 59;
				_regressiveTimer_Seconds--;
			}
			if (_regressiveTimer_Seconds <= 0)
			{
				_regressiveTimer_Seconds = 59;
				_regressiveTimer_Minutes--;
			}
			if (_regressiveTimer_Minutes < 0)
			{
				//_txt.text = string.Format("{0:00}:{1:00}", 0, 0);

				GameManager.OnNextGameState.Invoke(GamePlayStates.GAMEOVER);
			}
			else
			{
				//_txt.text = string.Format("{0:00}:{1:00}", _regressiveTimer_Minutes, _regressiveTimer_Seconds);
			}
		}
	}

	void ShowCurrentTimer()
	{
		GameObject _txt = GameObject.FindGameObjectWithTag("FinishTimer");

		if (_txt != null)
			_txt.GetComponent<TMPro.TMP_Text>().text = string.Format("{0:00}:{1:00}:{2:00}", _progressiveTimer_Minutes, _progressiveTimer_Seconds, _progressiveTimer_Miliseconds);
	}

	void BeatGame()
	{
		_isTicTimer = false;

		GameObject _txt = GameObject.FindGameObjectWithTag("FinishTimer");

		if (_txt != null)
			_txt.GetComponent<TMPro.TMP_Text>().text = string.Format("{0:00}:{1:00}:{2:00}", _progressiveTimer_Minutes, _progressiveTimer_Seconds, _progressiveTimer_Miliseconds);

	}

	private void OnEnable()
	{
		OnBeatGame += BeatGame;
	}

	private void OnDisable()
	{
		OnBeatGame -= BeatGame;
	}
}
