using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsPopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public static PointsPopup Create(Vector3 position, string text, Color color)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        PointsPopup damagePopup = damagePopupTransform.GetComponent<PointsPopup>();
        damagePopup.Setup(text, color);
        return damagePopup;
    }

    public void Setup(string text, Color color)
    {
        textMesh.SetText(text);
        textMesh.color = color;
        textColor = textMesh.color;
        disappearTimer = 0.5f;
    }

    
    private void Update()
    {
        float moveYSpeed = 4f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if (disappearTimer <0)
        {
            float disappearSpeed = 250f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
