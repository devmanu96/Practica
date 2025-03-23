using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2doParcial
{
    public  class Pago
    {
        public event EventHandler PagoExedido;
        private decimal monto;
        public int CodigoNumerico { get; set; }
        public DateTime FechaDeVencimiento { get; set; }
        public decimal Importe
        {
            get { return monto; }
            set { monto = value; if (Importe > 10000) PagoExedido?.Invoke(this, new EventArgs()); }
        }
        public bool Cancelado { get; set; }
        public Pago() { Cancelado = false; }

        public virtual void MarcarComoCancelado()
        {
            if (!Cancelado)
            {
                Cancelado = true;
            }
        }
       
    }
}
