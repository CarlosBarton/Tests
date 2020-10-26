using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using Shared;
using OpenQA.Selenium.Interactions;

namespace EvolutionAutomationTests
{
    [TestFixture]
    public class CategoriasDePuesto
    {

        IWebDriver driver;
        WebDriverWait wait;
        Actions actions;
        string txtDescripcionExpected;

        [OneTimeSetUp]
        public void SetUp()
        {
            //Inicialización de variables
            CommonLogic commonLogic = new CommonLogic();
            driver = WebDriverSingleton.GetInstance();
            actions = new Actions(driver);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            string txtDescripcionExpected = "valor1";

            //Ir a pantalla de categorias de puesto
            IWebElement applicacion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='menucenter']/a[2]")));
            applicacion.Click();
            IWebElement modulo = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='innerright']/table[1]/tbody/tr/td/div[1]/div[2]/h2/a")));
            modulo.Click();
            IWebElement seccion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='innerright']/div[2]/fieldset[3]/h4/a")));
            seccion.Click();
            IWebElement opcion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='innerright']/div[2]/fieldset[3]/div/div[1]/div[3]/div[2]/h2/a")));
            opcion.Click();
        }

        [Test, Order(1)]
        public void Crear_Categoria_De_Puesto()
        {
            //Crear nueva categoría de puesto
            IWebElement btnNuevo = driver.FindElement(By.Id("smlCategorias_new"));
            btnNuevo.Click();
            IWebElement txtDescripcion = driver.FindElement(By.Id("Descripcion"));
            txtDescripcion.SendKeys(txtDescripcionExpected);
            IWebElement btnGuardar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("btnGuardarCategoriaPuesto")));
            btnGuardar.Click();

            //Editar la categoria de puesto
            IWebElement txtBuscar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("smlCategorias_txtQuickSearch")));
            txtBuscar.SendKeys(txtDescripcionExpected);
            txtBuscar.SendKeys(Keys.Enter);
            txtBuscar.SendKeys(Keys.Enter);
            IWebElement tblRegistro = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@id='smlCategorias_grid']/div[3]/table/tbody/tr")));
            actions.DoubleClick(tblRegistro).Perform();

            //Verificaciones
            IWebElement txtDescripcionActual = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("Descripcion")));
            string txtDescripcionActualTexto = txtDescripcionActual.GetAttribute("value");
            Assert.AreEqual(txtDescripcionExpected, txtDescripcionActualTexto);

            //Regresar a lista categorias de puesto
            IWebElement btnCancelar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnCancelarCategoria"))); ;
            btnCancelar.Click();
        }

        // ------------------------------------------------------ EDITAR
        [Test, Order(2)]
        public void Editar_Una_Categoria_De_Puesto_con_Informacion_Basica()
        {
            //Variables
            string txtDescripcionEditada = "valor3";

            //Editar la categoría de puesto creada
            IWebElement txtBuscar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("smlCategorias_txtQuickSearch")));
            txtBuscar.SendKeys(txtDescripcionExpected);
            txtBuscar.SendKeys(Keys.Enter);
            txtBuscar.SendKeys(Keys.Enter);
            IWebElement tblRegistro = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='smlCategorias_grid']/div[3]/table/tbody/tr")));
            actions.DoubleClick(tblRegistro).Perform();

            IWebElement txtDescripcion = driver.FindElement(By.Id("Descripcion"));
            txtDescripcion.Clear();
            txtDescripcion.SendKeys(txtDescripcionEditada);
            IWebElement btnGuardar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("btnGuardarCategoriaPuesto")));
            btnGuardar.Click();

            //Editar la categoria de puesto editada
            txtBuscar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("smlCategorias_txtQuickSearch")));
            txtBuscar.Clear();
            txtBuscar.SendKeys(txtDescripcionEditada);
            txtBuscar.SendKeys(Keys.Enter);
            txtBuscar.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            IWebElement tblRegistroEditado = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='smlCategorias_grid']/div[3]/table/tbody/tr")));
            tblRegistroEditado.Click();
            Thread.Sleep(1000);
            IWebElement btnEditar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='smlCategorias_edit']")));
            btnEditar.Click();

            //Verificaciones
            IWebElement txtDescripcionActual = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("Descripcion")));
            string txtDescripcionActualTexto = txtDescripcionActual.GetAttribute("value");
            Assert.AreEqual(txtDescripcionEditada, txtDescripcionActualTexto);

            //Regresar a lista de categorias de puestos
            IWebElement btnCancelar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnCancelarCategoria"))); ;
            btnCancelar.Click();
        }

        // ------------------------------------------------------ GUARDAR DATOS EN BLANCO
        [Test, Order(3)]
        public void Guardar_Area_Funcional_Con_Campos_En_Blanco()
        {
            //Inicialización de variables
            var txtErrorExpected = "Favor ingrese el nombre del área funcional";

            //Crear nueva área funcional
            IWebElement btnNuevo = driver.FindElement(By.Id("smlAreasFuncionales_new"));
            btnNuevo.Click();
            IWebElement txtNombre = driver.FindElement(By.Id("Nombre"));
            IWebElement btnGuardar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("btnGuardarAreaFuncional")));
            btnGuardar.Click();

            //Verificaciones
            IWebElement msgError = driver.FindElement(By.XPath("//*[@id='MessagesAndErrors']/div/ul/li"));  
            Assert.AreEqual(txtErrorExpected, msgError.Text);

            //Regresar a lista de áreas funcionales
            IWebElement btnCancelar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnCancelarAreaFuncional"))); ;
            btnCancelar.Click();
        }

        // -------------------------------------------------------- CONSULTAR
        [Test, Order(4)]
        public void Consultar_Un_Area_Funcional_Con_Informacion_Basica()
        {

            var txtdatos = "Área Funcional 1 editada";
            var txtgrupo = "Aseinfo";

            // Ingresar a usuario auditoria
            Logout();
            Login("auditoria", "auditoria");

            //Ir a pantalla de áreas funcionales
            IWebElement applicacion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='menucenter']/a[2]")));
            applicacion.Click();
            IWebElement modulo = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='innerright']/table[1]/tbody/tr/td/div[1]/div[2]/h2/a")));
            modulo.Click();
            IWebElement seccion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='innerright']/div[2]/fieldset[3]/h4/a")));
            seccion.Click();
            IWebElement opcion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='innerright']/div[2]/fieldset[3]/div/div[1]/div[1]/div[2]/h2/a")));
            opcion.Click();

            // Buscar área funcional
            IWebElement txtBuscar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("smlAreasFuncionales_txtQuickSearch")));
            txtBuscar.SendKeys(txtdatos);
            txtBuscar.SendKeys(Keys.Enter);
            txtBuscar.SendKeys(Keys.Enter);
            IWebElement tblRegistro = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@id='smlAreasFuncionales_grid']/div[3]/table/tbody/tr")));
            actions.DoubleClick(tblRegistro).Perform();

            // Comparar datos

            IWebElement msgdatos = driver.FindElement(By.XPath("//*[@id='innerright']/div[2]/fieldset/div[2]/span"));
            Assert.AreEqual(txtdatos, msgdatos.Text);
            IWebElement msggrupo = driver.FindElement(By.XPath("//*[@id='innerright']/div[2]/fieldset/div[3]/span"));
            Assert.AreEqual(txtgrupo, msggrupo.Text);

            // Ingresar a usuario soporteit
            Logout();
            Login("soporteit", "soporteit");
        }

        //Eliminar un área funcional
        [Test, Order(5)]
        public void Eliminar_Area_Funcional()
        {
            //Inicialización de variables
            string txtNombreEditado = "Área Funcional 1 editada";

            //Eliminar un área funcional
            IWebElement txtBuscar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("smlAreasFuncionales_txtQuickSearch")));
            txtBuscar.SendKeys(txtNombreEditado);
            txtBuscar.SendKeys(Keys.Enter);
            txtBuscar.SendKeys(Keys.Enter);
            IWebElement tblRegistro = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='smlAreasFuncionales_grid']/div[3]/table/tbody/tr")));
            tblRegistro.Click();
            IWebElement btnEliminar = driver.FindElement(By.Id("smlAreasFuncionales_delete"));
            btnEliminar.Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();

            //Verificaciones
            IWebElement lblRegistros = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@id='smlAreasFuncionales_grid']/div[4]/span[2]")));
            Assert.AreEqual("No hay registros.", lblRegistros);
        }

        public void Login(string username, string password)
        {
            IWebElement inputUser = driver.FindElement(By.Id("txtUsername"));
            inputUser.SendKeys(username);
            IWebElement inputPassword = driver.FindElement(By.Id("txtPassword"));
            inputPassword.SendKeys(password);
            inputPassword.SendKeys(Keys.Enter);
        }
        public void Logout()
        {
            IWebElement CerrarSesion = driver.FindElement(By.XPath("//*[@id='topmenu']/a[1]"));
            CerrarSesion.Click();
        }
    }
}