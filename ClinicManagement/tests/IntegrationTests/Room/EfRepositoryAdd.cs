﻿using System.Linq;
using System.Threading.Tasks;
using ClinicManagement.Infrastructure.Data;
using UnitTests.Builders;
using Xunit;

namespace IntegrationTests.Room
{
  public class EfRepositoryAdd : BaseEfRepoTestFixture
  {
    private readonly EfRepository _repository;

    public EfRepositoryAdd()
    {
      _repository = GetRepository();
    }

    [Fact]
    public async Task AddsRoomAndSetsId()
    {
      var room = await AddRoom();

      var newRoom = (await _repository.ListAsync<ClinicManagement.Core.Aggregates.Room, int>()).FirstOrDefault();

      Assert.Equal(room, newRoom);
      Assert.True(newRoom?.Id > 0);
    }

    private async Task<ClinicManagement.Core.Aggregates.Room> AddRoom()
    {
      var room = new RoomBuilder().Id(2).Build();

      await _repository.AddAsync<ClinicManagement.Core.Aggregates.Room, int>(room);

      return room;
    }
  }
}
