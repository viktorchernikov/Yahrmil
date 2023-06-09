using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public abstract class BaseWorldTape : MonoBehaviour
{
    public TapeVertex BeginningVertex;
    public TapeVertex EndingVertex;
    public Vector2 Beginning
    {
        get => _beginning;
        set
        {
            _beginning = value - (Vector2)transform.position;
            BeginningVertex.SetPosition(_beginning);
            BeginningVertex.Show();
            BeginningVertex.Rotate = true;
        }
    }
    public Vector2 End
    {
        get => _ending;
        set
        {
            _ending = value - (Vector2)transform.position;
            EndingVertex.SetPosition(_ending);
            EndingVertex.Show();
            BeginningVertex.Rotate = false;
            UpdateLine();
            UpdateCollider();
        }
    }
    public Vector2 BeginningWp { get => (Vector2)transform.position + _beginning; }
    public Vector2 EndWp { get => (Vector2)transform.position + _ending; }
    public Color Color;

    private Vector2 _beginning;
    private Vector2 _ending;
    private LineRenderer _renderer;
    private EdgeCollider2D _collider;

    public virtual void OnPlayerEnter(Player player, ContactPoint2D contact) { }
    public virtual void OnPlayerStay(Player player, ContactPoint2D contact) { }
    public virtual void OnPlayerExit(Player player, ContactPoint2D contact) { }
    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    public void UpdateLine()
    {
        if (_renderer.positionCount != 2)
        {
            _renderer.positionCount = 2;
        }
        _renderer.SetPosition(0, transform.position + (Vector3)Beginning);
        _renderer.SetPosition(1, transform.position + (Vector3)End);
    }
    public void UpdateCollider()
    {
        List<Vector2> points = new();
        points.Add(Beginning);
        points.Add(End);
        _collider.SetPoints(points);
    }
    public void UpdateColors()
    {
        _renderer.startColor = Color;
        _renderer.endColor = Color;
        BeginningVertex.SetColor(Color);
        EndingVertex.SetColor(Color);
    }

    #region Unity callbacks
    private void Awake()
    {
        _renderer = GetComponent<LineRenderer>();
        _collider = GetComponent<EdgeCollider2D>();
    }
    private void Start()
    {
        UpdateColors();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D p in collision.contacts)
        {
            if (p.collider.CompareTag("Player"))
            {
                Player pl = p.collider.GetComponent<Player>();
                OnPlayerEnter(pl, p);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D p in collision.contacts)
        {
            if (p.collider.CompareTag("Player"))
            {
                Player pl = p.collider.GetComponent<Player>();
                OnPlayerStay(pl, p);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        foreach (ContactPoint2D p in collision.contacts)
        {
            if (p.collider.CompareTag("Player"))
            {
                Player pl = p.collider.GetComponent<Player>();
                OnPlayerExit(pl, p);
            }
        }
    }
    #endregion
}

