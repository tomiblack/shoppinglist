using System.Net;
using System.Threading.Tasks;
using Moq;
using ShoppingList.WebApi.Controllers;
using ShoppingList.WebApi.Models;
using ShoppingList.WebApi.Services;
using Xunit;

namespace ShoppingList.WebApi.Tests
{
    public class ItemsControllerTests
    {
        private readonly Mock<IItemService> _mockItemService;
        private readonly ItemsController _controller;

        public ItemsControllerTests()
        {
            _mockItemService = new Mock<IItemService>();
            _controller = new ItemsController(_mockItemService.Object);
        }

        [Fact]
        public async Task Upsert_NoItem_BadRequest()
        {
            var exception = await Assert.ThrowsAsync<HttpStatusCodeException>(async () => await _controller.Upsert(null));
            Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        }

        [Fact]
        public async Task Upsert_Item_UpsertAndReturnNewItem()
        {
            var returns = new Item { Id = 12, Name = "NewItem" };
            _mockItemService
                .Setup(x => x.Upsert(It.Is<Item>(i => i.Id == 11 && i.Name == "Item")))
                .Returns(Task.FromResult(returns));
            var item = new Item { Id = 11, Name = "Item" };

            var newItem = await _controller.Upsert(item);

            Assert.NotNull(newItem);
            Assert.Equal(returns.Id, newItem.Id);
            Assert.Equal(returns.Name, newItem.Name);
        }

        // TODO: More unit tests
    }
}
