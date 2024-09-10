using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Pizza.Api.Controllers;
using Pizza.Api.Repository;

namespace Pizza.Api.Tests.Controllers
{
    [TestClass]
    public class PizzaControllerTests
    {
        private PizzaController _controller;
        private IRepository<Models.Pizza> _pizzaRepository;
        private IRepository<Models.Burger> _burgerRepository;

        [TestInitialize]
        public void SetUp()
        {
            _pizzaRepository = Substitute.For<IRepository<Models.Pizza>>();
            _burgerRepository = Substitute.For<IRepository<Models.Burger>>();
            _controller = new PizzaController(_pizzaRepository, _burgerRepository);
        }

        [TestMethod]
        public void GetAll_ShouldReturnOkWithPizzas()
        {
            // Arrange
            var pizzas = new List<Models.Pizza> { new Models.Pizza { Id = Guid.NewGuid(), Name = "Margherita" } };
            _pizzaRepository.GetAll().Returns(pizzas);

            // Act
            var result = _controller.GetAll() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(pizzas, result.Value);
        }

        [TestMethod]
        public void GetById_PizzaExists_ShouldReturnOkWithPizza()
        {
            // Arrange
            var pizzaId = Guid.NewGuid();
            var pizza = new Models.Pizza { Id = pizzaId, Name = "Margherita" };
            _pizzaRepository.GetById(pizzaId).Returns(pizza);

            // Act
            var result = _controller.GetById(pizzaId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(pizza, result.Value);
        }

        [TestMethod]
        public void GetById_PizzaDoesNotExist_ShouldReturnNotFound()
        {
            // Arrange
            var pizzaId = Guid.NewGuid();
            _pizzaRepository.GetById(pizzaId).Returns((Models.Pizza)null);

            // Act
            var result = _controller.GetById(pizzaId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Create_ValidPizza_ShouldReturnCreatedAtAction()
        {
            // Arrange
            var pizza = new Models.Pizza { Id = Guid.NewGuid(), Name = "Margherita" };

            // Act
            var result = _controller.Create(pizza) as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual(nameof(_controller.GetById), result.ActionName);
            Assert.AreEqual(pizza.Id, result.RouteValues["id"]);
            Assert.AreEqual(pizza, result.Value);
        }

        [TestMethod]
        public void Update_PizzaExists_ShouldReturnNoContent()
        {
            // Arrange
            var pizzaId = Guid.NewGuid();
            var pizza = new Models.Pizza { Id = pizzaId, Name = "Margherita" };
            _pizzaRepository.GetById(pizzaId).Returns(pizza);

            // Act
            var result = _controller.Update(pizzaId, pizza);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void Update_PizzaIdMismatch_ShouldReturnBadRequest()
        {
            // Arrange
            var pizzaId = Guid.NewGuid();
            var pizza = new Models.Pizza { Id = Guid.NewGuid(), Name = "Margherita" }; // Different ID

            // Act
            var result = _controller.Update(pizzaId, pizza);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Update_PizzaDoesNotExist_ShouldReturnNotFound()
        {
            // Arrange
            var pizzaId = Guid.NewGuid();
            var pizza = new Models.Pizza { Id = pizzaId, Name = "Margherita" };
            _pizzaRepository.GetById(pizzaId).Returns((Models.Pizza)null);

            // Act
            var result = _controller.Update(pizzaId, pizza);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Delete_PizzaExists_ShouldReturnNoContent()
        {
            // Arrange
            var pizzaId = Guid.NewGuid();
            var pizza = new Models.Pizza { Id = pizzaId, Name = "Margherita" };
            _pizzaRepository.GetById(pizzaId).Returns(pizza);

            // Act
            var result = _controller.Delete(pizzaId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void Delete_PizzaDoesNotExist_ShouldReturnNotFound()
        {
            // Arrange
            var pizzaId = Guid.NewGuid();
            _pizzaRepository.GetById(pizzaId).Returns((Models.Pizza)null);

            // Act
            var result = _controller.Delete(pizzaId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
