using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/*
 * <summary> 
 * <remarks>
	Author: Filippo Taiuti 
	Date: 12/03/2021 
	</remarks>
		
	caratteristiche dei nemici

	</summary>
 */
public class StatsEnemyManager : MonoBehaviour
{
    [Header("Generalità del mob")]
    //aggiunto da Paolo Fasano in data 17/03/2021
    [Tooltip("identifica il mob dal numero")]
    public int id;
    //aggiunto da Paolo Fasano in data 17/03/2021
    [Tooltip("identifica la 'versione' del mob")]
    public int lvCorruzione;
    [Header("Difesa del mob")]
    [Tooltip("Gli HP del Mob, slide rossa")]
    public float hp = 30f;
    [Tooltip("percentuale che il mob abbia lo scudo, slide blu")]
    [Range(0f, 100f)]
    public float percentualeScudoMob = 30f;
    [Tooltip("Num. di colpi assorbiti dallo scudo")]
    public int scudo = 3;
    [Header("Range del mob")]
    [Tooltip("Il mob aggra subito il player più vicino")]
    public bool aggroIstantaneo = false;
    [SerializeReference]
    [Tooltip("Area di inseguimento")]
    public float rangeAggro = 10f;
    [Tooltip("Area di attacco")]
    public float rangeAttacco = 10f;
    [Tooltip("Distanza del proiettile/esplosione")]
    public float rangeProiettile = 10f;
    [Tooltip("Disegna cerchio dei range aggro, attacco e proiettile")]
    public bool mostraRangeInScena;
    [Header("Danni del mob")]
    [Tooltip("Danno del proiettile al PG")]
    public float dannoPG = 1f;
    [Tooltip("Danno agli altri Mob.\n0 = no danno")]
    public float dannoMob = 1f;
    [Tooltip("Danno agli ostacoli - Da implementare")]
    public float dannoOstacoli = 1f;
    [Tooltip("Danno da contatto con il PG")]
    public float dannoDaContattoPG = 1f;
    [Header("Velocità")]
    [Tooltip("Velocità proiettile")]
    public float speedProiettile = 1f;
    [Tooltip("Velocità camminata")]
    public float speedCamminata = 1f;
    [Tooltip("Numero di attacchi al secondo")]
    public float speedAtk = 1f;
    [Tooltip("Tempo di attesa dell'esplosione.\nPer l'attacco kamikaze")]
    public float tempoPrimaDiEsplodere = 1f;

    [Header("Altro")]
    [Tooltip("Punti esperienza uccisione nemico")]
    public float pointExp = 1f;
    [Tooltip("Esclusioni nemico - Da implementare")]
    public Esclusione esclusione;

    [Header("Vita canvas")]
    public GameObject vitaConScudo;
    public GameObject vitaSenzaScudo;

    public enum Esclusione
    {
        None,
        NDC,
        noAggro,
        ImmuneDaSpell
    } 

    // spostato sullo script di attacco del mob
    //    [Header("arma del mob")]
    //    [Tooltip("proiettile usato dal mob")]
    //    public GameObject proiettile;

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (mostraRangeInScena)
        {
            float x, y;
            Vector3 pos, newPos, lastPos;

            if (!aggroIstantaneo)
            {
                // aggro
                Gizmos.color = Color.white;
                x = rangeAggro * Mathf.Cos(0);
                y = rangeAggro * Mathf.Sin(0);
                pos = transform.position + new Vector3(x, y, 0);
                lastPos = pos;
                for (float ray = 0.1f; ray < Mathf.PI * 2; ray += 0.1f)
                {
                    x = rangeAggro * Mathf.Cos(ray);
                    y = rangeAggro * Mathf.Sin(ray);
                    newPos = transform.position + new Vector3(x, y, 0);
                    Gizmos.DrawLine(pos, newPos);
                    pos = newPos;
                }
                Gizmos.DrawLine(pos, lastPos);
            }

            // attacco
            Gizmos.color = Color.magenta;
            x = rangeAttacco * Mathf.Cos(0);
            y = rangeAttacco * Mathf.Sin(0);
            pos = transform.position + new Vector3(x, y, 0);
            lastPos = pos;
            for (float ray = 0.1f; ray < Mathf.PI * 2; ray += 0.1f)
            {
                x = rangeAttacco * Mathf.Cos(ray);
                y = rangeAttacco * Mathf.Sin(ray);
                newPos = transform.position + new Vector3(x, y, 0);
                Gizmos.DrawLine(pos, newPos);
                pos = newPos;
            }
            Gizmos.DrawLine(pos, lastPos);

            // proiettile
            Gizmos.color = Color.red;
            x = rangeProiettile * Mathf.Cos(0);
            y = rangeProiettile * Mathf.Sin(0);
            pos = transform.position + new Vector3(x, y, 0);
            lastPos = pos;
            for (float ray = 0.1f; ray < Mathf.PI * 2; ray += 0.1f)
            {
                x = rangeProiettile * Mathf.Cos(ray);
                y = rangeProiettile * Mathf.Sin(ray);
                newPos = transform.position + new Vector3(x, y, 0);
                Gizmos.DrawLine(pos, newPos);
                pos = newPos;
            }
            Gizmos.DrawLine(pos, lastPos);
        }
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(StatsEnemyManager))]

public class StatsEnemyManagerEditor: Editor
{
    SerializedProperty aggroIstantaneo;

    private void OnEnable()
    {
        aggroIstantaneo = serializedObject.FindProperty("aggroIstantaneo");
    }

    public override void OnInspectorGUI()
    {
        SerializedProperty prop = serializedObject.GetIterator();

        if (prop.NextVisible(true))
        {
            do
            {
                if (prop.name == "scudo")
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(prop.name), true);
                    EditorGUI.indentLevel--;
                }
                else if (prop.name == "rangeAggro")
                {
                    if (!aggroIstantaneo.boolValue)
                    {
                        EditorGUILayout.PropertyField(serializedObject.FindProperty(prop.name), true);
                    }
                }
                else
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(prop.name), true);
                }
                // Draw movePoi
            }
            while (prop.NextVisible(false));
        }
        
        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();

    }
}
#endif

