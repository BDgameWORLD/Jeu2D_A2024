using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _vitesseLaserJoueur = 20f;
    [SerializeField] private string _nom = default;
    [SerializeField] private GameObject _miniExplosionPrefab = default;

    // La vitesse du laser ennemi est bas� sur la vitesse de celui-ci
    private float _vitesseLaserEnnemi;

    void Update()
    {
        //Si c'est un laser du joueur
        if (_nom == "Player")
        {
            // D�place le laser vers le haut
            DeplacementLaserJoueur();
        }
        else if (_nom == "LaserEnemy")
        {
            _vitesseLaserEnnemi = GameManager.Instance.VitesseEnnemi + 3.0f;
            transform.Translate(Vector3.down * Time.deltaTime * _vitesseLaserEnnemi);
            if (transform.position.y < -6f)
            {
                Destroy(this.gameObject);
            }
        }
        else if(_nom == "MissileEnemy")
        {
            _vitesseLaserEnnemi = GameManager.Instance.VitesseEnnemi + 6.0f;
            transform.Translate(Vector3.down * Time.deltaTime * _vitesseLaserEnnemi);
            if (transform.position.y < -6f)
            {
                Destroy(this.gameObject);
            }
        }


    }

    private void DeplacementLaserJoueur()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _vitesseLaserJoueur);
        if (transform.position.y > 8f)
        {
            // Si le laser sort de l'�cran il se d�truit
            if (this.transform.parent == null)
            {
                Destroy(this.gameObject);
            }
            // Si le laser fait partie d'un conteneur il d�truit le conteneur (triple laser)
            else
            {
                Destroy(this.transform.parent.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Condition qui v�rifie si le laser ou le missile ennemi frappe le joueur
        if (other.tag == "Player" && _nom != "Player")
        {
            Player player = other.GetComponent<Player>();
            player.Degats();
            Instantiate(_miniExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
