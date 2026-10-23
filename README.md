# Borderless Gaming (fork en español)

> **Aviso:** este es un **fork personal**, traducido al español y hecho para que lo use un amigo. **No tiene soporte oficial** ni relación con el autor.
> El proyecto original es de **Andrew Sampson ([andrewmd5](https://github.com/andrewmd5))**: [andrewmd5/Borderless-Gaming](https://github.com/andrewmd5/Borderless-Gaming), bajo licencia [GNU GPL v2.0](LICENSE). Si te sirve, apoya al autor comprándolo en [Steam](https://store.steampowered.com/app/388080/Borderless_Gaming/).

## ¿Qué es?

Borderless Gaming es una herramienta sencilla que convierte tus juegos en modo ventana en aplicaciones a "pantalla completa", sin los efectos negativos de la pantalla completa exclusiva (alt-tab lento, parpadeos, cambios de resolución).

![Antes de Borderless Gaming](http://cdn.akamai.steamstatic.com/steam/apps/388080/ss_a599d209638e8db8af430b7e9028c490c3c24f78.1920x1080.jpg?t=1437672493)

![Después de Borderless Gaming](http://cdn.akamai.steamstatic.com/steam/apps/388080/ss_3f6206f211ea573e6013665f5bca126c6ba895ce.1920x1080.jpg?t=1437672493)

## Qué cambia en este fork

- Interfaz, mensajes, recursos y documentación en **español (es-419)**, y el español es el idioma por defecto. Los demás idiomas siguen disponibles en *Opciones > Seleccionar idioma*.
- Compilación sin instalar nada (sin Developer Pack ni permisos de admin).
- Se omite la firma del autor (DigiCert), así que el `.exe` **no está firmado**.
- Dicen que alguien peludito vive en *Acerca de*. 🐾

## Descarga (portable)

En [Releases](../../releases) hay un `.zip` portable. Descomprímelo en cualquier carpeta y ejecuta `BorderlessGaming.exe`. Requiere .NET Framework 4.8, que ya viene incluido en Windows 10 y 11.

**Importante:** `Languages.zip` tiene que estar junto al `.exe`. Al primer inicio la app lo extrae a su carpeta de datos y lo borra; si falta y no hay idiomas extraídos, la app avisa que faltan las traducciones y se cierra.

## Compilar desde el código

### Requisitos
- Git y PowerShell
- MSBuild de Visual Studio 2017/2019/2022, o *Build Tools for Visual Studio* con ".NET desktop build tools"
- **No** hace falta el .NET Framework 4.8 Developer Pack

### Pasos
```powershell
cd $env:USERPROFILE\Documents
git clone -b foxito https://github.com/megapetazeta/Borderless-Gaming.git
# Ensamblados de referencia de .NET 4.8 como paquete NuGet (es solo un zip)
Invoke-WebRequest https://www.nuget.org/api/v2/package/Microsoft.NETFramework.ReferenceAssemblies.net48/1.0.3 -OutFile ref48.zip
Expand-Archive ref48.zip ref48

cd Borderless-Gaming
$mb = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"   # ajusta a tu VS
$fp = "$env:USERPROFILE\Documents\ref48\build\.NETFramework\v4.8"
& $mb BorderlessGaming.sln /restore /p:Configuration=Release /p:PostBuildEvent= "/p:FrameworkPathOverride=$fp" /m /v:m
Copy-Item Languages\Languages.zip BorderlessGaming\bin\Release\
```

- `/restore`: descarga los paquetes NuGet (CommandLineParser, protobuf-net, TaskScheduler).
- `/p:PostBuildEvent=`: desactiva el post-build que firma el `.exe` con `Tools\DigiCertUtil.exe` y el certificado del autor; sin esto falla en cualquier otro PC.
- `FrameworkPathOverride`: usa las referencias de `Microsoft.NETFramework.ReferenceAssemblies.net48` 1.0.3 en vez del Developer Pack (evita el error MSB3644).
- La advertencia **MSB3245** sobre `DotNetZip` es inofensiva: es una referencia muerta que no afecta el resultado.
- Si editas algún `Languages\*.lang` (UTF-8 sin BOM, formato `clave|valor`), regenera `Languages.zip`:
  `Compress-Archive Languages\*.lang Languages\Languages.zip -Force`

El resultado queda en `BorderlessGaming\bin\Release\`. Esa carpeta (sin los `.pdb`, con `Languages.zip`) es portable.

## Uso

1. Pon el juego en **modo ventana** (no pantalla completa exclusiva).
2. Abre `BorderlessGaming.exe`; a la izquierda aparecen las ventanas abiertas.
3. Selecciona el juego y agrégalo a **Favoritos**: desde ahí la app le quita los bordes automáticamente cada vez que se abre.
4. Clic derecho sobre un favorito para más opciones: tamaño o posición manual, ocultar la barra de tareas, siempre visible, monitor de destino, etc.
5. *Ejecutar al iniciar Windows* crea una tarea programada; si quieres que sea 100 % portable, no la actives.

## Notas técnicas

- App WinForms en C#, .NET Framework 4.8, x86.
- Quita los estilos `WS_CAPTION`/`WS_THICKFRAME` con `SetWindowLong` (Win32) y reposiciona/redimensiona la ventana al tamaño del monitor con `SetWindowPos`.
- **UIPI:** si el juego corre elevado (como administrador), Borderless Gaming también debe ejecutarse como administrador; un proceso con integridad normal no puede modificar ventanas de uno elevado.
- Juegos con anticheat: cambiar el estilo de ventana suele estar permitido, pero no hay garantía.
- El `.exe` no está firmado, así que SmartScreen o el antivirus pueden advertir.

## Licencia

GNU General Public License v2.0, igual que el original. Ver [LICENSE](LICENSE). Créditos completos al autor original y sus colaboradores.
