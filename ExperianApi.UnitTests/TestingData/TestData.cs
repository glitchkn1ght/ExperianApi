using ExperianApi.Models.Photos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExperianApi.UnitTests.TestingData
{
    public class TestData
    {
        public Album ReturnValidAlbumData()
        {
            return new Album()
            {
                AlbumId = 1,
                UserId = 1,
                AlbumTitle = "Mutter",
                AlbumPhotos = new List<Photo>
                {
                    new Photo{
                        PhotoId = 1,
                        AlbumId = 1,
                        PhotoTitle ="SomeTitle1",
                        PhotoUrl = "https://via.placeholder.com/600/92c952",
                        PhotoThumbnailUrl= "https://via.placeholder.com/150/92c952",
                    },

                      new Photo{
                        PhotoId = 2,
                        AlbumId = 1,
                        PhotoTitle ="SomeTitle2",
                        PhotoUrl = "https://via.placeholder.com/600/92c952",
                        PhotoThumbnailUrl= "https://via.placeholder.com/150/92c952",
                    },


                }
            };
        }

    }
}
