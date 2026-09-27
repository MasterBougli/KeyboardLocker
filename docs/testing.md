# Vérifications

**Statut :** Brouillon — checklist définie, essais non exécutés

**Dernière mise à jour : 2026-09-27**

Cette version s’appuie sur une checklist manuelle. Aucun framework ni package de test n’est ajouté au projet.

## Précautions avant les essais

- Effectuer les vérifications sur une session Windows de test, avec une souris fonctionnelle.
- Enregistrer et fermer les documents ouverts ; ne pas faire l’essai au milieu d’une saisie importante.
- Commencer avec le délai minimal d’une minute et garder le menu de notification accessible.
- Ne pas tester sur l’écran de connexion ou dans un bureau sécurisé. Ctrl+Alt+Suppr est une séquence Windows et n’est pas filtrée par l’application.
- Après chaque scénario, vérifier que le clavier est de nouveau utilisable avant de poursuivre.

## Parcours fonctionnel

### Configuration et activation

1. Au démarrage, confirmer l’état « Prêt à verrouiller le clavier », le délai de 5 minutes et l’option Alt+F4 décochée.
2. Vérifier que le délai accepte 1 et 60 minutes, refuse les valeurs hors limites et ne peut plus être modifié pendant le verrouillage.
3. Choisir une minute et cliquer sur « Verrouiller le clavier ».
4. Confirmer que l’état passe à verrouillé, que le compte à rebours apparaît et que l’icône reste présente dans la zone de notification.

### Clavier et souris

5. Essayer des lettres, chiffres, Entrée, Échap, les touches de fonction, les flèches, les touches Windows et quelques combinaisons ; vérifier qu’elles ne sont pas transmises pendant le verrouillage.
6. Utiliser la souris pour cliquer, déplacer la fenêtre, ouvrir le menu de notification et sélectionner « Déverrouiller » ; le clavier doit refonctionner immédiatement.
7. Refaire le verrouillage avec l’option « Autoriser Alt+F4 » décochée : Alt+F4 ne doit pas fermer la fenêtre active.
8. Cocher l’option avant un nouveau verrouillage ; Alt+F4 doit fonctionner conformément au comportement normal de Windows. Confirmer ensuite que les autres frappes restent filtrées.
9. Appuyer sur Ctrl+Alt+Suppr : vérifier que Windows affiche son écran sécurisé, puis revenir à la session. Ne pas s’attendre à ce que l’application bloque cette séquence.

### Échéance et fermeture

10. Verrouiller pour une minute, attendre l’échéance et confirmer le déverrouillage automatique ainsi que le retour de l’état « prêt ».
11. Verrouiller puis réduire la fenêtre ; confirmer qu’elle disparaît de la barre des tâches, reste accessible depuis l’icône et peut être rouverte par double-clic.
12. Déverrouiller depuis le menu de l’icône de notification, puis refaire l’essai et choisir « Quitter » ; le clavier doit être utilisable après fermeture.
13. Verrouiller, mettre Windows en veille au-delà de l’échéance, puis reprendre la session ; confirmer que le délai écoulé libère le clavier rapidement après reprise.

## Compatibilité et livraison

- Refaire les parcours principaux sous Windows 10 et Windows 11, sur chaque architecture effectivement publiée.
- Vérifier l’affichage à l’échelle Windows par défaut et avec une mise à l’échelle supérieure.
- Publier le binaire autonome, le lancer sur une machine de test sans runtime .NET préinstallé et refaire les parcours principaux.
- Confirmer dans GitHub Actions que le build réussit et que l’archive de release contient l’exécutable et les documents annoncés.

## Critères de réussite

- La souris et les chemins de déverrouillage restent disponibles pendant tout le verrouillage.
- Le délai maximal choisi, la fermeture et le retour de veille ne laissent pas le clavier bloqué.
- Le comportement d’Alt+F4 correspond à l’option choisie ; Ctrl+Alt+Suppr reste géré par Windows.
- Aucune vérification n’est marquée comme exécutée avant d’avoir été réalisée sur la version concernée.
