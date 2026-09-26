# Architecture

**Statut : Brouillon** — **Dernière mise à jour : 2026-09-26**

## Choix

C# avec WinForms et .NET 8 pour s’appuyer sur les API Windows sans bibliothèque tierce. La publication autonome en fichier unique permet une utilisation portable.

## Composants

- `Program.cs` initialise l’application Windows Forms.
- `MainForm.cs` gère l’interface, le délai, la zone de notification et les transitions verrouillé/déverrouillé.
- `KeyboardHook.cs` installe et retire le hook `WH_KEYBOARD_LL`.
- Le réglage `Alt+F4` décide si l’état Alt/F4 est transmis à Windows pendant le verrouillage. Les autres frappes sont bloquées.

Le hook est maintenu par la boucle de messages de l’interface. Le timer vérifie l’échéance. À l’arrêt normal, le hook est retiré ; en cas d’arrêt du processus, Windows le retire avec le processus.

## Compilation et publication

La GitHub Action compile les pull requests en lecture seule et compile/publie une release Windows x64 à chaque push sur la branche par défaut. Le tag utilise la version à quatre composants du projet, synchronisée avec le manifeste Windows, et le numéro d’exécution GitHub : `v<version>_build-<numéro>`, par exemple `v1.0.0.6_build-2`. La permission `contents: write` est limitée au travail de publication.
