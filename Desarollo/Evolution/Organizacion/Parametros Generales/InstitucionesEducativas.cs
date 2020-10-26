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
	public class InstitucionesEductivas
	{

		IWebDriver driver;
		WebDriverWait wait;
		Actions actions;
		string txtNombreExpected;

		[OneTimeSetUp]
		public void SetUp()
		{
			//Inicialización de variables
			CommonLogic commonLogic = new CommonLogic();
			driver = WebDriverSingleton.GetInstance();
			actions = new Actions(driver);
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
			txtNombreExpected = "InstituciónEducativa";

			//Ir a pantalla de Instituciones Educativas
			IWebElement applicacion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='menucenter']/a[2]")));
			applicacion.Click();
			IWebElement modulo = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div[4]/div[2]/div[2]/table[4]/tbody/tr/td/div[1]/div[2]/h2/a")));
			modulo.Click();
			IWebElement seccion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div[4]/div[2]/div[2]/div[2]/fieldset/div/div[4]/div[1]/div[2]/h2/a")));
			seccion.Click();
			//IWebElement opcion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='innerright']/div[2]/fieldset[3]/div/div[1]/div[1]/div[2]/h2/a")));
		}

		[Test, Order(1)]
		public void Crear_Institucion_Educativa_Con_Informacion_Basica()
		{
			//Crear nueva institucion educativa
			IWebElement btnNuevo = driver.FindElement(By.Id("smlInstitucionesEducativas_new"));
			btnNuevo.Click();
			IWebElement txtNombre = driver.FindElement(By.Id("Nombre"));
			txtNombre.SendKeys(txtNombreExpected);
            IWebElement txtNombreCorto = driver.FindElement(By.Id("NombreCorto"));
            txtNombreCorto.SendKeys(txtNombreExpected);
            IWebElement cmbPais = driver.FindElement(By.Id("codigoPais"));
            cmbPais.Click();
            IWebElement paisGuatemala = driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[2]/div[2]/form/div[1]/fieldset/div[4]/select/option[2]"));
            paisGuatemala.Click();
			IWebElement btnGuardar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("btnGuardarInstitucionEducativa")));
			btnGuardar.Click();

			//Editar institucion educativa creada
			IWebElement txtBuscar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("smlInstitucionesEducativas_txtQuickSearch")));
			txtBuscar.SendKeys(txtNombreExpected);
			txtBuscar.SendKeys(Keys.Enter);
			txtBuscar.SendKeys(Keys.Enter);
			IWebElement tblRegistro = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@id='smlInstitucionesEducativas_grid']/div[3]/table/tbody/tr")));
			actions.DoubleClick(tblRegistro).Perform();

			//Verificaciones
			IWebElement txtNombreActual = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("Nombre")));
			string txtNombreActualTexto = txtNombreActual.GetAttribute("value");
			Assert.AreEqual(txtNombreExpected, txtNombreActualTexto);

			//Regresar a lista de instituciones educativas
			IWebElement btnCancelar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnCancelarInstitucionEducativa"))); 
			btnCancelar.Click();
		}


        // ------------------------------------------------------ EDITAR
        [Test, Order(2)]
        public void Editar_Institucion_Educativa_Con_Informacion_Basica()
        {
            //Variables
            string txtNombreEditado = "Institución Educativa 1 editada";

            //Editar Institución Educativa creada
            IWebElement txtBuscar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("smlInstitucionesEducativas_txtQuickSearch")));
            txtBuscar.SendKeys(txtNombreExpected);
            txtBuscar.SendKeys(Keys.Enter);
            txtBuscar.SendKeys(Keys.Enter);
            IWebElement tblRegistro = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div[4]/div[2]/div[2]/div[2]/div[2]/div[3]/table/tbody/tr")));
            actions.DoubleClick(tblRegistro).Perform();

            IWebElement txtNombre = driver.FindElement(By.Id("Nombre"));
            txtNombre.Clear();
            txtNombre.SendKeys(txtNombreEditado);
            IWebElement btnGuardar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("btnGuardarInstitucionEducativa")));
            btnGuardar.Click();

            //Editar la institución educativa editada
            txtBuscar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("smlInstitucionesEducativas_txtQuickSearch")));
            txtBuscar.Clear();
            txtBuscar.SendKeys(txtNombreEditado);
            txtBuscar.SendKeys(Keys.Enter);
            txtBuscar.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            IWebElement tblRegistroEditado = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div[4]/div[2]/div[2]/div[2]/div[2]/div[3]/table/tbody/tr")));
            tblRegistroEditado.Click();
            Thread.Sleep(1000);
            IWebElement btnEditar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div[4]/div[2]/div[2]/div[2]/div[1]/div/button[2]/table/tbody/tr/td[2]")));
            btnEditar.Click();

            //Verificaciones
            IWebElement txtNombreActual = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("Nombre")));
            string txtNombreActualTexto = txtNombreActual.GetAttribute("value");
            Assert.AreEqual(txtNombreEditado, txtNombreActualTexto);

            //Regresar a lista de Instituciones Educativas
            IWebElement btnCancelar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnCancelarInstitucionEducativa"))); 
            btnCancelar.Click();
        }

        // ------------------------------------------------------ GUARDAR DATOS EN BLANCO
        [Test, Order(3)]
        public void Guardar_Institución_Educativa_Con_Campos_En_Blanco()
        {
            //Inicialización de variables
            var txtErrorExpected = "Favor ingrese el nombre de la Institución Educativa";

            //Crear nueva Institución Educativa
            IWebElement btnNuevo = driver.FindElement(By.Id("smlInstitucionesEducativas_new"));
            btnNuevo.Click();
            IWebElement txtNombre = driver.FindElement(By.Id("Nombre"));
            IWebElement btnGuardar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Name("btnGuardarInstitucionEducativa")));
            btnGuardar.Click();

            //Verificaciones
            IWebElement msgError = driver.FindElement(By.XPath("//*[@id='MessagesAndErrors']/div"));
            Assert.AreEqual(txtErrorExpected, msgError.Text);

            //Regresar a lista de Instituciones Educativas
            IWebElement btnCancelar = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnCancelarInstitucionEducativa"))); 
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