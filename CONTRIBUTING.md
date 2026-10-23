# Cómo contribuir

Te animamos a contribuir al repositorio. Queremos que sea lo más fácil posible, pero hay algunas cosas a tener en cuenta. Sigue estas pautas si quieres enviar un pull request.

## Preparación

* Necesitas una [cuenta de GitHub](https://github.com/signup/free).
* Abre un [issue](https://github.com/andrewmd5/Borderless-Gaming/issues) para tu problema si todavía no existe uno.
	* Describe el problema e incluye los pasos para reproducirlo si es un error.
	* Indica la versión más antigua que sabes que está afectada.
* Si puedes y quieres corregirlo, haz un fork del repositorio en GitHub.

## Hacer cambios

* En tu fork, crea una rama temática para tu parche (por ejemplo, `mejor-deteccion-favoritos` o `fix-error-al-editar-ventana`).
	* Normalmente se basa en la rama principal.
	* Crea la rama con `git branch fix/master/mi_contribucion master` y cámbiate a ella con `git checkout fix/master/mi_contribucion`. Evita trabajar directamente sobre la rama `master`.
* Respeta el estilo de código que ya se usa.
* Haz commits por unidades lógicas y descríbelos bien.
* Sintaxis:
    * Dos espacios, sin tabulaciones.
    * Sin espacios al final de las líneas. Las líneas en blanco no deben tener espacios.
    * `a = b` y no `a=b`.
    * Sigue las convenciones que ya ves en el código.

## Enviar cambios

* Sube tus cambios a una rama temática de tu fork.
* Abre un pull request contra el repositorio original y elige la rama correcta que quieres modificar.
* Si no lo hiciste en los mensajes de commit (que es lo recomendable), referencia y actualiza tu issue con los cambios de código. Pero _por favor no cierres el issue tú mismo_.
* Aunque tengas permisos de escritura en el repositorio, no hagas push ni merge directo de pull requests. Deja que otro miembro del equipo revise y apruebe tu pull request.

# Recursos adicionales

* [Documentación general de GitHub](https://docs.github.com/es)
* [Documentación sobre pull requests](https://docs.github.com/es/pull-requests)
