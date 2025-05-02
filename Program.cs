using System;
using System.Runtime.CompilerServices;
using Gtk;

class Program
{
    public static datos da = new datos();
    public static Blockchain l = new Blockchain();

    public static ListaDoble d = new ListaDoble();
    public static ArbolAVL a = new ArbolAVL(); 
    public static ArbolBinarioBusqueda bus = new ArbolBinarioBusqueda();
    public static ArbolMerkle m = new ArbolMerkle();
    public static Leer le = new Leer();

    
     public static string texto = "";
    static void Main()
    {
        l.CargarDesdeArchivoJson("archivosJson/listaSimple.json");
        d.CargarDesdeArchivoEdd("archivosJson/listaDoble.edd");
        a.CargarDesdeArchivoEdd("archivosJson/arbolAVL.edd");
        bus.CargarDesdeArchivoJson("archivosJson/arbolBB.json");

        Application.Init();

     login();

        Application.Run();

    
        //l.Mostrar();  
    /////////////////////////////////////////////////////////////////////////////
        //d.Imprimir();
    //////////////////////////////////////////////////////////
    ///
    
    
    }

   private static void login(){
        Ventana ventanaPrincipal = new Ventana("Ventana Principal",500,400);
        ventanaPrincipal.DeleteEvent += (o, args) => Application.Quit();


        Fixed fix = new Fixed();
        Botones b1 = new Botones("Aceptar",100,50,200,300);

        Label titulo = new Label("Login");
        Label Nombre = new Label("Nombre");
        Label Contrasena = new Label("Contrasena");

        CajaTexto txtNombre = new CajaTexto(200,50,200,100);
        CajaTexto txtContrasena = new CajaTexto(200,50,200,200);


        fix.Put(titulo, 200,50);
        fix.Put(Nombre, 100,100);
        fix.Put(Contrasena, 100,200);
        fix.Put(txtNombre, txtNombre.x,txtNombre.y);
        fix.Put(txtContrasena, txtContrasena.x,txtContrasena.y);
        fix.Put(b1, b1.x,b1.y);

        b1.Clicked += (sender, e) => validar( txtNombre.Text, txtContrasena.Text,  b1);

        ventanaPrincipal.Add(fix);
        ventanaPrincipal.ShowAll();

    }

    public static void validar(string usu, string contra, Botones b){

        
        if(usu == "admin@usac.com" && contra == "admint123"){
            b.abrir(menu());
            
   
        }else if(l.Ingresar(usu, contra)){
            b.abrir(menuUsuario());
            
        }else{
            Console.WriteLine("DATOS ERRONEOS");
        }
    }

    private static Ventana menu(){
        Ventana Menu = new Ventana("menu",800,450);
        Menu.DeleteEvent += (o, args) => Application.Quit();

        Fixed fix = new Fixed();

        Botones b1 = new Botones("Carga Massiva",250,50,100,50);
        Botones b2 = new Botones("Gestor de Entidades",250,50,100,150);
        Botones b3 = new Botones("Actualizacion de Repuestos",250,50,100,250);
        Botones b4 = new Botones("Visualizacion de Repuestos",250,50,450,50);
        Botones b5 = new Botones("Generar Servicios ",250,50,450,150);
        Botones b6 = new Botones("Control de Logueo ",250,50,450,250);
        Botones b7 = new Botones("Generar Reportes ",250,50,275,350);

      
        b1.Clicked += (sender, e) => b1.abrir(cargaMasiva());
        b2.Clicked += (sender, e) => b2.abrir(menuGestion());
        b3.Clicked += (sender, e) => b3.abrir(actualizarRepuesto());
        b4.Clicked += (sender, e) => b4.abrir(visualizarRepuesto());
        b5.Clicked += (sender, e) => b5.abrir(generarServicio());
        b6.Clicked += (sender, e) => Console.WriteLine("logeo");
        b7.Clicked += (sender, e) => Reportes();

        
        fix.Put(b1, b1.x,b1.y);
        fix.Put(b2, b2.x,b2.y);
        fix.Put(b3, b3.x,b3.y);
        fix.Put(b4, b4.x,b4.y);
        fix.Put(b5, b5.x,b5.y);
        fix.Put(b6, b6.x,b6.y);
        fix.Put(b7, b7.x,b7.y);
        
        Menu.Add(fix);
        Menu.ShowAll();
        
        return Menu;
    }

