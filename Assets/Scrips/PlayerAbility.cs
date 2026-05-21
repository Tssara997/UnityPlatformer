using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilty : MonoBehaviour
{
    public int groundLayer;
    
    private AbilityLogic _ability;
    private int _maxNumOfClones;
    private float _cooldown;

    void Awake()
    {
        _maxNumOfClones = 1;
        _cooldown = 1f;
        _ability = new AbilityLogic(_cooldown, _maxNumOfClones, groundLayer);
    }

    public void OnDeath(InputValue value)
    {
        _ability.TryUse(gameObject);
    }

    public void Update()
    {
        _ability.Tick(Time.deltaTime);
    }
}

public class AbilityLogic
{
    private float _cooldown;
    private float _timer;
    private int _numOfClones;
    private int _maxNumOfClones;
    private GameObject _platform;
    private int _groundLayer;

    public AbilityLogic(float cooldown, int maxNumOfClones, int groundLayer)
    {
        _cooldown = cooldown;
        _maxNumOfClones = maxNumOfClones;
        _groundLayer = groundLayer;
        _numOfClones = 0;
        _timer = 0;
    }

    public void Tick(float deltaTime)
    {
        if (_timer < _cooldown) _timer += deltaTime;
    }

    public bool TryUse(GameObject player)
    {
        if (_timer <= _cooldown) return false;

        if (_numOfClones < _maxNumOfClones)
            _platform = CreateClone(player);
        MoveClones(player);
        
        _timer = 0;
        return true;
    }

    private GameObject CreateClone(GameObject player)
    {
        GameObject platform = new GameObject("Platforma", typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(SpriteRenderer));
        platform.layer = _groundLayer;
        platform.transform.localScale = player.transform.localScale;
        platform.GetComponent<BoxCollider2D>().size = player.GetComponent<BoxCollider2D>().size;
        platform.GetComponent<BoxCollider2D>().offset = player.GetComponent<BoxCollider2D>().offset;
        platform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        platform.GetComponent<SpriteRenderer>().sprite = player.GetComponent<SpriteRenderer>().sprite;
        platform.GetComponent<SpriteRenderer>().color = Color.black;
        platform.GetComponent<SpriteRenderer>().size = player.GetComponent<SpriteRenderer>().size;
        platform.GetComponent<SpriteRenderer>().sortingLayerName = "Ground";
        
        _numOfClones++;
        
        return platform;
    }

    private void MoveClones(GameObject player)
    {
        _platform.transform.position = player.transform.position;
        player.transform.position = new Vector3(0, 0, 0);
    }
    
}