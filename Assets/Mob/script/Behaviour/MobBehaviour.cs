using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBehaviour : MonoBehaviour
{
    protected StatsEnemyManager stats;
    protected MobMovement move;
    protected bool spawned = false;

    protected string direction;

    public bool isTesting = false;
    public bool isScudoAttivo = false;
    bool isAlive = true; // per evitare che venga chiamata più volte la funzione di distruzione del mob


    HealthCanvas hc;

    private void Start()
    {
        mobInit();
        initCanvasVita();
    }

    void mobInit()
    {
        move = GetComponent<MobMovement>();
        stats = GetComponent<StatsEnemyManager>();
    }

    void initCanvasVita()
    {
        isScudoAttivo = generaScudo();
        // disabilito il canvas vita non utilizzato
        if (isScudoAttivo)
        {
            if (stats.vitaSenzaScudo != null)
                stats.vitaSenzaScudo.SetActive(false);
            hc = stats.vitaConScudo.GetComponentInChildren<HealthCanvas>();
        }
        else
        {
            if (stats.vitaConScudo != null)
                stats.vitaConScudo.SetActive(false);
            hc = stats.vitaSenzaScudo.GetComponentInChildren<HealthCanvas>();
        }
    }

    bool generaScudo()
    {
        return Random.Range(0f, 99f) > stats.percentualeScudoMob ? false : true;
    }

    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        spawned = true;
    }

    private void FixedUpdate()
    {
        if (spawned)
        {
            behaviour();
        }
    }

    void behaviour()
    {
        move.movement();                //avvia il pattern di movimento del mob
    }

    public void onMobDamageBomb(float damage)
    {
        if (isScudoAttivo && stats.scudo > 0)
        {
            // la bomba distrugge lo scudo
            stats.scudo = 0;
        }
        else
        {
            onMobDamage(damage);
        }

    }

    public void onMobDamage(float damage)
    {
        if (isScudoAttivo && stats.scudo > 0)
        {
            // metodo scudo a punti ferita
            // var tempScudo = stats.scudo; 
            // stats.scudo = Mathf.Max(tempScudo - damage, 0);
            // damage = Mathf.Max(damage - tempScudo, 0);

            // metodo scudo a tacche
            // togli una tacca allo scudo
            stats.scudo--;
        }
        else
        {
            
            // togli il danno
            stats.hp -= damage;
            if (stats.hp <= 0)
            {
                stats.speedCamminata = 0;
                stats.rangeAttacco = 0;
                if (isAlive)
                {
                    Die(GameObject.FindGameObjectWithTag("Player"));
                    isAlive = false;
                }
            }
        }
        hc.updateHealthCanvas();
    }

    public void Die(GameObject player)
    {

        if (player != null)
        {
//            player.GetComponent<ConsumableManager>().points += stats.pointExp;
//            player.GetComponentInChildren<dropMob>().drop(gameObject.transform);
            // segno il mob ucciso
//            player.GetComponent<ConsumableManager>().addMobUcciso();
        }


        // disattivo la visualizzazione della vita
        visualizzoVitaCanvas(false);

//        if (TryGetComponent(out  MobDeathEffect mobDeath))
//            mobDeath.hp = 0;

        if (!isTesting)
        {
//            stanza = generale.GetComponent<InformazioniGenerali>().GetStanza;
//            stanza.GetComponent<InformazioniStanza>().RemoveMob();
        }         
        
    }

    void visualizzoVitaCanvas(bool isVisible)
    {
        hc.gameObject.SetActive(isVisible);
    }

}
