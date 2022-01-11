# WixInstallerPOC
This project is a POC done for the WIX installer setup using .net core 3.1 WPF application.
The project has 2 different ways to create the installer.
1. WIX Manual bundle of files
2. WIX automated bundle of files using HEAT extension.

In the solution there is 3rd project that uses WIX bootstrapper bundle to install the prerequisities silently post creation of the exe from it.

Please note, the solution uses a simple logging mechanism by writing in log file created in D drive. In case D drive does not exists, please change the path accordingly.