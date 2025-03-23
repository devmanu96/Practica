using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace _2doParcial
{
    internal class Proveedor : ICloneable
    {
        List<Negocio> ListaNegocios;
        List<Pago> ListaPagosDeN;
        public Proveedor() { ListaNegocios = new List<Negocio>();ListaPagosDeN = new List<Pago>(); }
        
        public int Legajo { get; set; }
        public string Nombre { get; set; }
        public int Telefono { get; set; }


        public object Clone() => MemberwiseClone();

        public Proveedor CloneTipado()
        {
            Proveedor aux = (Proveedor)Clone();
            return aux;
        }

        public void AgregarNegocioAProveedor(Negocio pNegocio)
        {
            ListaNegocios.Add(pNegocio);
        }
        public void BorrarNegocioDeProveedor(Negocio pNegocio) => ListaNegocios.Remove(pNegocio);
        public List<Negocio> RetornaNegociosDeProveedores()
        {
            List<Negocio> aux = new List<Negocio>();
            foreach(var x in ListaNegocios)
            {
                aux.Add(x.CloneTipado());
            }
            return aux;
        }
        public Negocio RetornarUnicoNegocio(Negocio n)
        {
            return ListaNegocios.Find(x => x.Codigo == n.Codigo);
        }

        public void AgregarPagoDeN(Pago p)
        {
            ListaPagosDeN.Add((Pago)p);
        }
        public List<Pago> RetornaPago()
        {
            List<Pago> aux = new List<Pago>();

            foreach(var x in ListaPagosDeN)
            {
                aux.Add((Pago)x);
            }
            return aux;
        }
        public List<Negocio> RetornaNegociosDeProveedoresAux() => ListaNegocios;
        public List<Pago> RetornaPagoAux() => ListaPagosDeN;
        public void CancelarPago(Pago pPago)
        {
            var x = ListaPagosDeN.Find(x => x.CodigoNumerico == pPago.CodigoNumerico);
            ListaPagosDeN.Remove(x);
            ListaPagosDeN.Add(pPago);
        }
        
    }
}
