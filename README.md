# Keyboard Locker

**Statut :** Première version — **Dernière mise à jour :** 2026-09-26

Petit utilitaire Windows portable pour bloquer les frappes du clavier pendant le nettoyage. La souris reste utilisable et une icône de notification donne accès au déverrouillage.

## Fonctionnalités

- Bouton « Verrouiller le clavier » et réglage du délai de sécurité (1 à 60 minutes, 5 par défaut).
- Déverrouillage par clic souris dans la fenêtre ou depuis le menu de l’icône de notification.
- Déverrouillage automatique à l’échéance ; fermeture de l’application libère aussi le clavier.
- Case « Autoriser Alt+F4 pendant le verrouillage », décochée par défaut.
- Windows 10/11, sans service, télémétrie ni dépendance tierce.

Le code est distribué sous **GNU GPL v3.0** ; voir [`LICENCE.md`](LICENCE.md).

**Auteur et mainteneur :** Bougli.  
**Copyright :** © 2026 Bougli.

## Compilation

Installer le SDK .NET 8 et compiler depuis Windows :

```powershell
dotnet publish .\src\KeyboardLocker\KeyboardLocker.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

L’exécutable se trouve dans `src/KeyboardLocker/bin/Release/net8.0-windows/win-x64/publish/`. Pour une autre architecture, remplacer `win-x64` par `win-arm64` ou `win-x86`. La publication autonome inclut le runtime .NET et augmente la taille du fichier ; l’application n’installe aucun service.

## Utilisation

Lancer `KeyboardLocker.exe`, choisir le délai et cliquer sur « Verrouiller le clavier ». Le clavier est alors filtré. Déverrouiller avec le bouton de la fenêtre ou clic droit sur l’icône de notification puis « Déverrouiller ». Fermer la fenêtre avec la souris libère aussi le clavier. Réduire la fenêtre la place dans la zone de notification.

## Limites techniques et sécurité

L’application utilise un hook clavier bas niveau Windows dans son propre processus. Si le processus s’arrête, Windows retire le hook. Le délai de sécurité fonctionne tant que le processus et sa boucle de messages restent actifs. Le verrouillage est un filtre logiciel, pas une garantie contre les outils privilégiés ou un arrêt forcé.

Toutes les frappes clavier, y compris les touches Windows, sont filtrées par défaut. La case d’option permet de laisser passer Alt+F4. Les séquences sécurisées gérées par Windows, comme Ctrl+Alt+Suppr, ne peuvent pas être interceptées par une application ordinaire. Certaines fenêtres élevées ou certains écrans sécurisés peuvent également échapper au filtre. Le clavier n’est donc pas bloqué de façon absolue. L’application ne demande pas les droits administrateur.

## Compilation et releases automatiques

La GitHub Action compile l’application sur les pull requests et crée une release Windows x64 à chaque push sur la branche par défaut. Les tags suivent le format `v<version>_build-<numéro>`, par exemple `v1.0.0.7_build-2`. Le workflow utilise le `GITHUB_TOKEN` du dépôt avec la permission `contents: write`; aucun secret personnel n’est requis. Voir [`.github/workflows/build-release.yml`](.github/workflows/build-release.yml).

## Documentation

Voir [`docs/Architecture.md`](docs/Architecture.md), [`docs/Security.md`](docs/Security.md), [`docs/PRD.md`](docs/PRD.md) et les autres documents du dossier `docs/`.

Voir aussi le [journal des changements](CHANGELOG.md).
