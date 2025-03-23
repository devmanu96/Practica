using Microsoft.VisualBasic;

namespace _2doParcial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            admin = new Administracion();
        }

        Administracion admin;
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.MultiSelect = false;
            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView4.MultiSelect = false;
            dataGridView5.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView5.MultiSelect = false;
            dataGridView6.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView6.MultiSelect = false;
        }
        private void Mostrar(DataGridView pDGV, object pO)// <------- FUNCION MOSTRAR
        {
            pDGV.DataSource = null; pDGV.DataSource = pO;
        }

        // CREAR NEGOCIO
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //instancio un nuevo negocio
                Negocio n = new Negocio();
                //valido codigo
                string codigo = Interaction.InputBox("Ingresar Codigo del Negocio: ").ToUpper();
                if (admin.ExisteCodigo(codigo)) throw new Exception("El codigo ingresado ya esta en uso");
                if (codigo.Length != 3) throw new Exception("El codigo debe ser del TIPO: A12");
                if (!(char.IsLetter(codigo[0]) && char.IsDigit(codigo[1]) && char.IsDigit(codigo[2]))) throw new Exception("El codigo debe ser del TIPO: A12");
                //Nombre
                string nombre = Interaction.InputBox("Nombre de Negocio: ");
                if (admin.ExisteNombre(nombre)) throw new Exception($"El negocio {nombre} actualmente esta registrado");
                //telefono
                string telefono = Interaction.InputBox("Ingresar numero de telefono: ");
                if (!Information.IsNumeric(telefono)) throw new Exception("Caracter invalido");
                if (admin.ExisteTelefono(telefono)) throw new Exception($"El telefono {telefono} ya se encuentra registrado");
                //direccion
                string dir = Interaction.InputBox("Indicar la direccion del negocio");

                //cargo el negocio
                n.RazonSocial = nombre.ToUpper();
                n.Codigo = codigo;
                n.Telefono = Convert.ToInt32(telefono);
                n.Direccion = dir;

                admin.AgregarNegocio(n);
                Mostrar(dataGridView1, admin.RetornaNegocios());

                if (dataGridView1.Rows.Count == 0) { dataGridView5.DataSource = null; dataGridView3.DataSource = null; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        // CREAR PROVEEDOR
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //instancio un nuevo proveedor
                Proveedor p = new Proveedor();
                //valido codigo
                string codigo = Interaction.InputBox("Ingresar Legajo del Proveedor: ");
                if (!Information.IsNumeric(codigo)) throw new Exception("El legajo debe ser numerico");
                if (admin.ExisteLegajo(Convert.ToInt32(codigo))) throw new Exception("El legajo ingresado ya esta en uso");

                //Nombre
                string nombre = Interaction.InputBox("Nombre de Proveedor: ").ToUpper();

                if (admin.ExisteNombreProveedor(nombre)) throw new Exception($"El Proveedor {nombre} actualmente esta registrado");
                //telefono
                string telefono = Interaction.InputBox("Ingresar numero de telefono: ");
                if (!Information.IsNumeric(telefono)) throw new Exception("Caracter invalido");
                if (admin.ExisteTelefonoProveedor(telefono)) throw new Exception($"El telefono {telefono} ya se encuentra registrado");

                //cargo el Proveedor
                p.Nombre = nombre.ToUpper();
                p.Legajo = Convert.ToInt32(codigo);
                p.Telefono = Convert.ToInt32(telefono);

                admin.AgregarProveedor(p);
                Mostrar(dataGridView2, admin.RetornaProveedores());
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        // BOTON ----------------> ASIGNAR NEGOCIO/PROVEEDOR
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (admin.ProveedorAsignado(dataGridView1.SelectedRows[0].DataBoundItem as Negocio, dataGridView2.SelectedRows[0].DataBoundItem as Proveedor)) throw new Exception("El Negocio ya tiene asignado al proveedor");
                admin.AsignarProveedor(dataGridView1.SelectedRows[0].DataBoundItem as Negocio, dataGridView2.SelectedRows[0].DataBoundItem as Proveedor);
                Mostrar(dataGridView1, admin.RetornaNegocios());

                Mostrar(dataGridView4, admin.RetornaNegocioDeProveedores(dataGridView2.SelectedRows[0].DataBoundItem as Proveedor));
                Mostrar(dataGridView3, admin.RetornaProveedoresDeNegocio(dataGridView1.SelectedRows[0].DataBoundItem as Negocio));
                Mostrar(dataGridView5, admin.DevolverPagos(dataGridView1.SelectedRows[0].DataBoundItem as Negocio, dataGridView3.SelectedRows[0].DataBoundItem as Proveedor));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // EVENTO AL SELECCIONAR UNA FILA EN LA GRILLA 1
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {

                    Mostrar(dataGridView3, admin.RetornaProveedoresDeNegocio(dataGridView1.SelectedRows[0].DataBoundItem as Negocio));
                    if (dataGridView3.Rows.Count > 0)
                    {
                        Mostrar(dataGridView5, admin.DevolverPagos(dataGridView1.SelectedRows[0].DataBoundItem as Negocio, dataGridView3.SelectedRows[0].DataBoundItem as Proveedor));
                    }
                    else { dataGridView5.DataSource = null; dataGridView3.DataSource = null; }
                }

            }
            catch (Exception)
            {
            }

        }
        // EVENTO AL SELECCIONAR UNA FILA EN LA GRILLA 2
        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    Mostrar(dataGridView4, admin.RetornaNegocioDeProveedores(dataGridView2.SelectedRows[0].DataBoundItem as Proveedor));
                }
            }
            catch (Exception)
            {
            }
        }
        //GenerarPago
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0 || dataGridView3.Rows.Count == 0) throw new Exception("No hay negocios y/o proveedores asignados");

                Pago pago = new Pago();
                string codigo = Interaction.InputBox("Ingresar codigo de pago: ");
                if (admin.ExisteCodigoPago(dataGridView1.SelectedRows[0].DataBoundItem as Negocio, dataGridView3.SelectedRows[0].DataBoundItem as Proveedor, Convert.ToInt32(codigo))) throw new Exception("El codigo de pago no esta disponible");

                DateTime FdV = Convert.ToDateTime(Interaction.InputBox("Ingresar Fecha de vencimiento: "));
                decimal importe = Convert.ToDecimal(Interaction.InputBox("Importe de Pago: "));

                pago.CodigoNumerico = Convert.ToInt32(codigo);
                pago.FechaDeVencimiento = FdV;
                pago.Importe = importe;

                admin.GenerarPago(dataGridView1.SelectedRows[0].DataBoundItem as Negocio, dataGridView3.SelectedRows[0].DataBoundItem as Proveedor, pago);

                //Mostrar(dataGridView5, (dataGridView3.SelectedRows[0].DataBoundItem as Proveedor).RetornaPago);
                Mostrar(dataGridView5, admin.DevolverPagos(dataGridView1.SelectedRows[0].DataBoundItem as Negocio, dataGridView3.SelectedRows[0].DataBoundItem as Proveedor));
                Mostrar(dataGridView6, admin.DevolverTodosLosPagos());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // EVENTO AL SELECCIONAR UNA FILA EN LA GRILLA 3
        private void dataGridView3_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView3.Rows.Count > 0)
                {
                    Mostrar(dataGridView5, admin.DevolverPagos(dataGridView1.SelectedRows[0].DataBoundItem as Negocio, dataGridView3.SelectedRows[0].DataBoundItem as Proveedor));
                }
            }
            catch (Exception)
            {
            }
        }
        //Baja Negocio
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count == 0) throw new Exception("No hay filas negocios para dar de baja");
                var negocio = admin.SeleccionarNegocio(dataGridView1.SelectedRows[0].DataBoundItem as Negocio);
                if (admin.ComprobarSiExistenPagosPendientes(negocio)) throw new Exception("El negocio posee pagos");
                admin.BajaNegocio(negocio);
                Mostrar(dataGridView1, admin.RetornaNegocios());

                if (dataGridView1.Rows.Count == 0) { dataGridView5.DataSource = null; dataGridView3.DataSource = null; Mostrar(dataGridView4, admin.RetornaNegocioDeProveedores(dataGridView2.SelectedRows[0].DataBoundItem as Proveedor)); }
                else
                {
                    Mostrar(dataGridView3, admin.RetornaProveedoresDeNegocio(dataGridView1.SelectedRows[0].DataBoundItem as Negocio));
                    Mostrar(dataGridView4, admin.RetornaNegocioDeProveedores(dataGridView2.SelectedRows[0].DataBoundItem as Proveedor));
                    Mostrar(dataGridView5, admin.DevolverPagos(dataGridView1.SelectedRows[0].DataBoundItem as Negocio, dataGridView3.SelectedRows[0].DataBoundItem as Proveedor));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // MODIFICAR--------> NEGOCIO
                var negocio = dataGridView1.SelectedRows[0].DataBoundItem as Negocio;

                //Nombre
                string nombre = Interaction.InputBox("Cambiar nombre de Negocio: ", "Cambiando", $"{(negocio.RazonSocial).ToUpper()}").ToUpper();
                if (admin.ExisteNombreModificadoN(negocio.RazonSocial)) throw new Exception($"El negocio {nombre} actualmente esta registrado");
                //telefono
                string telefono = Interaction.InputBox("Cambiar numero de telefono: ", "Cambiando ", $"{negocio.Telefono}");
                if (!Information.IsNumeric(telefono)) throw new Exception("Caracter invalido");
                if (admin.ExisteTelefonoModificadoN(negocio.Telefono.ToString())) throw new Exception($"El telefono {telefono} ya se encuentra registrado");

                //direccion
                string dir = Interaction.InputBox("Indicar la direccion del negocio", "Modificando", negocio.Direccion);

                negocio.RazonSocial = nombre.ToUpper();
                negocio.Telefono = Convert.ToInt32(telefono);
                negocio.Direccion = dir;

                //cargo el negocio
                admin.ModificarRazonSocialNegocio(negocio);
                Mostrar(dataGridView1, admin.RetornaNegocios());
                Mostrar(dataGridView4, admin.RetornaNegocioDeProveedores(dataGridView2.SelectedRows[0].DataBoundItem as Proveedor));
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        // BOTON --------->PAGAR
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView5.Rows.Count > 0)
                {
                    if (!radioButton1.Checked && !radioButton2.Checked) throw new Exception("Se debe elegir un metodo de pago. Efectivo o Tarjeta");
                    var verpago = dataGridView5.SelectedRows[0].DataBoundItem as Pago;
                    Pago pagado = radioButton1.Checked ? new PagoEfectivo() : new PagoTarjeta();
                    pagado.PagoExedido += MontoExedido;
                    if (pagado is PagoEfectivo)
                    {

                        pagado.CodigoNumerico = verpago.CodigoNumerico;
                        pagado.Importe = verpago.Importe;
                        pagado.FechaDeVencimiento = verpago.FechaDeVencimiento;
                        ((PagoEfectivo)pagado).RecargoPago();
                    }
                    else if (pagado is PagoTarjeta)
                    {
                        pagado.CodigoNumerico = verpago.CodigoNumerico;
                        pagado.Importe = verpago.Importe;
                        pagado.FechaDeVencimiento = verpago.FechaDeVencimiento;
                        ((PagoTarjeta)pagado).RecargoPago();
                    }
                    admin.ActualizarPago(dataGridView1.SelectedRows[0].DataBoundItem as Negocio, dataGridView3.SelectedRows[0].DataBoundItem as Proveedor, pagado);
                    
                }
                Mostrar(dataGridView6, admin.DevolverTodosLosPagos());
                Mostrar(dataGridView5, admin.DevolverPagos(dataGridView1.SelectedRows[0].DataBoundItem as Negocio, dataGridView3.SelectedRows[0].DataBoundItem as Proveedor));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //  EVENTO DE MONTO EXEDIDO
        private void MontoExedido(object sender, EventArgs e)
        {
            MessageBox.Show("El monto total exede los 10.000 pesos");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count == 0) throw new Exception("No hay filas negocios para dar de baja");
                var proveedor = admin.SeleccionarProveedor(dataGridView2.SelectedRows[0].DataBoundItem as Proveedor);
                if (admin.ComprobarSiExistenPagosPendientesDeProveedores(proveedor)) throw new Exception("El proveedor posee cliente/s atendidos");
                admin.BajaProveedor(proveedor);
                Mostrar(dataGridView2, admin.RetornaProveedores());

                if (dataGridView2.Rows.Count == 0) { dataGridView4.DataSource = null; dataGridView5.DataSource = null; dataGridView3.DataSource = null; }
                else
                {
                    Mostrar(dataGridView3, admin.RetornaProveedoresDeNegocio(dataGridView1.SelectedRows[0].DataBoundItem as Negocio));
                    Mostrar(dataGridView4, admin.RetornaNegocioDeProveedores(dataGridView2.SelectedRows[0].DataBoundItem as Proveedor));
                    Mostrar(dataGridView5, admin.DevolverPagos(dataGridView1.SelectedRows[0].DataBoundItem as Negocio, dataGridView3.SelectedRows[0].DataBoundItem as Proveedor));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                //Modificar Proveedor
                var proveedor = dataGridView2.SelectedRows[0].DataBoundItem as Proveedor;

                //Nombre
                string nombre = Interaction.InputBox("Cambiar nombre de Negocio: ", "Cambiando", $"{(proveedor.Nombre).ToUpper()}").ToUpper();
                if (admin.ExisteNombreModificadoP(proveedor.Nombre)) throw new Exception($"El negocio {nombre} actualmente esta registrado");
                //telefono
                string telefono = Interaction.InputBox("Cambiar numero de telefono: ", "Cambiando ", $"{proveedor.Telefono}");
                if (!Information.IsNumeric(telefono)) throw new Exception("Caracter invalido");
                if (admin.ExisteTelefonoModificadoP(proveedor.Telefono.ToString())) throw new Exception($"El telefono {telefono} ya se encuentra registrado");


                proveedor.Nombre = nombre.ToUpper();
                proveedor.Telefono = Convert.ToInt32(telefono);

                //cargo el negocio
                admin.ModificarNombreProveedor(proveedor);
                Mostrar(dataGridView2, admin.RetornaProveedores());
                Mostrar(dataGridView3, admin.RetornaProveedoresDeNegocio(dataGridView1.SelectedRows[0].DataBoundItem as Negocio));
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
