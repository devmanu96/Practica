using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2doParcial
{
    internal class Administracion
    {
        List<Negocio> NegociosAdministracion;
        List<Proveedor> ProveedoresDeNegocios;
        List<Negocio> NegociosConPagosHistorial;
        
        public Administracion() { NegociosAdministracion = new List<Negocio>(); ProveedoresDeNegocios = new List<Proveedor>();NegociosConPagosHistorial = new List<Negocio>() ; }

        //Existencias de negocio
        internal bool ExisteCodigo(string pCodigo)
        {
            return NegociosAdministracion.Exists(x => x.Codigo == pCodigo);
            
        }
        internal bool ExisteNombre (string nombre)
        {
            return NegociosAdministracion.Exists(x => x.RazonSocial == nombre);
        }
        internal bool ExisteTelefono(string telefono)
        {
            return NegociosAdministracion.Exists(x => x.Telefono == Convert.ToInt32(telefono));
        }
        //EXISTE MODIFICADO 
        public bool ExisteNombreModificadoN(string nombre)//<------Negocio Razon social
        {
            List<Negocio> aux = new List<Negocio>();
            foreach(Negocio x in NegociosAdministracion)
            {
                if(x.RazonSocial != nombre ) aux.Add(x);
            }
            return aux.Exists(x => x.RazonSocial == nombre);
        }
        public bool ExisteNombreModificadoP(string nombre)//<------Proveedor Nombre
        {
            List<Proveedor> aux = new List<Proveedor>();
            foreach (Proveedor x in ProveedoresDeNegocios)
            {
                if (x.Nombre != nombre) aux.Add(x);
            }
            return aux.Exists(x => x.Nombre == nombre);
        }
        public bool ExisteTelefonoModificadoN(string telefono)//<------Negocio Telefono
        {
            List<Negocio> aux = new List<Negocio>();
            foreach (Negocio x in NegociosAdministracion)
            {
                if (x.Telefono != Convert.ToInt32(telefono)) aux.Add(x);
            }
            return aux.Exists(x => x.Telefono == Convert.ToUInt32(telefono));
        }
        public bool ExisteTelefonoModificadoP(string telefono)//<------Proveedor Telefono
        {
            List<Proveedor> aux = new List<Proveedor>();
            foreach (Proveedor x in ProveedoresDeNegocios)
            {
                if (x.Telefono != Convert.ToInt32(telefono)) aux.Add(x);
            }
            return aux.Exists(x => x.Telefono == Convert.ToUInt32(telefono));
        }

        public void ModificarRazonSocialNegocio(Negocio pNegocio)//<------Negocio Modificado
        {
            //ListaNegocio
            var n = NegociosAdministracion.Find(x => x.Codigo == pNegocio.Codigo);
            n.RazonSocial = pNegocio.RazonSocial;
            n.Telefono = pNegocio.Telefono;
            n.Direccion = pNegocio.Direccion;
            //ListaProveedores
            
            foreach(var x in ProveedoresDeNegocios)
            {
                if(x.RetornaNegociosDeProveedores().Exists(x =>x.Codigo != null && x.Codigo == pNegocio.Codigo))
                {
                    var cambio = x.RetornaNegociosDeProveedoresAux().Find(x =>(x.Codigo != null) && (x.Codigo == pNegocio.Codigo));
                    cambio.RazonSocial = pNegocio.RazonSocial;
                    cambio.Telefono = pNegocio.Telefono;
                    cambio.Direccion = pNegocio.Direccion;
                }
            }
            
        }
        public void ModificarNombreProveedor(Proveedor pProveedor)//<------Proveedor Modificado
        {
            //ListaNegocio
            var n = ProveedoresDeNegocios.Find(x => x.Legajo == pProveedor.Legajo);
            n.Nombre = pProveedor.Nombre;
            n.Telefono = pProveedor.Telefono;
            //ListaProveedores

            foreach (var x in NegociosAdministracion)
            {
                if (x.RetornaProveedoresDeNegocios().Exists(x => x.Legajo != null && x.Legajo == pProveedor.Legajo))
                {
                    var cambio = x.RetornaProveedoresDeNegociosAux().Find(x => x.Legajo != null && x.Legajo == pProveedor.Legajo);
                    cambio.Nombre = pProveedor.Nombre;
                    cambio.Telefono = pProveedor.Telefono;
                }
            }

        }

        //Existencia de Proveedor
        internal bool ExisteLegajo(int pLegajo)
        {
            return ProveedoresDeNegocios.Exists(x => x.Legajo == pLegajo);
        }
        internal bool ExisteNombreProveedor(string nombre)
        {
            return ProveedoresDeNegocios.Exists(x => x.Nombre == nombre);
        }
        internal bool ExisteTelefonoProveedor(string telefono)
        {
            return ProveedoresDeNegocios.Exists(x => x.Telefono == Convert.ToInt32(telefono));
        }
        //Agregar Negocios/Proveedores
        public void AgregarNegocio(Negocio pNegocio) => NegociosAdministracion.Add(pNegocio.CloneTipado());
        public void AgregarProveedor(Proveedor pProveedor) => ProveedoresDeNegocios.Add(pProveedor.CloneTipado());

        // BAJA Y MODIFICACION
        //--NEGOCIO---------------------------------------------
        public Negocio SeleccionarNegocio(Negocio n)
        {
            return NegociosAdministracion.Find(x => x.Codigo == n.Codigo);
            
        }
        public bool ComprobarSiExistenPagosPendientes(Negocio n)
        {
            var a = SeleccionarNegocio(n).RetornaProveedoresDeNegocios();
            var c = a.Exists(x => x.RetornaPago().Count != 0);
            return c;
        }
        public void BajaNegocio(Negocio n) 
        {
            var negocio = NegociosAdministracion.Find(x => x.Codigo == n.Codigo);
            
            foreach(var z in ProveedoresDeNegocios)
            {
                var borrarNegocios = z.RetornaNegociosDeProveedoresAux().Find(x =>(x.Codigo != null) && (x.Codigo == n.Codigo));
                z.BorrarNegocioDeProveedor(borrarNegocios);
            }
            NegociosAdministracion.Remove(negocio);
        }
        //--Proveedor------------------------------------------------
        public Proveedor SeleccionarProveedor(Proveedor p)
        {
            return ProveedoresDeNegocios.Find(x => x.Legajo == p.Legajo);
        }
        public bool ComprobarSiExistenPagosPendientesDeProveedores(Proveedor p)
        {
            var a = SeleccionarProveedor(p).RetornaNegociosDeProveedores();
            var c = a.Exists(x => x.RetornaPagosAdeudados().Count != 0);
            return c;
        }
        public void BajaProveedor(Proveedor p)
        {
            var proveedor = ProveedoresDeNegocios.Find(x => x.Legajo == p.Legajo);

            foreach (var z in NegociosAdministracion)
            {
                var borrarProveedor = z.RetornaProveedoresDeNegociosAux().Find(x => (x.Legajo != null) && (x.Legajo == p.Legajo));
                z.BorrarProveedorDeNegocio(borrarProveedor);
            }
            ProveedoresDeNegocios.Remove(proveedor);
        }


        //Retornar Negocios/Proveedores
        public List<Negocio> RetornaNegocios() => (from x in NegociosAdministracion select x.CloneTipado()).ToList<Negocio>();
        public List <Proveedor> RetornaProveedores() => (from x in ProveedoresDeNegocios select x.CloneTipado()).ToList<Proveedor>();
        public List<Proveedor> RetornaProveedoresDeNegocio(Negocio pNegocio) => (from a in NegociosAdministracion.Find(x => x.Codigo == pNegocio.Codigo).CloneTipado().RetornaProveedoresDeNegocios() select a).ToList<Proveedor>();
        public List<Negocio> RetornaNegocioDeProveedores(Proveedor pProveedor) => (from a in ProveedoresDeNegocios.Find(x => x.Legajo == pProveedor.Legajo).CloneTipado().RetornaNegociosDeProveedores() select a).ToList<Negocio>();
        public void AsignarProveedor(Negocio pNegocio, Proveedor pProveedor)
        {
            //filtro la lista de negoccios y proveedores y guardo los correspondientes en una variable 
            var n = NegociosAdministracion.Find(x => x.Codigo == pNegocio.Codigo).CloneTipado();
            var p = ProveedoresDeNegocios.Find(x => x.Legajo == pProveedor.Legajo).CloneTipado();
            
            //cargo nuevos objetos negocio y proveedor para cargarlos en sus listas
            var negocio = new Negocio() { Codigo = n.Codigo,RazonSocial=n.RazonSocial,Telefono=n.Telefono,Direccion=n.Direccion};
            var proveedor = new Proveedor() { Legajo=p.Legajo,Nombre=p.Nombre,Telefono=p.Telefono};
            n.AgregarProveedorANegocio(proveedor);
            p.AgregarNegocioAProveedor(negocio);
        }
        public bool ProveedorAsignado(Negocio pNegocio, Proveedor pProveedor)
        {
            var n = NegociosAdministracion.Find(x => x.Codigo == pNegocio.Codigo);
            var p = ProveedoresDeNegocios.Find(x => x.Legajo == pProveedor.Legajo);
            return n.RetornaProveedoresDeNegocios().Exists(x =>(x.Legajo != null) &&  (x.Legajo == p.Legajo));
        }

        //Pagos
        public void GenerarPago(Negocio pN, Proveedor pP,Pago pago)
        {
            var n = NegociosAdministracion.Find(x => x.Codigo == pN.Codigo).CloneTipado();
            var p = ProveedoresDeNegocios.Find(x => x.Legajo == pP.Legajo).CloneTipado();
            // Agrego el mismo pago al negocio y proveedor seleccionado---------------
            if (n.Codigo == p.RetornarUnicoNegocio(n).Codigo) { (n.RetornarUnicoProveedor(p)).AgregarPagoDeN(pago);p.RetornarUnicoNegocio(n).AgregarPagoAP(pago); }
            if(!NegociosConPagosHistorial.Exists(x => x.RazonSocial == pN.RazonSocial))NegociosConPagosHistorial.Add(n);
        }
        public List<Pago>DevolverPagos(Negocio n, Proveedor p) => (from a in ProveedoresDeNegocios.Find(x => x.Legajo == p.Legajo ).RetornarUnicoNegocio(n).RetornaPagosAdeudados() orderby a.FechaDeVencimiento select a).ToList<Pago>();
        //public List<object> DevolverTodosLosPagos() =>(from a in NegociosConPagosHistorial.FindAll(x => x.RetornaProveedoresDeNegocios().Find(x => x.RetornaPago().AsEnumerable<Pago>)) select new {}).ToList<object>(); 
        public List<object> DevolverTodosLosPagos()
        {
            return (from negocio in NegociosConPagosHistorial
                    from proveedor in negocio.RetornaProveedoresDeNegocios()
                    from pago in proveedor.RetornaPago()
                    orderby negocio.Codigo       // Esto devuelve una lista de pagos ordenada con el codigo del negocio
                    select new
                    {
                        CodigoNegocio = negocio.Codigo,
                        RazonSocialNegocio = negocio.RazonSocial,
                        Proveedor = proveedor.Nombre,
                        Monto = pago.Importe,
                        Fecha = pago.FechaDeVencimiento,
                        Estado = pago.Cancelado
                        
                    }).ToList<object>();
        }

        public void ActualizarPago(Negocio pNegocio,Proveedor pProveedor,Pago pPago)
        {
            //Negocios
            var n = NegociosAdministracion.Find(x => x.Codigo == pNegocio.Codigo);
            var p = n.RetornaProveedoresDeNegociosAux().Find(x => x.Legajo == pProveedor.Legajo);
            p.CancelarPago(pPago);
            //Lista NegociosConPagos
            var h = NegociosConPagosHistorial.Find(x => x.Codigo == pNegocio.Codigo);
            var k = n.RetornaProveedoresDeNegociosAux().Find(x => x.Legajo == pProveedor.Legajo);
            k.CancelarPago(pPago);
            //EnProveedores
            var proveedor = ProveedoresDeNegocios.Find(x => x.Legajo == pProveedor.Legajo);
            var PagoProveedor = proveedor.RetornaNegociosDeProveedoresAux().Find(x => x.RazonSocial == pNegocio.RazonSocial);
            PagoProveedor.CancelarPago(pPago);
        }
        public bool ExisteCodigoPago(Negocio pNegocio,Proveedor pProveedor,int codigo)
        {
            var n = NegociosAdministracion.Find(x => x.Codigo == pNegocio.Codigo);
            var p = n.RetornaProveedoresDeNegociosAux().Find(x => x.Legajo == pProveedor.Legajo);

            return p.RetornaPago().Exists(x => x.CodigoNumerico == codigo);
        }
        public void ActualizarListaConPagos(Negocio n) { NegociosConPagosHistorial.Add(n); }
    }
}