    public static Ventana cargaMasiva(){
        string eleccion = "";
        Fixed fix = new Fixed();
        Leer le = new Leer();
        Ventana CargaMassiva = new Ventana("Carga Massiva",500,450);

        ComboBox combo = new ComboBox("Cargar Usuarios", "Cargar Vehiculos", "Cargar Repuestos", 200, 50, 100, 50);
        fix.Put(combo,combo.x,combo.y);
        combo.Changed += (sender, e) => {
            eleccion = combo.ActiveText;
        };

        Botones b2 = new Botones("Cargar",100,50,200,200);
        b2.Clicked += (sender, e) => {
            if(eleccion == "Cargar Usuarios"){
                da.UsuariosMasivos(l);
            }else if(eleccion == "Cargar Vehiculos"){
                da.VehiculosMasivos(d);
            }else if(eleccion == "Cargar Repuestos"){
                da.RepuestoMasivos(a);
            }else{
                Console.WriteLine("no se a seleccionado una opcion");
            }
        };
       /* b2.Clicked += (sender, e) => {
            CargaMassiva.Destroy();
            CargaMassiva = null;
        };*/

        fix.Put(b2, b2.x,b2.y);

        CargaMassiva.Add(fix);
        CargaMassiva.ShowAll();

        return CargaMassiva;
    }

    public static Ventana menuGestion(){
        string eleccion = "";
        Fixed fix = new Fixed();
        Leer le = new Leer();
        Ventana CargaMassiva = new Ventana("Gestion",500,450);

         ComboBox combo = new ComboBox("Gestionar Usuarios", "Gestionar Vehiculos", "", 200, 50, 100, 50);
        fix.Put(combo,combo.x,combo.y);
        combo.Changed += (sender, e) => {
            eleccion = combo.ActiveText;
        };

        Botones b2 = new Botones("Cargar",100,50,200,200);
        b2.Clicked += (sender, e) => {
            if(eleccion == "Gestionar Usuarios"){
                gestionusuarios();
            }else if(eleccion == "Gestionar Vehiculos"){
                gestionVehiculos();
            }else{
                Console.WriteLine("no se a seleccionado una opcion");
            }
        };
      
        /*b2.Clicked += (sender, e) => {
            CargaMassiva.Destroy();
            CargaMassiva = null;
        };*/

        fix.Put(b2, b2.x,b2.y);
        CargaMassiva.Add(fix);
        CargaMassiva.ShowAll();
        

        return CargaMassiva;
    }

