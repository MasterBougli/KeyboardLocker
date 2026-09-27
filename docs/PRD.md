# Exigences produit

**Statut : Brouillon** — **Dernière mise à jour : 2026-09-26**

## Objectif

Permettre à une personne de nettoyer un clavier Windows sans le débrancher, en bloquant temporairement les frappes tout en conservant l’usage de la souris.

## Périmètre de la première version

- Windows 10/11, application de bureau portable et autonome, sans runtime .NET préinstallé ; une archive proche de 70 Mo est acceptable.
- Verrouillage depuis un bouton, délai de sécurité réglable de 1 à 60 minutes (5 minutes par défaut), déverrouillage souris et icône de notification.
- Option de mode de verrouillage pour `Alt+F4` : laisser cette combinaison fonctionner, ou la bloquer avec les autres frappes. Par défaut, `Alt+F4` est bloqué. La souris doit rester utilisable.
- Toutes les frappes clavier, y compris les touches Windows, sont bloquées par défaut ; seule l’option `Alt+F4` permet une exception contrôlée par l’utilisateur.
- Aucun service ni fonctionnement arrière-plan après fermeture.
- Compilation automatique de l’application Windows et publication d’une release GitHub à chaque push sur la branche par défaut du dépôt (actuellement l’unique branche publique). Les tags suivent le format `v<version>_build-<numéro>`, par exemple `v1.0.0.5_build-2`.

## Critères d’acceptation

- La fenêtre indique clairement l’état verrouillé et le temps restant.
- La souris reste utilisable pour déverrouiller.
- Le verrouillage se termine à l’échéance et à la fermeture de l’application.
- Les limites du mécanisme sont documentées.
- Chaque push sur la branche par défaut lance une compilation reproductible et publie l’exécutable dans une release GitHub.

## Limite technique

`Ctrl+Alt+Suppr` est une séquence sécurisée contrôlée par Windows et ne peut pas être bloquée par cette application. L’option ne portera donc que sur `Alt+F4` : autoriser ou bloquer la combinaison pendant le verrouillage.
