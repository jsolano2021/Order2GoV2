using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Order2GoV2.Models
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            ComercioUsuario = new HashSet<ComercioUsuario>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DisplayName("Usuario")]
        public string UserName { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DisplayName("Contraseña")]
        public string Clave { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DisplayName("Perfil")]
        public int IdPerfil { get; set; }

        public virtual Perfil IdPerfilNavigation { get; set; }
        public virtual ICollection<ComercioUsuario> ComercioUsuario { get; set; }
    }
}