    private static Ventana gestionusuarios()
{

    Ventana GestionUsuarios = new Ventana("Gestion de Usuarios", 600, 700);
    Fixed fix = new Fixed();

    Botones b = new Botones("Buscar", 100, 50, 50, 650);
    Botones b1 = new Botones("Insertar", 100, 50, 200, 650);

    Labels labId = new Labels("ID", 50, 50);
    Labels labNombre = new Labels("Nombre", 50, 150);
    Labels labApellido = new Labels("Apellido", 50, 250);
    Labels labEdad = new Labels("Edad", 50, 350);
    Labels labCorreo = new Labels("Correo", 50, 450);
    Labels labContrasenia = new Labels("Contrasenia", 50, 550);

    fix.Put(labId, labId.x, labId.y);
    fix.Put(labNombre, labNombre.x, labNombre.y);
    fix.Put(labApellido, labApellido.x, labApellido.y);
    fix.Put(labEdad, labEdad.x, labEdad.y);
    fix.Put(labCorreo, labCorreo.x, labCorreo.y);
    fix.Put(labContrasenia, labContrasenia.x, labContrasenia.y);

    CajaTexto txtId = new CajaTexto(200, 50, 150, 50);
    CajaTexto txtNombre = new CajaTexto(200, 50, 150, 150);
    CajaTexto txtApellido = new CajaTexto(200, 50, 150, 250);
    CajaTexto txtEdad = new CajaTexto(200, 50, 150, 350);
    CajaTexto txtCorreo = new CajaTexto(200, 50, 150, 450);
    CajaTexto txtContrasenia = new CajaTexto(200, 50, 150, 550);

    b.Clicked += (sender, e) =>
    {  
        if(l.BuscarNodo(Convert.ToInt32(txtId.Text)) != null){
            txtNombre.Text = l.BuscarNodo(Convert.ToInt32(txtId.Text)).Nombres; 
            txtApellido.Text = l.BuscarNodo(Convert.ToInt32(txtId.Text)).Apellidos ;
            txtEdad.Text = l.BuscarNodo(Convert.ToInt32(txtId.Text)).Edad.ToString() ;
            txtCorreo.Text = l.BuscarNodo(Convert.ToInt32(txtId.Text)).Correo ;
            txtContrasenia.Text = l.BuscarNodo(Convert.ToInt32(txtId.Text)).Contrasenia ;
        }
        
    };

    b1.Clicked += (sender, e) =>
    {  
           da.crearUsusario(l,Convert.ToInt32(txtId.Text), txtNombre.Text, txtApellido.Text, txtCorreo.Text, Convert.ToInt32(txtEdad.Text),txtContrasenia.Text);
            
    };

    fix.Put(txtId, txtId.x, txtId.y);
    fix.Put(txtNombre, txtNombre.x, txtNombre.y);
    fix.Put(txtApellido, txtApellido.x, txtApellido.y);
    fix.Put(txtEdad, txtEdad.x, txtEdad.y);
    fix.Put(txtCorreo, txtCorreo.x, txtCorreo.y);
    fix.Put(txtContrasenia, txtContrasenia.x, txtContrasenia.y);

    fix.Put(b, b.x, b.y);
    fix.Put(b1, b1.x, b1.y);

    GestionUsuarios.Add(fix);
    GestionUsuarios.ShowAll();

    return GestionUsuarios;
}

private static Ventana gestionVehiculos()
{

    Ventana GestionVehiculo = new Ventana("Gestion de Vehiculo", 600, 700);
    Fixed fix = new Fixed();

    Botones b = new Botones("Buscar", 100, 50, 50, 650);
    Botones b1 = new Botones("Editar", 100, 50, 200, 650);
    Botones b2 = new Botones("Eliminar", 100, 50, 350, 650);

    Labels labId = new Labels("ID", 50, 50);
    Labels labNombre = new Labels("ID Usuario", 50, 150);
    Labels labApellido = new Labels("Marca", 50, 250);
    Labels labEdad = new Labels("Modelo", 50, 350);
    Labels labCorreo = new Labels("Placa", 50, 450);

    fix.Put(labId, labId.x, labId.y);
    fix.Put(labNombre, labNombre.x, labNombre.y);
    fix.Put(labApellido, labApellido.x, labApellido.y);
    fix.Put(labEdad, labEdad.x, labEdad.y);
    fix.Put(labCorreo, labCorreo.x, labCorreo.y);

    CajaTexto txtId = new CajaTexto(200, 50, 150, 50);
    CajaTexto txtNombre = new CajaTexto(200, 50, 150, 150);
    CajaTexto txtApellido = new CajaTexto(200, 50, 150, 250);
    CajaTexto txtEdad = new CajaTexto(200, 50, 150, 350);
    CajaTexto txtCorreo = new CajaTexto(200, 50, 150, 450);

    b.Clicked += (sender, e) =>
    {  
        if(d.BuscarNodo(Convert.ToInt32(txtId.Text)) != null){
            txtNombre.Text = d.BuscarNodo(Convert.ToInt32(txtId.Text)).IdUsuario.ToString(); 
            txtApellido.Text = d.BuscarNodo(Convert.ToInt32(txtId.Text)).Marca ;
            txtEdad.Text = d.BuscarNodo(Convert.ToInt32(txtId.Text)).Modelo.ToString() ;
            txtCorreo.Text = d.BuscarNodo(Convert.ToInt32(txtId.Text)).Placa ;
        }
        
    };

    b1.Clicked += (sender, e) =>
    {  
            d.Editar(Convert.ToInt32(txtId.Text), Convert.ToInt32(txtNombre.Text), txtApellido.Text, Convert.ToInt32(txtCorreo.Text),txtEdad.Text);
            
            
        
    };
    b2.Clicked += (sender, e) => {
            if(Convert.ToInt32(txtId.Text) != null){
            d.Eliminar(Convert.ToInt32(txtId.Text));
            }

    };

    fix.Put(txtId, txtId.x, txtId.y);
    fix.Put(txtNombre, txtNombre.x, txtNombre.y);
    fix.Put(txtApellido, txtApellido.x, txtApellido.y);
    fix.Put(txtEdad, txtEdad.x, txtEdad.y);
    fix.Put(txtCorreo, txtCorreo.x, txtCorreo.y);

    fix.Put(b, b.x, b.y);
    fix.Put(b1, b1.x, b1.y);
    fix.Put(b2, b2.x, b2.y);

    GestionVehiculo.Add(fix);
    GestionVehiculo.ShowAll();

    return GestionVehiculo;
}

private static Ventana actualizarRepuesto()
{

    Ventana GestionRepuesto = new Ventana("Gestion de Usuarios", 450, 550);
    Fixed fix = new Fixed();

    Botones b = new Botones("Buscar", 100, 50, 50, 450);
    Botones b1 = new Botones("Actualizar", 100, 50, 200, 450);

    Labels labId = new Labels("ID", 50, 50);
    Labels labNombre = new Labels("Repuesto", 50, 150);
    Labels labApellido = new Labels("Detalle", 50, 250);
    Labels labEdad = new Labels("Costo", 50, 350);

    fix.Put(labId, labId.x, labId.y);
    fix.Put(labNombre, labNombre.x, labNombre.y);
    fix.Put(labApellido, labApellido.x, labApellido.y);
    fix.Put(labEdad, labEdad.x, labEdad.y);

    CajaTexto txtId = new CajaTexto(200, 50, 150, 50);
    CajaTexto txtNombre = new CajaTexto(200, 50, 150, 150);
    CajaTexto txtApellido = new CajaTexto(200, 50, 150, 250);
    CajaTexto txtEdad = new CajaTexto(200, 50, 150, 350);

    b.Clicked += (sender, e) =>
    {  
        if(a.BuscarNodo(Convert.ToInt32(txtId.Text)) != null){
            txtNombre.Text = a.BuscarNodo(Convert.ToInt32(txtId.Text)).repuesto.ToString(); 
            txtApellido.Text = a.BuscarNodo(Convert.ToInt32(txtId.Text)).detalles ;
            txtEdad.Text = a.BuscarNodo(Convert.ToInt32(txtId.Text)).costo.ToString() ;
        }
        
    };

    b1.Clicked += (sender, e) =>
    {  
            a.Editar(Convert.ToInt32(txtId.Text), txtNombre.Text, txtApellido.Text,Convert.ToDouble(txtEdad.Text));
          
    };

    fix.Put(txtId, txtId.x, txtId.y);
    fix.Put(txtNombre, txtNombre.x, txtNombre.y);
    fix.Put(txtApellido, txtApellido.x, txtApellido.y);
    fix.Put(txtEdad, txtEdad.x, txtEdad.y);

    fix.Put(b, b.x, b.y);
    fix.Put(b1, b1.x, b1.y);

    GestionRepuesto.Add(fix);
    GestionRepuesto.ShowAll();

    return GestionRepuesto;
}


public static Ventana visualizarRepuesto(){
        string eleccion = "";
        Fixed fix = new Fixed();
        Leer le = new Leer();
        Ventana CargaMassiva = new Ventana("Visualizar Repuesto",500,450);

        ComboBox combo = new ComboBox("Pre-Orden", "In-Orden", "Post-Orden", 50, 50, 200, 50);
        fix.Put(combo,combo.x,combo.y);
        combo.Changed += (sender, e) => {
            eleccion = combo.ActiveText;
        };

        VBox vbox = new VBox();

        // Crear un TextView con un ScrollWindow
        TextView textView = new TextView();
        textView.Editable = false;  // Deshabilitar edición (si solo queremos mostrar datos)
        textView.WrapMode = WrapMode.Word; // Ajustar palabras al ancho
        vbox.SetSizeRequest(400,250);

        string tableContent = "Id\t Repuesto\tDetalle\tCoste\n"  ;

      
        Botones b2 = new Botones("Cargar",100,50,300,50);
        b2.Clicked += (sender, e) => {
            if(eleccion == "Pre-Orden"){
                a.RecorridoPreOrden(a.raiz);
                tableContent += a.texto;
         textView.Buffer.Text = tableContent;
         tableContent = "Id\t Repuesto\tDetalle\tCoste\n" ;
         a.texto = "";
            }else if(eleccion == "In-Orden"){
                a.RecorridoInOrden(a.raiz);
                tableContent += a.texto;
         textView.Buffer.Text = tableContent;
         tableContent = "Id\t Repuesto\tDetalle\tCoste\n" ;
                a.texto = "";
            }else if(eleccion == "Post-Orden"){
            a.RecorridoPostOrden(a.raiz);
            tableContent += a.texto;
         textView.Buffer.Text = tableContent;
         tableContent = "Id\t Repuesto\tDetalle\tCoste\n" ;
            a.texto = "";
            }else{
                Console.WriteLine("no se a seleccionado una opcion");
            }
        };
        

        ScrolledWindow scrolledWindow = new ScrolledWindow();
        scrolledWindow.Add(textView);

        vbox.PackStart(scrolledWindow, true, true, 0);

        fix.Put(vbox, 50, 150);
        fix.Put(b2, b2.x,b2.y);

        CargaMassiva.Add(fix);
        CargaMassiva.ShowAll();

        return CargaMassiva;
    }
    


public static Ventana generarServicio(){

        Fixed fix = new Fixed();

         Ventana IngresoManual = new Ventana("Ingreso Manual",500,650);
        
        
        Labels labId = new Labels("ID",50,50);
        Labels labNombre = new Labels("Id Repuesto",50,150);
        Labels labApellido = new Labels("Id Vehiculo",50,250);
        Labels labCorreo = new Labels("Detalles",50,350);
        Labels labContrasenia = new Labels("Costo",50,450);

        fix.Put(labId, labId.x,labId.y);
        fix.Put(labNombre, labNombre.x,labNombre.y);
        fix.Put(labApellido, labApellido.x,labApellido.y);
        fix.Put(labCorreo, labCorreo.x,labCorreo.y);
        fix.Put(labContrasenia, labContrasenia.x,labContrasenia.y);

        CajaTexto txtId = new CajaTexto(200,50,150,50);
        CajaTexto txtNombre = new CajaTexto(200,50,150,150);
        CajaTexto txtApellido = new CajaTexto(200,50,150,250);
        CajaTexto txtCorreo = new CajaTexto(200,50,150,350);
        CajaTexto txtContrasenia = new CajaTexto(200,50,150,450);

        Botones b = new Botones("Ingreso Manual",100,50,200,550);
        b.Clicked += (sender, e) => da.generarServicio(bus,Int32.Parse(txtId.Text), Int32.Parse(txtNombre.Text), Int32.Parse(txtApellido.Text), txtCorreo.Text, Convert.ToDouble(txtContrasenia.Text));
        b.Clicked += (sender, e) => da.generarFactura(m,Int32.Parse(txtId.Text),Int32.Parse(txtId.Text),Convert.ToDouble(txtContrasenia.Text), "");

        fix.Put(txtId, txtId.x,txtId.y);
        fix.Put(txtNombre, txtNombre.x,txtNombre.y);
        fix.Put(txtApellido, txtApellido.x,txtApellido.y);
        fix.Put(txtCorreo, txtCorreo.x,txtCorreo.y);
        fix.Put(txtContrasenia, txtContrasenia.x,txtContrasenia.y);
        fix.Put(b, b.x, b.y);


        IngresoManual.Add(fix);
        IngresoManual.ShowAll();

        return IngresoManual;

    }

