using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2doParcial
{
    public class PagoEfectivo : Pago,ICloneable
    {
        
        private decimal Recargo = 0.01M;
        private decimal MontoTotal;
        private DateTime FechaDePago = DateTime.Now;

        public object Clone()=> MemberwiseClone();
        public PagoEfectivo CloneTipado() => Clone() as PagoEfectivo;

        public void RecargoPago() { if (FechaDeVencimiento < FechaDePago) MontoTotal= Importe += (Importe * Recargo) ; base.MarcarComoCancelado(); ; }


    }
}
