using AutoFixture;
using BooksApi.Controllers.RequestsResponse;
using BooksApi.Infrastructure;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BooksApi.IntegrationTests
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly Fixture _fixture;


        public IntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Given_CorrectBookRequest_When_Call_PostMethod_Then_BookIdShouldBeReturned()
        {
            //given
            var createBookRequest = _fixture.Build<Book>().With(s => s.Isbn, "ISBN 978-0-672-33601-0").Create();

            //when
            var httpResponse = await _client.PostAsync("/Books", new StringContent(JsonConvert.SerializeObject(createBookRequest), Encoding.UTF8, "application/json"));


            //then
            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var responseGuid = JsonConvert.DeserializeObject<Guid>(await httpResponse.Content.ReadAsStringAsync());
            responseGuid.Should().NotBeEmpty();
        }


        [Fact]
        public async Task Given_IncorrectBookRequest_When_Call_PostMethod_Then_BadRequestShuoldBeReturned()
        {
            //given
            var createBookRequest = _fixture.Create<Book>();

            //when
            var httpResponse = await _client.PostAsync("/Books", new StringContent(JsonConvert.SerializeObject(createBookRequest), Encoding.UTF8, "application/json"));

            //then
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Fact]
        public async Task Given_CorrectUserRequest_When_Call_PostMethod_Then_UserIdShouldBeReturned()
        {
            //given
            var createUserRequest = _fixture.Create<User>();

            //when
            var httpResponse = await _client.PostAsync("/Users", new StringContent(JsonConvert.SerializeObject(createUserRequest), Encoding.UTF8, "application/json"));

            //then
            httpResponse.EnsureSuccessStatusCode();

            var responseGuid = JsonConvert.DeserializeObject<Guid>(await httpResponse.Content.ReadAsStringAsync());
            responseGuid.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Given_CorrectUserBookOpinionRequest_And_CreateUserAndBook_When_Call_PostMethod_Then_UserOpinionIdShouldIdShouldBeReturned()
        {
            //given

            //1. Create book
            var createBookRequest = _fixture.Build<Book>().With(s => s.Isbn, "ISBN 978-0-672-33601-0").Create();

            var bookHttpResponse = await _client.PostAsync("/Books", new StringContent(JsonConvert.SerializeObject(createBookRequest), Encoding.UTF8, "application/json"));

            bookHttpResponse.EnsureSuccessStatusCode();

            var bookId = JsonConvert.DeserializeObject<Guid>(await bookHttpResponse.Content.ReadAsStringAsync());

            //2. Create user
            var createUserRequest = _fixture.Create<User>();

            var httpResponseUser = await _client.PostAsync("/Users", new StringContent(JsonConvert.SerializeObject(createUserRequest), Encoding.UTF8, "application/json"));

            httpResponseUser.EnsureSuccessStatusCode();

            var userId = JsonConvert.DeserializeObject<Guid>(await httpResponseUser.Content.ReadAsStringAsync());

            var userBookOpinion = _fixture.Build<UserBookOpinionRequest>().With(s => s.BookId, bookId).With(s => s.UserId, userId).With(s => s.UserRating, 3).Create();

            //when
            var userBookOpinionrRsponse = await _client.PostAsync("/BooksOpinions", new StringContent(JsonConvert.SerializeObject(userBookOpinion), Encoding.UTF8, "application/json"));

            //then
            userBookOpinionrRsponse.EnsureSuccessStatusCode();

            var responseGuid = JsonConvert.DeserializeObject<Guid>(await userBookOpinionrRsponse.Content.ReadAsStringAsync());
            responseGuid.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Given_CreatedUserBookOpinion_When_CallGetAll_Then_UserOpinionIdShouldIdShouldBeReturned()
        {
            //given

            //1. Create book
            var createBookRequest = _fixture.Build<Book>().With(s => s.Isbn, "ISBN 978-0-672-33601-0").Create();

            var bookHttpResponse = await _client.PostAsync("/Books", new StringContent(JsonConvert.SerializeObject(createBookRequest), Encoding.UTF8, "application/json"));

            bookHttpResponse.EnsureSuccessStatusCode();

            var bookId = JsonConvert.DeserializeObject<Guid>(await bookHttpResponse.Content.ReadAsStringAsync());

            //2. Create user
            var createUserRequest = _fixture.Create<User>();

            var httpResponseUser = await _client.PostAsync("/Users", new StringContent(JsonConvert.SerializeObject(createUserRequest), Encoding.UTF8, "application/json"));

            httpResponseUser.EnsureSuccessStatusCode();

            var userId = JsonConvert.DeserializeObject<Guid>(await httpResponseUser.Content.ReadAsStringAsync());

            var userBookOpinion = _fixture.Build<UserBookOpinionRequest>().With(s => s.BookId, bookId).With(s => s.UserId, userId).With(s => s.UserRating, 3).Create();

            //3. Create user book opinion
            var userBookOpinionrRsponse = await _client.PostAsync("/BooksOpinions", new StringContent(JsonConvert.SerializeObject(userBookOpinion), Encoding.UTF8, "application/json"));

            userBookOpinionrRsponse.EnsureSuccessStatusCode();

            //then
            var getAllHttpResponse = await _client.GetAsync("/Books");

            getAllHttpResponse.EnsureSuccessStatusCode();

            var booksResult = JsonConvert.DeserializeObject<IList<Book>>(await getAllHttpResponse.Content.ReadAsStringAsync());
            booksResult.Should().NotBeEmpty();
            booksResult.All(s => s.DeletionDate == null).Should().BeTrue();
            booksResult.First(s => s.Id == bookId).UsersBookOpinions.Count().Should().Be(1);
        }
    }
}