    public static void Reportes(){
        da.g.graficosSimples(l);
        da.g.graficosDobles(d);
        da.g.graficosAVL(a);
        da.g.graficosABB(bus);
        da.g.graficosM(m);

    }



    ///////////////////////////USUARIO///////////////////////////////////////////////////////////////////////////////////
    ///
     private static Ventana menuUsuario(){
        Ventana Menu = new Ventana("Menu de Usuario",500,450);
        Menu.DeleteEvent += (o, args) => Application.Quit();

        Fixed fix = new Fixed();

        Botones b1 = new Botones("Insertar Vehiculo",250,50,150,50);
        Botones b2 = new Botones("Visualizacion de Servicio",250,50,150,150);
        Botones b3 = new Botones("Visualizacion de Facturas",250,50,150,250);
        Botones b4 = new Botones("Cancelar Facturas",250,50,150,350);
       
      
        b1.Clicked += (sender, e) => b1.abrir(InsertarVehiculo());
        b2.Clicked += (sender, e) => b2.abrir(visualizarServicio());
        b3.Clicked += (sender, e) => b3.abrir(visualizarFacturas());
        b4.Clicked += (sender, e) => b4.abrir(cancelarFactura());
       
        
        fix.Put(b1, b1.x,b1.y);
        fix.Put(b2, b2.x,b2.y);
        fix.Put(b3, b3.x,b3.y);
        fix.Put(b4, b4.x,b4.y);
       
        Menu.Add(fix);
        Menu.ShowAll();
        
        return Menu;
    }

