# 🚀️👀️ API pour gérer mes candidatures 👀️🚀️

Commençant à accumuler les candidatures lors de ma recherche d'emploi, j'ai fini par me dire : "Hey ! Pourquoi ne pas développer ma propre application pour gérer mes candidatures, tout en apprenant React.js et TypeScript ?".

A ce stade, l'application permet de gérer toutes les candidatures présentes dans la base de données (de type SQL Server), sans notion d'utilisateur. Il est possible d'importer un ensemble de candidatures à partir d'un fichier au format .xlsx.

La gestion de comptes utilisateurs sera ajoutée plus tard.

---

## Résumé des requêtes disponibles :

### Etats de candidature :

> GET - GetAll - _domain_name_/**statuses**

### Candidatures :

> GET - GetAll - _domain_name_/**jobapplications**

> POST - ImportSeveralFromXlsx - _domain_name_/**importjobapps**

> POST - PostOne - _domain_name_/**jobapplication**

> PUT - UpdateOne - _domain_name_/**jobapplication**?id=_[job_application_id]_

> DELETE - DeleteOne - _domain_name_/**jobapplication**?id=_[job_application_id]_

## 1. Entités disponibles

### 1.1. Etat de la candidature

#### Propriétés

| Nom colonne | Type   | Description                                                                           |
| ----------- | ------ | ------------------------------------------------------------------------------------- |
| Name        | String | Nom qui sera utilisé pour l'affichage front-end.<br /> Exemple : "En préparation"     |
| CodeName    | String | Nom utilisé en interne, **à ne pas modifier**. <br/><br />_Exemple : "InPreparation"_ |
| Color       | String | Au format CSS, pour affichage des icônes et étiquettes liées                          |
| IconName    | String | Nom Material Design Icon, par exemple "mdiDrawPen"                                    |

### 1.2. Candidature

#### Propriétés

Identification de l'état / du statut + autres :

| Nom colonne        | Type       | Description                                                                      |
| ------------------ | ---------- | -------------------------------------------------------------------------------- |
| Date               | DateTime   | Date d'envoi ou de réponse à la candidature                                      |
| Source             | String     | Entreprise, site web ou cabinet de recrutement source                            |
| IsSpontaneous      | Booléen    | Candidature spontanée ?                                                          |
| IsFromMyInitiative | Booléen    | Ai-je envoyé une candidature ou ai-je été contactée ?                            |
| OfferUrl           | String     | URL de l'offre d'emploi                                                          |
| Position           | String     | Poste proposé / voulu                                                            |
| Place              | String     | Lieu / ville de travail                                                          |
| Statut             | **Status** | Décrit l'état de la candidature                                                  |
| Motivations        | String     |                                                                                  |
| Notes              | String     |                                                                                  |
| Contacts           | String     |                                                                                  |
| FeelingLevel       | Integer    | De 0 à 5, comment je le sens ? (L'entreprise, le contact avec le recruteur, etc) |

## 2. Requêtes disponibles

### 2.1. Etat de la candidature

#### **GET - GetAll**

- URL : _domain_name_/**statuses**
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

- URL : _domain_name_/**jobapplications**
- Format d'objet renvoyé par l'API :

```
[
    {
        "id": "string",
        "date": "string",
        "source": "string",
        "isSpontaneous": boolean,
        "isFromMyInitiative": boolean,
        "offerUrl": "string",
        "position": "string",
        "place": "string",
        "statusId": "string",
        "motivations": "string",
        "notes": "string",
        "contacts": "string",
        "feelingLevel": number
    }
]
```

#### **POST - ImportSeveralFromXlsx**

- URL : _domain_name_/**importjobapps**
- Corps de requête : un fichier .xlsx transmis dans un objet de type FormData (pour javascript) avec l'intitulé "file". Tout autre format de fichier sera refusé avec un message d'erreur.
- Structure du fichier : **LES 3 PREMIERES COLONNES NE SONT PAS LUES LORS DE L'IMPORT car contiennent actuellement des informations qui ne seront plus utiles par la suite**

| Nom colonne         | Type    | Obligatoire ? | Description                                                                                         |
| ------------------- | ------- | ------------- | --------------------------------------------------------------------------------------------------- |
| COLONNE 1           |         |               | **COLONNE NON UTILISEE DANS L'IMPORT**                                                              |
| COLONNE 2           |         |               | **COLONNE NON UTILISEE DANS L'IMPORT**                                                              |
| COLONNE 3           |         |               | **COLONNE NON UTILISEE DANS L'IMPORT**                                                              |
| source              | string  | OUI           |                                                                                                     |
| isSpontaneous       | string  | OUI           | "FALSE" or "TRUE"                                                                                   |
| fromMyInitiative    | string  | OUI           | "FALSE" or "TRUE"                                                                                   |
| offerUrl            | string  |               |                                                                                                     |
| position            | string  | OUI           |                                                                                                     |
| place               | string  | OUI           |                                                                                                     |
| status              | string  | OUI           | Valeurs correspondant au "CodeName" de la base de données ("InPreparation", "Sent", "Ghosted", etc) |
| motivations         | string  |               |                                                                                                     |
| notes               | string  |               |                                                                                                     |
| contacts            | string  |               |                                                                                                     |
| feelingLevel        | integer |               |                                                                                                     |
| answerDelay (weeks) | integer |               |                                                                                                     |
| dateForBackend      | string  |               | Au format yyyy-MM-dd                                                                                |

#### **POST - PostOne**

- URL : _domain_name_/**jobapplication**
- Corps de requête :

```
{
    "date": "string",
    "source": "string",
    "isSpontaneous": boolean,
    "isFromMyInitiative": boolean,
    "offerUrl": "string",
    "position": "string",
    "place": "string",
    "statusId": "string",
    "motivations": "string",
    "notes": "string",
    "contacts": "string",
    "feelingLevel": number
}
```

- Renvoi par l'API de l'objet créé :

```
{
    "id": "string",
    "date": "string",
    "source": "string",
    "isSpontaneous": boolean,
    "isFromMyInitiative": boolean,
    "offerUrl": "string",
    "position": "string",
    "place": "string",
    "statusId": "string",
    "motivations": "string",
    "notes": "string",
    "contacts": "string",
    "feelingLevel": number
}
```

#### **PUT - UpdateOne**

- URL : _domain_name_/**jobapplication**?id=_[job_application_id]_
- Corps de requête :

```
{
    "date": "string",
    "source": "string",
    "isSpontaneous": boolean,
    "isFromMyInitiative": boolean,
    "offerUrl": "string",
    "position": "string",
    "place": "string",
    "statusId": "string",
    "motivations": "string",
    "notes": "string",
    "contacts": "string",
    "feelingLevel": number
}
```

- Renvoi par l'API de l'objet mis à jour :

```
{
    "id": "string",
    "date": "string",
    "source": "string",
    "isSpontaneous": boolean,
    "isFromMyInitiative": boolean,
    "offerUrl": "string",
    "position": "string",
    "place": "string",
    "statusId": "string",
    "motivations": "string",
    "notes": "string",
    "contacts": "string",
    "feelingLevel": number
}
```

#### **DELETE - DeleteOne**

- URL : _domain_name_/**jobapplication**?id=_[job_application_id]_
- Renvoie un message de confirmation ou d'erreur après traitement

## 3. Descriptif des variables d'environnement nécessaires

| Nom              | Descriptif                                                                                                                                                                                                                                                                  |
| ---------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ConnectionString | Chaîne de connexion SQL : **Server=_INSTANCE_SQL_;Database=_NOM_BASE_;Trusted_Connection=True;TrustServerCertificate=True;** -------- Si nécessaire, remplacer **EN LOCAL, LORS DU DEV** "Trusted_Connection=True;" par "User ID=[VALUE];Password=[VALUE];Database=[VALUE]; |
| FrontEndDomains  | Urls des domaines pouvant appeler l'API, séparés par des point-virgules                                                                                                                                                                                                     |
