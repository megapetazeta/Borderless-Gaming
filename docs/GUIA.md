# Borderless Gaming: compilar y usar sin instalar nada

Probado el 23-10-2026 (actualizada con la versión en español) en Windows 11 (build 26200) con VS 2019 Community, sin permisos de admin. Repo: https://github.com/andrewmd5/Borderless-Gaming (commit d6a541a, versión 9.6.0).

## Qué es
App WinForms (C#, .NET Framework 4.8, x86) que le quita el borde a la ventana de un juego en modo ventana y la estira a la resolución del monitor. Así tienes "pantalla completa" con alt-tab instantáneo, porque el juego nunca toma control exclusivo del display. Por dentro usa Win32: `SetWindowLong` para quitar WS_CAPTION/WS_THICKFRAME y `SetWindowPos` para reposicionar y redimensionar.

## Opción A: descargar la versión en español ya compilada (lo más fácil)
Repo (público, fork personal para uso de un amigo, rama por defecto `foxito`): https://github.com/megapetazeta/Borderless-Gaming
Release: https://github.com/megapetazeta/Borderless-Gaming/releases/tag/v9.6.0-es

1. Descarga `Borderless-Gaming-9.6.0-es-portable.zip` y descomprímelo donde quieras.
2. Ejecuta `BorderlessGaming.exe`. Sin instalar nada, sin admin. La interfaz viene en español por defecto.
3. **`Languages.zip` tiene que estar al lado del .exe** la primera vez: la app lo extrae a su carpeta de datos y luego lo borra. Si falta y nunca se extrajo, se cierra con "Faltan las traducciones de la interfaz en el disco".

## Opción A2: Steam
En Steam (app 388080) la venden ya compilada y firmada por el autor (en inglés por defecto).

## Opción B: compilarla tú (gratis, GPL v2)

### Requisitos
- Git
- MSBuild de Visual Studio 2017/2019/2022, o "Build Tools for Visual Studio" con la carga ".NET desktop build tools"
- PowerShell
- **No** hace falta instalar el .NET 4.8 Developer Pack (ver paso 2)

### 1. Clonar
```powershell
cd $env:USERPROFILE\Documents
git clone -b foxito https://github.com/megapetazeta/Borderless-Gaming.git   # versión en español
# (o el original: git clone --depth 1 https://github.com/andrewmd5/Borderless-Gaming.git)
```

### 2. Referencias de .NET 4.8 sin instalador
Si compilas tal cual sale `error MSB3644: No se encuentran los ensamblados de referencia para .NETFramework,Version=v4.8`. En vez de instalar el Developer Pack (pide admin), baja el paquete NuGet con las reference assemblies, que es solo un zip:
```powershell
Invoke-WebRequest https://www.nuget.org/api/v2/package/Microsoft.NETFramework.ReferenceAssemblies.net48/1.0.3 -OutFile ref48.zip
Expand-Archive ref48.zip ref48
```

### 3. Compilar
```powershell
cd Borderless-Gaming-ES
$mb = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"   # ajusta según tu VS
$fp = "$env:USERPROFILE\Documents\ref48\build\.NETFramework\v4.8"
& $mb BorderlessGaming.sln /restore /p:Configuration=Release /p:PostBuildEvent= "/p:FrameworkPathOverride=$fp" /m /v:m
Copy-Item Languages\Languages.zip BorderlessGaming\bin\Release\   # ¡obligatorio!
```
Qué hace cada flag:
- `/restore`: baja los paquetes NuGet (CommandLineParser, protobuf-net, TaskScheduler).
- `/p:PostBuildEvent=`: anula el post-build, que intenta firmar el .exe con `Tools\DigiCertUtil.exe` y el certificado del autor. Sin esto el build falla en cualquier PC que no sea el suyo.
- `FrameworkPathOverride`: apunta a las referencias del paso 2.

Un warning MSB3245 sobre `DotNetZip` es normal: la referencia está muerta y no afecta el resultado. El build tarda alrededor de 50 s.

### 4. Resultado
`BorderlessGaming\bin\Release\` queda con:
- `BorderlessGaming.exe` (unos 370 KB), `.exe.config` y `BorderlessGaming.Logic.dll`
- `CommandLine.dll`, `protobuf-net.dll`, `Microsoft.Win32.TaskScheduler.dll`
- carpetas de recursos satélite (`es`, `en`, ...)
- `Languages.zip` (cópialo tú desde `Languages\`, MSBuild no lo hace)

Esa carpeta completa es portable: cópiala a un pendrive o a donde quieras. El runtime 4.8 ya viene en Windows 10 y 11. Los `.pdb` se pueden borrar.

## Uso
1. Pon el juego en **modo ventana** (no "pantalla completa exclusiva"). Si el juego tiene "borderless" nativo, no necesitas esta app.
2. Abre `BorderlessGaming.exe`. A la izquierda aparecen las ventanas abiertas.
3. Selecciona el juego y presiona la flecha `>` para agregarlo a **Favoritos**. Desde ahí la app lo detecta y le quita el borde cada vez que lo abres.
4. Clic derecho sobre el favorito para ajustar: tamaño o posición manual, ocultar la barra de tareas, mantener encima, monitor destino, etc.
5. En Opciones puedes dejarlo iniciando con Windows, minimizado a la bandeja. Ojo: eso crea una tarea programada (por eso usa `TaskScheduler.dll`), así que ya no es "cero invasivo".

## Problemas comunes
- **Juegos con anticheat** (Vanguard, EAC, BattlEye): cambiar el estilo de ventana suele estar permitido, pero no hay garantía. Úsalo bajo tu responsabilidad en online competitivo.
- **SmartScreen o antivirus**: el .exe no está firmado (desactivamos la firma), así que Windows puede avisar. Es esperable porque lo compilaste tú.
- **Juego elevado (admin)**: si el juego corre como admin, Borderless Gaming también tiene que hacerlo. Por UIPI, un proceso normal no puede tocar ventanas de uno elevado.
- **DPI/escalado**: si la ventana queda corrida o recortada, ve a Propiedades del .exe del juego > Compatibilidad > Cambiar config. de PPP alto > "Aplicación".
- **No aparece la ventana**: actualiza la lista o revisa que el juego no esté en fullscreen exclusivo.

## Desinstalar
Borra la carpeta. Si activaste el inicio con Windows, desactívalo antes en Opciones o elimina la tarea en el Programador de tareas. La configuración queda en `%AppData%\Andrew Sampson\Borderless Gaming` (o similar); bórrala si quieres limpiar todo.