    public static Ventana InsertarVehiculo(){

        Fixed fix = new Fixed();

         Ventana IngresoManual = new Ventana("Ingreso Vehiculo",500,650);
        
        
        Labels labId = new Labels("ID",50,50);
        Labels labNombre = new Labels("Id Usuario",50,150);
        Labels labApellido = new Labels("Marca",50,250);
        Labels labCorreo = new Labels("Modelo",50,350);
        Labels labContrasenia = new Labels("Placa",50,450);

        fix.Put(labId, labId.x,labId.y);
        fix.Put(labNombre, labNombre.x,labNombre.y);
        fix.Put(labApellido, labApellido.x,labApellido.y);
        fix.Put(labCorreo, labCorreo.x,labCorreo.y);
        fix.Put(labContrasenia, labContrasenia.x,labContrasenia.y);

        CajaTexto txtId = new CajaTexto(200,50,150,50);
        CajaTexto txtNombre = new CajaTexto(200,50,150,150);
        CajaTexto txtApellido = new CajaTexto(200,50,150,250);
        CajaTexto txtCorreo = new CajaTexto(200,50,150,350);
        CajaTexto txtContrasenia = new CajaTexto(200,50,150,450);

        Botones b = new Botones("Ingreso Manual",100,50,200,550);
        b.Clicked += (sender, e) => da.crearVehiculo(d,Int32.Parse(txtId.Text), Int32.Parse(txtNombre.Text), txtApellido.Text, Int32.Parse(txtCorreo.Text), txtContrasenia.Text);
       // b.Clicked += (sender, e) => IngresoManual.Destroy();

        fix.Put(txtId, txtId.x,txtId.y);
        fix.Put(txtNombre, txtNombre.x,txtNombre.y);
        fix.Put(txtApellido, txtApellido.x,txtApellido.y);
        fix.Put(txtCorreo, txtCorreo.x,txtCorreo.y);
        fix.Put(txtContrasenia, txtContrasenia.x,txtContrasenia.y);
        fix.Put(b, b.x, b.y);


        IngresoManual.Add(fix);
        IngresoManual.ShowAll();

        return IngresoManual;

    }

