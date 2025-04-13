# 🚀️👀️ API pour gérer mes candidatures 👀️🚀️

Commençant à accumuler les candidatures pour un emploi dans le développement web .NET, j'ai fini par me dire : "Hey ! Pourquoi ne pas développer ma propre application pour gérer mes candidatures ?".

Pour le moment, l'application se contente de gérer toutes les candidatures présentes dans la base de données. 

Par la suite je vais ajouter la gestion de comptes utilisateurs.

_____________________

Résumé des requêtes disponibles :
- Etats de candidature : 
>GET - GetAll - *domain_name*/**statuses**
- Candidatures : 
>GET - GetAll - *domain_name*/**jobapplications**

>POST - PostOne - *domain_name*/**jobapplication**

>DELETE - DeleteOne - *domain_name*/**jobapplication**?id=*[job_application_id]*

## 1. Entités disponibles

### 1.1. Etat de la candidature

#### Propriétés


| Nom colonne | Type   | Description                                                                        |
| ----------- | ------ | ---------------------------------------------------------------------------------- |
| Name        | String | Nom qui sera utilisé pour l'affichage front-end<br />*Exemple : "En préparation"*  |
| CodeName    | String | Nom utilisé en interne,**à ne pas modifier**<br/><br />*Exemple : "InPreparation"* |
| Color       | String | Au format CSS, pour affichage des icônes et étiquettes liées                       |
| IconName    | String | Nom Material Design Icon, par exemple "mdiDrawPen"                                 |

### 1.2. Candidature

#### Propriétés

Identification de l'état / du statut + autres :


| Nom colonne        | Type     | Description                                                                      |
| ------------------ | -------- | -------------------------------------------------------------------------------- |
| Date               | DateTime | Date d'envoi ou de réponse à la candidature                                      |
| Source             | String   | Entreprise, site web ou cabinet de recrutement source                            |
| IsSpontaneous      | Booléen  | Candidature spontanée ?                                                          |
| IsFromMyInitiative | Booléen  | Ai-je envoyé une candidature ou ai-je été contactée ?                            |
| OfferUrl           | String   | URL de l'offre d'emploi                                                          |
| Position           | String   | Poste proposé / voulu                                                            |
| Place              | String   | Lieu / ville de travail                                                          |
| Motivations        | String   |                                                                                  |
| Notes              | String   |                                                                                  |
| Contacts           | String   |                                                                                  |
| FeelingLevel       | Integer  | De 0 à 5, comment je le sens ? (L'entreprise, le contact avec le recruteur, etc) |

## 2. Requêtes disponibles

### 2.1. Etat de la candidature

#### **GET - GetAll**

- URL : *domain_name*/**statuses**
- Format d'objet renvoyé par l'API :

```
[
    {
        "id": "string",
        "name": "string",
        "color": "string",
        "iconName": "string"
    }
]
```

### 2.2. Candidature

#### **GET - GetAll**

- URL : *domain_name*/**jobapplications**
- Format d'objet renvoyé par l'API :

```
[
    {
        "id": "string",
        "date": "string",
        "source": "string",
        "isSpontaneous": true,
        "isFromMyInitiative": true,
        "offerUrl": "string",
        "position": "string",
        "place": "string",
        "statusId": "string",
        "motivations": "string",
        "notes": "string",
        "contacts": "string",
        "feelingLevel": 0
    }
]
```

#### **POST - PostOne**

- URL : *domain_name*/**jobapplication**
- Corps de requête :

```
{
    "date": "string",
    "source": "string",
    "isSpontaneous": true,
    "isFromMyInitiative": true,
    "offerUrl": "string",
    "position": "string",
    "place": "string",
    "statusId": "string",
    "motivations": "string",
    "notes": "string",
    "contacts": "string",
    "feelingLevel": 0
}
```

- Renvoi par l'API de l'objet créé :

```
{
    "id": "string",
    "date": "string",
    "source": "string",
    "isSpontaneous": true,
    "isFromMyInitiative": true,
    "offerUrl": "string",
    "position": "string",
    "place": "string",
    "statusId": "string",
    "motivations": "string",
    "notes": "string",
    "contacts": "string",
    "feelingLevel": 0
}
```

#### **DELETE - DeleteOne**

- URL : *domain_name*/**jobapplication**?id=*[job_application_id]*
- Renvoie un message de confirmation ou d'erreur après traitement


## 3. Descriptif des variables d'environnement nécessaires

| Nom | Descriptif |
| ---- | --------- |
| ConnectionString | Chaîne de connexion SQL : Server=*INSTANCE_SQL*;Database=*NOM_BASE*;Trusted_Connection=True;TrustServerCertificate=True; |  
| FrontEndDomains | Urls des domaines pouvant appeler l'API, séparés par des point-virgules |

