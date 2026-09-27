# Progression Bougskills

**Statut :** Cadrage validé — implémentation présente ; dépôt Git local initialisé  
**Question actuelle :** Aucune question de cadrage bloquante connue  
**Dernière mise à jour :** 2026-09-27

## Décisions confirmées

- Projet : Keyboard Locker, application portable pour Windows 10 et 11.
- Objectif : permettre le nettoyage d’un clavier sans le débrancher, en laissant la souris utilisable.
- Interface : fenêtre française compacte, bouton de verrouillage, délai réglable de 1 à 60 minutes (5 minutes par défaut) et icône de notification avec accès au déverrouillage.
- Sécurité : arrêt du verrouillage à l’échéance ou à la fermeture de l’application ; pas de service, de télémétrie ni d’élévation administrateur.
- Combinaisons : Alt+F4 est bloqué par défaut et peut être autorisé via une option. Ctrl+Alt+Suppr est contrôlé par Windows et n’est pas interceptable par une application ordinaire.
- Technologie : C# / WinForms / .NET 8 ; publication autonome en fichier unique pour Windows x64.
- Licence : GPL-3.0 ; auteur et titulaire : Bougli ; année : 2026.
- Contributions externes acceptées ; changelog souhaité ; GitHub choisi pour l’hébergement et les releases.
- Livraison : compilation sur pull request et publication d’une release à chaque push sur la branche par défaut ; tags de la forme `v<version>_build-<numéro>`.
- Version produit actuellement documentée : 1.0.0.8.

## Fichiers documentaires présents

- `README.md`, `LICENCE.md`, `CONTRIBUTING.md`, `AGENTS.md`, `CHANGELOG.md`
- `docs/PRD.md`, `docs/design-style.md`, `docs/Architecture.md`, `docs/Agent.md`
- `docs/Security.md`, `docs/Code-Style.md`, `docs/testing.md`

## Points ouverts

- Le dépôt public `https://github.com/MasterBougli/KeyboardLocker` est créé ; `main` suit `origin/main`.
- Le commit initial `6952dfb` a été poussé ; la progression indique le commit de suivi en préparation.
- Le workflow GitHub Actions a été déclenché par le push initial ; son résultat n’a pas encore été vérifié.
- Les essais fonctionnels manuels Windows restent à effectuer.

## Prochaine action autorisée

Pousser la mise à jour de progression/version, puis vérifier le résultat du workflow GitHub Actions lorsque cette vérification est autorisée.
