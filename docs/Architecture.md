# Architecture

**Statut : Brouillon** — **Dernière mise à jour : 2026-09-27**

## Choix

C# avec WinForms et .NET 8 pour s’appuyer sur les API Windows sans bibliothèque tierce. `ApplicationHighDpiMode=PerMonitorV2` et une mise en page fondée sur `TableLayoutPanel`/`FlowLayoutPanel` permettent d’adapter les contrôles au redimensionnement et au DPI de chaque écran. La publication autonome en fichier unique inclut le runtime pour éviter tout prérequis d’installation ; une archive proche de 70 Mo est acceptable. La réduction par trimming n’est pas retenue pour WinForms.

## Composants

- `Program.cs` initialise l’application Windows Forms.
- `MainForm.cs` gère l’interface, le délai, la zone de notification et les transitions verrouillé/déverrouillé.
- `KeyboardHook.cs` installe et retire le hook `WH_KEYBOARD_LL`.
- Le réglage `Alt+F4` décide si l’état Alt/F4 est transmis à Windows pendant le verrouillage. Les autres frappes sont bloquées.

Le hook est maintenu par la boucle de messages de l’interface. Le timer vérifie l’échéance toutes les 250 ms à partir de `Environment.TickCount64`, sans dépendre des changements de l’horloge civile. Sur .NET 8 pour Windows, cette horloge inclut le temps en veille et en hibernation ; si l’échéance survient pendant la veille, le déverrouillage a lieu au prochain tick de la boucle à la reprise. À l’arrêt normal, le hook est retiré ; en cas d’arrêt du processus, Windows le retire avec le processus.

## Compilation et publication

La GitHub Action compile les pull requests en lecture seule et compile/publie une release Windows x64 à chaque push sur la branche par défaut. Le tag utilise la version à quatre composants du projet, synchronisée avec le manifeste Windows, et le numéro d’exécution GitHub : `v<version>_build-<numéro>`, par exemple `v1.0.0.6_build-2`. La permission `contents: write` est limitée au travail de publication.