    public static Ventana visualizarServicio(){
        string eleccion = "";
        Fixed fix = new Fixed();
        Leer le = new Leer();
        Ventana CargaMassiva = new Ventana("Visualizar Repuesto",500,450);

        ComboBox combo = new ComboBox("Pre-Orden", "In-Orden", "Post-Orden", 50, 50, 200, 50);
        fix.Put(combo,combo.x,combo.y);
        combo.Changed += (sender, e) => {
            eleccion = combo.ActiveText;
        };

        VBox vbox = new VBox();

        // Crear un TextView con un ScrollWindow
        TextView textView = new TextView();
        textView.Editable = false;  
        textView.WrapMode = WrapMode.Word; // Ajustar palabras al ancho
        vbox.SetSizeRequest(400,250);

        string tableContent = "Id\t Repuesto\tVehiculo\tDetalles\tCoste\n"  ;

      
        Botones b2 = new Botones("Cargar",100,50,300,50);
        b2.Clicked += (sender, e) => {
            if(eleccion == "Pre-Orden"){
                bus.RecorridoPreOrden(bus.raiz);
                tableContent += bus.texto;
         textView.Buffer.Text = tableContent;
         tableContent = "Id\t Vehiculo\tRepuesto\tDetalle\tCoste\n" ;
         bus.texto = "";
            }else if(eleccion == "In-Orden"){
                bus.RecorridoInOrden(bus.raiz);
                tableContent += bus.texto;
         textView.Buffer.Text = tableContent;
         tableContent = "Id\t Vehiculo\t Repuesto\tDetalle\tCoste\n" ;
                bus.texto = "";
            }else if(eleccion == "Post-Orden"){
            bus.RecorridoPostOrden(bus.raiz);
            tableContent += bus.texto;
         textView.Buffer.Text = tableContent;
         tableContent = "Id\t Vehiculo\t Repuesto\tDetalle\tCoste\n" ;
            bus.texto = "";
            }else{
                Console.WriteLine("no se a seleccionado una opcion");
            }
        };
        

        ScrolledWindow scrolledWindow = new ScrolledWindow();
        scrolledWindow.Add(textView);

        vbox.PackStart(scrolledWindow, true, true, 0);

        fix.Put(vbox, 50, 150);
        fix.Put(b2, b2.x,b2.y);

        CargaMassiva.Add(fix);
        CargaMassiva.ShowAll();

        return CargaMassiva;
}

public static Ventana visualizarFacturas(){
        string eleccion = "";
        Fixed fix = new Fixed();
        Leer le = new Leer();
        Ventana CargaMassiva = new Ventana("Visualizar Repuesto",500,450);


        VBox vbox = new VBox();

        // Crear un TextView con un ScrollWindow
        TextView textView = new TextView();
        textView.Editable = false;  
        textView.WrapMode = WrapMode.Word; // Ajustar palabras al ancho
        vbox.SetSizeRequest(400,250);

        string tableContent = ""  ;

      
        Botones b2 = new Botones("Cargar",100,50,200,50);
        b2.Clicked += (sender, e) => {
                bus.RecorridoPreOrden(bus.raiz);
                tableContent += bus.texto;
         textView.Buffer.Text = tableContent;
         tableContent = "Id\t Orden\tTotal\n" ;
         bus.texto = "";
            
        };
        

        ScrolledWindow scrolledWindow = new ScrolledWindow();
        scrolledWindow.Add(textView);

        vbox.PackStart(scrolledWindow, true, true, 0);

        fix.Put(vbox, 50, 150);
        fix.Put(b2, b2.x,b2.y);

        CargaMassiva.Add(fix);
        CargaMassiva.ShowAll();

        return CargaMassiva;
}
    private static Ventana cancelarFactura()
{

    Ventana GestionUsuarios = new Ventana("Cancelar Factura", 600, 450);
    Fixed fix = new Fixed();

    Botones b = new Botones("Buscar", 100, 50, 50, 350);
    Botones b2 = new Botones("Eliminar", 100, 50, 350, 350);

    Labels labId = new Labels("ID", 50, 50);
    Labels labNombre = new Labels("Orden", 50, 150);
    Labels labApellido = new Labels("Total", 50, 250);

    fix.Put(labId, labId.x, labId.y);
    fix.Put(labNombre, labNombre.x, labNombre.y);
    fix.Put(labApellido, labApellido.x, labApellido.y);

    CajaTexto txtId = new CajaTexto(200, 50, 150, 50);
    CajaTexto txtNombre = new CajaTexto(200, 50, 150, 150);
    CajaTexto txtApellido = new CajaTexto(200, 50, 150, 250);

    b.Clicked += (sender, e) =>
    {  
      /*  if(bb.Buscar(Convert.ToInt32(txtId.Text)) != null){
            txtNombre.Text = Convert.ToString(bb.Buscar(Convert.ToInt32(txtId.Text)).IdServicio); 
            txtApellido.Text = Convert.ToString(bb.Buscar(Convert.ToInt32(txtId.Text)).Total );
        }*/
        
    };

   
    b2.Clicked += (sender, e) => {
            if(Convert.ToInt32(txtId.Text) != null){
          //  bb.Eliminar(Convert.ToInt32(txtId.Text));
            }

    };

    fix.Put(txtId, txtId.x, txtId.y);
    fix.Put(txtNombre, txtNombre.x, txtNombre.y);
    fix.Put(txtApellido, txtApellido.x, txtApellido.y);

    fix.Put(b, b.x, b.y);
    fix.Put(b2, b2.x, b2.y);

    GestionUsuarios.Add(fix);
    GestionUsuarios.ShowAll();

    return GestionUsuarios;
}




}
