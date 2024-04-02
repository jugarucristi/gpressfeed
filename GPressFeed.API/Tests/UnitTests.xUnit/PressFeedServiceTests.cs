using Application.Interfaces;
using Application.Models;
using Application.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace UnitTests.xUnit
{
    public class PressFeedServiceTests
    {
        private readonly IGoogleTrendsRetriever _googleTrendsRetrieverMock;
        private readonly IPressFeedRepository _repositoryMock;
        private readonly IPaLMRetriever _paLMRetrieverMock;

        public PressFeedServiceTests()
        {
            _googleTrendsRetrieverMock = Substitute.For<IGoogleTrendsRetriever>();
            _repositoryMock = Substitute.For<IPressFeedRepository>();
            _paLMRetrieverMock = Substitute.For<IPaLMRetriever>();
        }

        [Fact]
        public async void GetLatestNewsAsync_ReturnsListOfNews()
        {
            //Arrange
            var latestAvailableFeed = new Feed();
            _repositoryMock.GetLatestAvailableFeed().Returns(latestAvailableFeed);

            var service = new PressFeedService(_googleTrendsRetrieverMock, _repositoryMock, _paLMRetrieverMock);

            //Act
            var result = await service.GetLatestNewsAsync();

            //Assert
            Assert.Equal(latestAvailableFeed, result);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void UpsertAndReturnTodaysNewsAsync_IfCurrentNewsIsNull_UpsertsAndReturnsNews(bool currentFeedIsNull)
        {
            //Arrange
            var currentFeed = new Feed();
            _repositoryMock.GetCurrentNewsFeedAsync().Returns(!currentFeedIsNull ? currentFeed : null);

            var articleList = new List<Article>();
            var newlyGeneratedFeed = new Feed();
            _googleTrendsRetrieverMock.GetNews().Returns(articleList);
            _paLMRetrieverMock.GetFeedWithArticleCategories(articleList).Returns(newlyGeneratedFeed);

            var service = new PressFeedService(_googleTrendsRetrieverMock, _repositoryMock, _paLMRetrieverMock);

            //Act
            var result = await service.UpsertAndReturnTodaysNewsAsync();

            //Assert
            Assert.Equal(currentFeedIsNull, result != currentFeed);
        }
    }
}