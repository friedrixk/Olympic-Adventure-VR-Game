using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Throwable))]
public class Ball : MonoBehaviour
{
    public enum Type {
        Basic,
        Green,
        Red,
        Blue,
        Yellow
    }

    [SerializeField]
    private Type type = Type.Basic;

    // References to materials for each type
    [SerializeField]
    private Material basicMaterial;

    [SerializeField]
    private Material greenMaterial;

    [SerializeField]
    private Material redMaterial;

    [SerializeField]
    private Material blueMaterial;

    [SerializeField]
    private Material yellowMaterial;

    [SerializeField]
    private Material basicHoverMaterial;

    [SerializeField]
    private Material blueHoverMaterial;

    [SerializeField]
    private Material redHoverMaterial;

    [SerializeField]
    private Material basicSelectedMaterial;

    [SerializeField]
    private Material blueSelectedMaterial;

    [SerializeField]
    private Material redSelectedMaterial;


    private bool isDissolving = false;
    private float fade = 1f;
    private float fadeSpeed = 0.1f;

    private Throwable throwable;


    void Start()
    {  
        UpdateMaterial();       

    }

    void Awake() {
        throwable = GetComponent<Throwable>();
        UpdateMaterial();
    }


    void Update()
    {
        UpdateMaterial();

        if (isDissolving && throwable.isSelected != -1) {
            isDissolving = false;
            fade = 1f;
        } else if (fade <= 0f) {
            isDissolving = false;
            fade = 0f;
            Destroy(gameObject);
        }
    }

    void FixedUpdate() {
        if (isDissolving) {
            fade -= fadeSpeed * Time.deltaTime;
        }
    }

    public void SetType(Type type) {
        this.type = type;
        UpdateMaterial();
    }

    public void UpdateMaterial() {
        Material compontentMaterial = GetComponent<Renderer>().material;

        if (compontentMaterial == null) {
            Debug.LogWarning("No material found on " + gameObject.name);
            return;
        }

        Material idleMaterial = basicMaterial;
        Material hoverMaterial = basicHoverMaterial;
        Material selectedMaterial = basicSelectedMaterial;
        switch (type) {
            case Type.Basic:
                break;
            case Type.Green:
                break;
            case Type.Red:
                idleMaterial = redMaterial;
                hoverMaterial = redHoverMaterial;
                selectedMaterial = redSelectedMaterial;
                break;
            case Type.Blue:
                idleMaterial = blueMaterial;
                hoverMaterial = blueHoverMaterial;
                selectedMaterial = blueSelectedMaterial;
                break;
            case Type.Yellow:
                break;
            default:
                break;
        }
        switch (throwable.GetState()) {
            case Throwable.State.Hover:
                GetComponent<Renderer>().material = hoverMaterial;
                Debug.Log("Stone is hovering");
                break;
            case Throwable.State.Selected:
                GetComponent<Renderer>().material = selectedMaterial;
                Debug.Log("Stone is selected");
                break;
            default:
                GetComponent<Renderer>().material = idleMaterial;
                break;
        }
    }

    public Type getType() {
        return type;
    }

    void OnTriggerEnter(Collider other) {


        if (other.gameObject.name == "Terrain") {
            isDissolving = true;
        }
        if (other.gameObject.tag == "Floor") {
            isDissolving = true;
        }

        if (other.gameObject.tag == "Fire") {
            if (type == Type.Basic) {
                SetType(Type.Red);
            }

            if (type == Type.Blue) {
                SetType(Type.Basic);
            }
        }

        if (other.gameObject.tag == "Trident") {
            if (type == Type.Basic) {
                SetType(Type.Blue);
            }

            if (type == Type.Red) {
                SetType(Type.Basic);
            }
        }
    }
}
