# Sécurité

**Statut : Brouillon** — **Dernière mise à jour : 2026-09-26**

## Niveau

Niveau A : utilitaire local sans compte, réseau, stockage de données ni privilège administrateur.

## Risques et contrôles

- **Clavier laissé bloqué :** libération à la fermeture, échéance réglable et accès au déverrouillage par souris via la fenêtre ou le menu de notification.
- **Filtre trop large :** toutes les frappes, y compris les touches Windows, sont filtrées ; Alt+F4 peut être autorisé explicitement. Ctrl+Alt+Suppr et les écrans sécurisés sont contrôlés par Windows et ne sont pas interceptés.
- **Élévation ou application privilégiée :** l’application s’exécute en utilisateur standard ; la couverture peut différer pour les fenêtres élevées.
- **Arrêt brutal :** le hook est attaché au processus et Windows le retire lorsque celui-ci s’arrête.

Aucun secret ou contenu de frappe n’est collecté ni conservé. Le callback ne journalise pas les touches.

## Publication continue

Le workflow GitHub n’utilise que le jeton éphémère du dépôt avec `contents: write` pour publier une release. Les actions externes sont épinglées à des révisions précises. Les contributions par pull request compilent sans permission d’écriture.
