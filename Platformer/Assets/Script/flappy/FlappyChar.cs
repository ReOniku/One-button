using UnityEngine;
using UnityEngine.UI;

public class FlappyChar : Ticker
{
    public static FlappyChar instance;

    public int defaultSpeedLevel = 3;
    private int _speedLevel;
    public float speedLevelFactor = 1;
    private float _moveSpeed;

    public float gravity = 4;
    private float _dropSpeed;
    public float jumpPower = 4;

    public float energyMax;
    public float energyPerJump;

    public float minJumpSpeedRequire = 1;
    private float _energy;

    public Slider energyBar;
    public Text speedText;
    public Text scoreText;
    public int score;

    public Transform spawnPoint;
    private bool _gravityIsReversed;

    private void Start()
    {
        instance = this;
    }

    public void OnStartGame()
    {
        _energy = energyMax;
        _dropSpeed = 0;
        score = 0;
        transform.position = spawnPoint.position;

        _speedLevel = defaultSpeedLevel;
        _gravityIsReversed = false;
        SetMoveSpeed();
    }

    public void OnEndGame()
    {
        _dropSpeed = 0;
        _energy = 0;
        _moveSpeed = 0;
    }

    void SetMoveSpeed()
    {
        if (_speedLevel < 0)
        {
            _speedLevel = 0;
        }
        _moveSpeed = _speedLevel * speedLevelFactor;
    }

    protected override void Update()
    {
        base.Update();

        if (GameSystem.instance.state == GameSystem.GameState.Playing)
        {
            ReadInput();
            Move();
        }

        UpdateView();
    }

    void UpdateView()
    {
        // energyBar.value = _energy / energyMax;
        speedText.text = Mathf.FloorToInt(_moveSpeed) + "";

        var distance = transform.position.x - spawnPoint.position.x;
        scoreText.text = "" + (score + Mathf.FloorToInt(distance));
    }

    private void ReadInput()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            TryJump();
        }
    }

    public void TryJump()
    {
        if (_gravityIsReversed)
        {
            if (_dropSpeed > minJumpSpeedRequire)
            {
                return;
            }
        }
        else
        {
            if (_dropSpeed <= -minJumpSpeedRequire)
            {
                return;
            }
        }

        //if (_energy < energyPerJump)
        //{
        //    return;
        //}
        //
        //_energy = _energy - energyPerJump;
        Jump();
    }

    private void Move()
    {
        Drop();
        transform.position += _dropSpeed * Vector3.down * Time.deltaTime + _moveSpeed * Vector3.right * Time.deltaTime;
    }

    void Drop()
    {
        if (_gravityIsReversed)
        {
            _dropSpeed -= gravity * Time.deltaTime;
        }
        else
        {
            _dropSpeed += gravity * Time.deltaTime;
        }
    }

    void Jump()
    {
        if (_gravityIsReversed)
        {
            _dropSpeed = jumpPower;
        }
        else
        {
            _dropSpeed = -jumpPower;
        }
    }

    protected override void Tick()
    {
        if (GameSystem.instance.state != GameSystem.GameState.Playing)
        {
            return;
        }

        _energy += 1;
    }

    public void OnCollect(FlappyCollectible flappyCollectible)
    {
        switch (flappyCollectible.category)
        {
            case FlappyCollectible.Category.SpeedMultiple:
                _speedLevel *= flappyCollectible.value;
                SetMoveSpeed();
                break;
            case FlappyCollectible.Category.SpeedUp:
                _speedLevel += flappyCollectible.value;
                SetMoveSpeed();
                break;
            case FlappyCollectible.Category.SpeedDown:
                _speedLevel -= flappyCollectible.value;
                SetMoveSpeed();
                break;
            case FlappyCollectible.Category.EnergyBonus:
                //_energy += flappyCollectible.value;
                //if (_energy > energyMax)
                //    _energy = energyMax;
                score += flappyCollectible.value;
                break;

            case FlappyCollectible.Category.BreakableWall:
                if (_speedLevel > flappyCollectible.value)
                {
                    _speedLevel -= flappyCollectible.value;
                    flappyCollectible.WallBreak();
                }
                else
                {
                    _speedLevel = 0;
                    SetMoveSpeed();
                }
                break;
            case FlappyCollectible.Category.Portal:
                transform.position = flappyCollectible.target.position;
                break;
            case FlappyCollectible.Category.Push:
                _dropSpeed = -flappyCollectible.value;
                break;
            case FlappyCollectible.Category.GravityReverse:
                _gravityIsReversed = !_gravityIsReversed;
                break;

        }
    }
}
