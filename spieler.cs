using UnityEngine;
using TMPro;

public class Spieler : MonoBehaviour
{
public TextMeshProUGUI zeitAltAnzeige;
void Start()
{
    float zeitAlt = 0;
    if (PlayerPrefs.HasKey("zeitAlt"))
        zeitAlt = PlayerPrefs.GetFloat("zeitAlt");
    zeitAltAnzeige.text = string.Format("Alt: {0,6:0.0} sec", zeitAlt);
}
public GameObject gewinn;
readonly float eingabeFaktor = 10;
float zeitStart;
bool spielGestartet = false;
public TextMeshProUGUI zeitAnzeige;
void Update()
{
    float xEingabe = Input.GetAxis("Horizontal");
    float yEingabe = Input.GetAxis("Vertical");
    if (yEingabe < 0)
    {
        return;
    }

    float xNeu = transform.position.x +
        xEingabe * eingabeFaktor * Time.deltaTime;
    if(xNeu > 8.3f)
    {
        xNeu = 8.3f;
    }
    if (xNeu < -8.3f)
    {
        xNeu = -8.3f;
    }
    float yNeu = transform.position.y +
        yEingabe * eingabeFaktor * Time.deltaTime;
    transform.position = new Vector3(xNeu, yNeu, 0);
    if (!spielGestartet && (xEingabe != 0 || yEingabe != 0))
    {
        spielGestartet = true;
        zeitStart = Time.time;
        infoAnzeige.text = "";
    }
    if (spielGestartet)
        zeitAnzeige.text = string.Format("Zeit: {0,6:0.0} sec.",
            Time.time - zeitStart);
}
int anzahlPunkte = 0;
public TextMeshProUGUI punkteAnzeige;
int anzahlLeben = 3;
public TextMeshProUGUI lebenAnzeige;
public TextMeshProUGUI infoAnzeige;
void OnCollisionEnter2D(Collision2D coll)
{
    if (coll.gameObject.CompareTag("Gewinn"))
    {
        anzahlPunkte++;
        gewinn.SetActive(false);
        if (anzahlPunkte < 6)
        {
        punkteAnzeige.text = "Punkte: " + anzahlPunkte;
        if (anzahlPunkte == 1)
            infoAnzeige.text = "Du hast bereits 1 Punkt!";
        else
            infoAnzeige.text = "Du hast bereits" + anzahlPunkte + "Punkte";
        }
       
        else
        {
            infoAnzeige.text = "Du hast gewonnen!";
            gameObject.SetActive (false);
            gewinn.SetActive (false);
            punkteAnzeige.text = "Gewonnen";
            PlayerPrefs.SetFloat("zeitAlt", Time.time - zeitStart);
            PlayerPrefs.Save();
        }
    
        float xNeu = Random.Range(-8.0f, 8.0f);
        float yNeu;
        if (anzahlPunkte < 2) yNeu = -2.7f;
        else if (anzahlPunkte < 4) yNeu = 0.15f;
        else yNeu = 3;

        gewinn.transform.position = new Vector3 (xNeu, yNeu, 0);
    }
    else if (coll.gameObject.CompareTag("Gefahr"))
    {
        anzahlLeben--;
        lebenAnzeige.text = "Leben:" + anzahlLeben;
    
        gameObject.SetActive (false);
        if(anzahlLeben > 0)
            {
                infoAnzeige.text = "Du hast nur noch" + anzahlLeben + "Leben!";
                Invoke (nameof(NaechstesLeben), 2);
            }
        else
        {
            gewinn.SetActive (false);
            lebenAnzeige.text = "Verloren";
        }
    }
}
void NaechsterGewinn()
{
    float xNeu = Random.Range(-8.0f, 8.0f);

    float yNeu;
    if (anzahlPunkte < 2) yNeu = -2.7f;
    else if (anzahlPunkte < 4) yNeu = 0.15f;
    else yNeu = 3;

    gewinn.transform.position = new Vector3 (xNeu, yNeu, 0);
    gewinn.SetActive (true);
    infoAnzeige.text = "";
}
void NaechstesLeben()
{
    transform.position = new Vector3(0, -4.4f, 0);
    gameObject.SetActive(true);
    infoAnzeige.text = "";
}
}
