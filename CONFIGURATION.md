# 🔧 Configuración de GymNet

## Configuración Requerida

Antes de ejecutar la aplicación, necesitas configurar tus credenciales de Firebase.

### 1. Archivo de Configuración

Edita el archivo `GymNet.Presentation/appsettings.json` y reemplaza los valores de placeholder:

```json
{
  "Firebase": {
    "ApiKey": "BJ_GbD466df-n6clDTW20QDHd7z-U03d3CLMh7feObSgpyt89RcniaPkFBFhx2XsNIQJA0v3QctHMcG_Fgbn-hg",
    "ProjectId": "gymnet-social"
  }
}
```

### 2. Obtener tu Firebase API Key

1. Ve a [Firebase Console](https://console.firebase.google.com/)
2. Selecciona tu proyecto "gymnet-social" (o crea uno nuevo)
3. Ve a **Configuración del proyecto** (⚙️ icono)
4. En la pestaña **General**, busca "Web API Key"
5. Copia ese valor y pégalo en `appsettings.json`

### 3. Seguridad

> ⚠️ **IMPORTANTE**: El archivo `appsettings.json` está en `.gitignore` y NO debe subirse a git.
> 
> Esto protege tu API Key de ser expuesta públicamente.

### 4. Despliegue

Para producción, considera:
- Usar variables de entorno
- Azure Key Vault / Google Secret Manager
- Configuración específica por entorno (Development, Staging, Production)

---

## Cambios Realizados

### ✅ Errores Corregidos

1. **Compilación XAML**: Corregidos 6 errores en `ProfilePage.xaml`
2. **Seguridad**: API Key movida a configuración externa
3. **HttpClient**: Ahora usa IHttpClientFactory (mejores prácticas)

### 📝 Archivos Modificados

- `ProfilePage.xaml` - Sintaxis Border corregida
- `MauiProgram.cs` - Carga de configuración segura
- `DependencyInjection.cs` - HttpClient factory
- `.gitignore` - Excluye appsettings.json
- `appsettings.json` - Nuevo archivo de configuración
- `AppSettings.cs` - Modelos de configuración

### 🎯 Próximos Pasos Recomendados

1. **Inmediato**: Configurar tu Firebase API Key
2. **Corto plazo**: Implementar persistencia de tokens
3. **Medio plazo**: Implementar Firestore y Storage
4. **Optimización**: Agregar compiled bindings (x:DataType)
