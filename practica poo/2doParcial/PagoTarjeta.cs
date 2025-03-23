using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2doParcial
{
    public class PagoTarjeta : Pago,ICloneable
    {
        private DateTime FechaDePago = DateTime.Now;
        private decimal Recargo = 0.1m;
        private decimal MontoTotal;
        public  void RecargoPago() { if (FechaDeVencimiento < FechaDePago) MontoTotal=  Importe += (Importe * Recargo); base.MarcarComoCancelado(); }

        public object Clone()=> MemberwiseClone();
        public PagoTarjeta CloneTipado() => Clone() as PagoTarjeta;
    }

}
