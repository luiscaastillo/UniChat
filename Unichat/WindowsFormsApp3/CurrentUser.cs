using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unichat;

namespace WindowsFormsApp3
{ 
  public static class CurrentUser //Clase para guardar el usuario actual para manipular todo lo relacionado con este.
    {
        public static int IdUser { get; private set; }
        public static string Username { get; private set; }

        // Método para establecer el usuario actual al iniciar sesión
        public static void SetCurrentUser(int idUser, string username)
        {
            IdUser = idUser;
            Username = username;
        }

        //Si se implementa CERRAR SESION usar ESTE bloque de codigo
        /*public static void Clear()
        {
            IdUser = null;
            Username = null;
        }
        */
    }


}
