# Changelog

Les changements notables du projet sont consignés dans ce fichier.

## [1.0.0.9] - 2026-09-27

### Modifié

- Décision confirmée : conserver une distribution autonome ; une archive d’environ 70 Mo est acceptable.
- Version du projet avancée de `1.0.0.8` à `1.0.0.9`.

## [1.0.0.8] - 2026-09-27

### Modifié

- Progression Bougskills mise à jour après la création du dépôt public `MasterBougli/KeyboardLocker` et le push initial sur `main`.
- Version du projet avancée de `1.0.0.7` à `1.0.0.8`.

## [1.0.0.7] - 2026-09-27

### Ajouté

- État de progression Bougskills pour suivre les décisions confirmées, les points ouverts et la suite du projet.
- Initialisation du dépôt Git local.

### Modifié

- Version du projet avancée de `1.0.0.6` à `1.0.0.7`.

## [1.0.0.6] - 2026-09-26

### Ajouté

- Contrôle utilisateur pour autoriser Alt+F4 pendant le verrouillage ; l’option est décochée par défaut.
- Workflow GitHub Actions pour compiler les pull requests et publier un build Windows x64 à chaque push sur la branche par défaut.
- Paquet portable de release comprenant l’application et sa documentation de licence.

### Modifié

- Toutes les frappes, y compris les touches Windows, sont désormais bloquées par défaut.
- Version d’identité Windows avancée de `1.0.0.5` à `1.0.0.6`.

## [1.0.0.5] - 2026-09-26

### Ajouté

- Format de tag de release GitHub `v<version>_build-<numéro>`, où la version provient du manifeste et le numéro du build du workflow.

### Modifié

- Version d’identité Windows avancée de `1.0.0.4` à `1.0.0.5`.

## [1.0.0.4] - 2026-09-26

### Modifié

- Les releases automatiques sont déclenchées à chaque push sur la branche par défaut du dépôt GitHub.
- Version d’identité Windows avancée de `1.0.0.3` à `1.0.0.4`.

## [1.0.0.3] - 2026-09-26

### Ajouté

- Exigence d’une compilation automatique et d’une release GitHub pour chaque push sur la branche par défaut.
- GitHub défini comme plateforme de contribution.

### Modifié

- Version d’identité Windows avancée de `1.0.0.2` à `1.0.0.3`.

## [1.0.0.2] - 2026-09-26

### Modifié

- Le mode de verrouillage documenté bloque `Alt+F4` par défaut ; l’utilisateur pourra choisir de l’autoriser.
- Version d’identité Windows avancée de `1.0.0.1` à `1.0.0.2`.

## [1.0.0.1] - 2026-09-26

### Ajouté

- Première version de Keyboard Locker pour Windows avec verrouillage temporaire du clavier, souris utilisable, délai de sécurité et icône de notification.
- Documentation du projet, du fonctionnement, des limites techniques, de la sécurité et des contributions.
- Licence GNU GPL v3.0, copyright © 2026 Bougli.
- Option prévue pour contrôler le comportement de `Alt+F4` pendant le verrouillage, bloqué par défaut.

### Modifié

- Version d’identité Windows avancée de `1.0.0.0` à `1.0.0.1`.
