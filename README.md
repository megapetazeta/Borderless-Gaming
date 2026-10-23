<a href="https://store.steampowered.com/app/388080/Borderless_Gaming/?utm_source=github&amp;utm_campaign=f2p" target="_blank">
  <div style="width: 600px; border: 1px solid #ccc; border-radius: 4px; overflow: hidden;">
    <img src="https://production.worker-lingering-dust-86c7.andrew-03e.workers.dev" style="width: 100%; height: 500px;"></img>
  </div>
</a>

# Borderless Gaming (edición en español)

Borderless Gaming es una herramienta sencilla que convierte tus juegos en modo ventana en aplicaciones de "pantalla completa", sin los efectos negativos de la pantalla completa exclusiva: el Alt-Tab es instantáneo y puedes usar otros monitores sin que el juego se minimice.

Este repositorio es una versión traducida y adaptada del proyecto original de **Andrew Sampson ([andrewmd5](https://github.com/andrewmd5/Borderless-Gaming))**. Cambios respecto al original:

- Interfaz, mensajes y documentación en español (es-419). El español es el idioma por defecto; los demás idiomas siguen disponibles en *Opciones > Seleccionar idioma*.
- Instrucciones para compilar sin instalar nada (sin Developer Pack de .NET 4.8 y sin la firma digital del autor).
- Se dice que alguien muy peludito vive en *Acerca de*. 🐾

El proyecto es software libre bajo la [Licencia Pública General de GNU v2.0](LICENSE). Todo el mérito del programa es de su autor original y sus colaboradores.

![Antes de Borderless Gaming](http://cdn.akamai.steamstatic.com/steam/apps/388080/ss_a599d209638e8db8af430b7e9028c490c3c24f78.1920x1080.jpg?t=1437672493)

![Después de Borderless Gaming](http://cdn.akamai.steamstatic.com/steam/apps/388080/ss_3f6206f211ea573e6013665f5bca126c6ba895ce.1920x1080.jpg?t=1437672493)

![Imagen](http://cdn.akamai.steamstatic.com/steam/apps/388080/ss_0bb8cabe18249b7fdc16376723e0fd6110bd7c4e.1920x1080.jpg?t=1437672493)

## Descarga (portable)

En la sección **Releases** de este repositorio hay un zip portable. Descomprímelo en cualquier carpeta (o en un pendrive) y ejecuta `BorderlessGaming.exe`. No requiere instalación ni permisos de administrador: Windows 10 y 11 ya traen el runtime de .NET Framework 4.8.

> Importante: `Languages.zip` debe estar junto al `.exe`. En el primer arranque se extrae a la carpeta de datos; si falta (y no existe esa carpeta), la aplicación se cierra con el error "Faltan las traducciones de la interfaz en el disco".

La versión oficial, compilada y firmada por el autor, se puede [comprar en Steam](http://store.steampowered.com/app/388080).

## Compilar desde el código fuente

### Requisitos

- Git
- MSBuild de Visual Studio 2017/2019/2022, o *Build Tools for Visual Studio* con la carga de trabajo ".NET desktop build tools"
- PowerShell
- **No** hace falta instalar el *.NET Framework 4.8 Developer Pack*

### 1. Clonar

```powershell
git clone https://github.com/megapetazeta/Borderless-Gaming-ES.git
cd Borderless-Gaming-ES
```

### 2. Ensamblados de referencia de .NET 4.8 sin instalador

Sin el Developer Pack, MSBuild falla con `error MSB3644` (no encuentra los ensamblados de referencia de `.NETFramework,Version=v4.8`). En lugar de instalarlo (pide administrador), descarga el paquete NuGet que solo contiene esas referencias; es un zip:

```powershell
Invoke-WebRequest https://www.nuget.org/api/v2/package/Microsoft.NETFramework.ReferenceAssemblies.net48/1.0.3 -OutFile ref48.zip
Expand-Archive ref48.zip ref48
```

### 3. Compilar

```powershell
$mb = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"   # ajústalo a tu VS
$fp = "$PWD\ref48\build\.NETFramework\v4.8"
& $mb BorderlessGaming.sln /restore /p:Configuration=Release /p:PostBuildEvent= "/p:FrameworkPathOverride=$fp" /m /v:m
Copy-Item Languages\Languages.zip BorderlessGaming\bin\Release\
```

Qué hace cada parámetro:

- `/restore`: descarga los paquetes NuGet (CommandLineParser, protobuf-net, TaskScheduler).
- `/p:PostBuildEvent=`: anula el evento posterior a la compilación, que intenta firmar el `.exe` con `Tools\DigiCertUtil.exe` y el certificado DigiCert del autor. Sin esto la compilación falla en cualquier otro PC.
- `/p:FrameworkPathOverride=...`: apunta a las referencias del paso 2.

La advertencia `MSB3245` sobre `DotNetZip` es inofensiva: es una referencia sin uso y no afecta el resultado.

Si cambias algún archivo `.lang`, regenera el zip: `Compress-Archive Languages\*.lang Languages\Languages.zip -Force`.

### 4. Resultado

`BorderlessGaming\bin\Release\` contiene `BorderlessGaming.exe`, `BorderlessGaming.Logic.dll`, `CommandLine.dll`, `protobuf-net.dll`, `Microsoft.Win32.TaskScheduler.dll`, las carpetas de recursos satélite y `Languages.zip`. Esa carpeta es portable; los `.pdb` se pueden borrar.

## Uso

1. Pon el juego en **modo ventana** (no en pantalla completa exclusiva).
2. Abre `BorderlessGaming.exe`. A la izquierda aparecen las ventanas abiertas.
3. Selecciona el juego y agrégalo a **Favoritos**. Desde ese momento la aplicación lo detecta y le quita los bordes automáticamente cada vez que lo abres.
4. Con clic derecho sobre un favorito puedes ajustar el tamaño o la posición, ocultar la barra de tareas, dejarlo siempre visible, elegir el monitor, etc.
5. *Opciones > Ejecutar al iniciar Windows* crea una tarea programada; si quieres que siga siendo 100 % portable, no la actives.

La configuración se guarda en `%AppData%\Andrew Sampson\Borderless Gaming`.

## Notas técnicas

- Es una aplicación WinForms en C# para .NET Framework 4.8 (x86).
- Quita los estilos `WS_CAPTION`, `WS_THICKFRAME`, etc. con `SetWindowLong` y luego reposiciona y redimensiona la ventana al tamaño del monitor con `SetWindowPos`. El juego sigue en modo ventana, por eso el Alt-Tab es instantáneo.
- **UIPI:** si el juego se ejecuta como administrador, Borderless Gaming también debe hacerlo; un proceso sin elevar no puede modificar ventanas de un proceso elevado (el título muestra `[Administrador]` cuando corre elevado).
- Los juegos con anticheat (EAC, BattlEye, Vanguard) normalmente lo toleran, pero no hay garantía en partidas competitivas.
- Como el `.exe` compilado no está firmado, SmartScreen o el antivirus pueden mostrar una advertencia.

## Contribuir

Revisa la [guía de contribución](CONTRIBUTING.md). Para errores o mejoras del programa en sí, considera reportarlos también en el [repositorio original](https://github.com/andrewmd5/Borderless-Gaming/issues).

## Créditos

- Autor original y mantenedor: **Andrew Sampson** ([@andrewmd5](https://github.com/andrewmd5)) — [andrew.im](https://andrew.im)
- Colaboradores del proyecto original: ver *Acerca de* en la aplicación.
- [Página de Borderless Gaming en Steam](https://steamcommunity.com/app/388080/discussions/)

Licencia: GNU GPL v2.0. Esta versión modificada se distribuye bajo la misma licencia.
