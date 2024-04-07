using Application.Interfaces;
using Application.Models;
using Application.Services;
using NSubstitute;

namespace UnitTests.xUnit
{
    public class PressFeedServiceTests
    {
        private readonly IGoogleTrendsRetriever _googleTrendsRetrieverMock;
        private readonly IPressFeedRepository _repositoryMock;
        private readonly ICategoryRetriever _paLMRetrieverMock;

        public PressFeedServiceTests()
        {
            _googleTrendsRetrieverMock = Substitute.For<IGoogleTrendsRetriever>();
            _repositoryMock = Substitute.For<IPressFeedRepository>();
            _paLMRetrieverMock = Substitute.For<ICategoryRetriever>();
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
        public async void UpsertTodaysNewsAsync_IfCurrentNewsIsNull_UpsertsNews(bool currentFeedIsNull)
        {
            //Arrange
            var currentFeed = new Feed();
            _repositoryMock.GetCurrentNewsFeedAsync().Returns(!currentFeedIsNull ? currentFeed : null);

            var articleList = new List<Article>();
            var newlyGeneratedFeed = new Feed();
            _googleTrendsRetrieverMock.GetNews().Returns(articleList);
            _paLMRetrieverMock.GetFeedWithArticleCategories(articleList).Returns(newlyGeneratedFeed);

            var service = new PressFeedService(_googleTrendsRetrieverMock, _repositoryMock, _paLMRetrieverMock);

            var numberOfCalls = currentFeedIsNull ? 1 : 0;

            //Act
            await service.UpsertTodaysNewsAsync();

            //Assert
            await _googleTrendsRetrieverMock.ReceivedWithAnyArgs(numberOfCalls).GetNews();
            await _paLMRetrieverMock.ReceivedWithAnyArgs(numberOfCalls).GetFeedWithArticleCategories(articleList);
            await _repositoryMock.ReceivedWithAnyArgs(numberOfCalls).InsertNewsFeedAsync(currentFeed);
        }
    }
}