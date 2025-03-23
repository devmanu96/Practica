using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2doParcial
{
    internal class Negocio : ICloneable
    {
        List<Proveedor> ListaProveedores;
        List<Pago> ListaPagosAP;
        public Negocio() { ListaProveedores = new List<Proveedor>(); ListaPagosAP = new List<Pago>(); }
        public string Codigo { get; set; }// El codigo seran de 3 caracteres ej: "A10" 1 letra y 2 numeros
        public string RazonSocial { get; set; }
        public int Telefono { get; set; }
        public string Direccion { get; set; }

        public object Clone() => MemberwiseClone();

        public Negocio CloneTipado()
        {
            Negocio aux = (Negocio)Clone();
            return aux;
        }

        public void AgregarProveedorANegocio(Proveedor pPoveedor)
        {
            ListaProveedores.Add(pPoveedor);
        }
        public List<Proveedor> RetornaProveedoresDeNegocios()
        {
            List<Proveedor> aux = new List<Proveedor>();
            foreach (var x in ListaProveedores)
            {
                aux.Add(x.CloneTipado());
            }
            return aux;
        }
        public List<Pago> RetornaProveedoresDeNegociosConPagos()
        {
            List<Pago> aux = new List<Pago>();
            foreach (var x in ListaProveedores)
            {
                var t = x.RetornaPago().FindAll(x => x.CodigoNumerico != null);
                aux = t;
            }
            return aux;
        }
        public Proveedor RetornarUnicoProveedor(Proveedor p)
        {
            return ListaProveedores.Find(x => x.Legajo == p.Legajo);
        }

        public void AgregarPagoAP(Pago pago)
        {
            ListaPagosAP.Add(pago);
        }

        public List<Pago> RetornaPagosAdeudados()
        {
            List<Pago> aux = new List<Pago>();
            foreach (var x in ListaPagosAP)
            {
                aux.Add((Pago)x);
            }
            return aux;
        }
        public List<Proveedor> RetornaProveedoresDeNegociosAux() => ListaProveedores;
        public void BorrarProveedorDeNegocio(Proveedor pProveedor) => ListaProveedores.Remove(pProveedor);
        public void CancelarPago(Pago pPago)
        {
            var x = ListaPagosAP.Find(x => x.CodigoNumerico == pPago.CodigoNumerico);
            ListaPagosAP.Remove(x);
            ListaPagosAP.Add(pPago);
        }
        
    }
}
