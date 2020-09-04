using E_CommerceApp;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Test
{
    public class TestFunctions
    {
        private IDataOperations _operations;

        [OneTimeSetUp]
        public void Setup()
        {
            _operations = GlobalConfig.Inject();
            _operations.LoadProductData();
            _operations.LoadCart();
        }

        [Test]
        public async Task InsertProductTest()
        {
            //Arrange
            var expected = _operations.ProductData.Count + 1;
            await Task.Run(() => _operations.AddProductData("Spinach", 5, 10));
            _operations.LoadProductData();

            //Act
            var actual = await Task.Run(() => _operations.ProductData.Count);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public async Task DeleteProductTest()
        {
            //Arrange
            int id = _operations.ProductData[^1].ProductId;
            var expected = _operations.ProductData.Count - 1;

            await Task.Run(() => _operations.DeleteProductData(id));
            _operations.LoadProductData();

            //Act
            var actual = await Task.Run(() => _operations.ProductData.Count);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public async Task UpdateProductTest()
        {
            //Arrange  // Act
            int id = _operations.ProductData[^1].ProductId;

            await Task.Run(() => _operations.UpdateProductData(id, "Mango", 4, 40));
            _operations.LoadProductData();

            //Act
            var expected = 40;
            var actual = _operations.ProductData[^1].CostPrice;

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public async Task InsertCartTest()
        {
            //Arrange
            var productId = _operations.ProductData[^1].ProductId;
            var expected = _operations.CartData.Count + 1;
            await Task.Run(() => _operations.AddToCart(productId, 5, 10));
            _operations.LoadCart();

            //Act
            var actual = await Task.Run(() => _operations.CartData.Count);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public async Task DeleteFromCartTest()
        {
            //Arrange
            int id = _operations.CartData[^1].Id;
            var expected = _operations.CartData.Count - 1;

            await Task.Run(() => _operations.DeleteFromCart(id));
            _operations.LoadCart();

            //Act
            var actual = await Task.Run(() => _operations.CartData.Count);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public async Task UpdateCartTest()
        {
            //Arrange  // Act
            int id = _operations.CartData[^1].Id;

            await Task.Run(() => _operations.UpdateCart(id, 20));
            _operations.LoadCart();

            //Act
            var expected = 20;
            var actual = _operations.CartData[^1].Quantity;

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}