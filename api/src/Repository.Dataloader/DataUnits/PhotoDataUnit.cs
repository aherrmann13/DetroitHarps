namespace Repository.Dataloader.DataUnit
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using Repository.Entities;

    public class PhotoDataUnit : DataUnitBase<PhotoGroup>
    {
        private readonly string _path;

        public PhotoDataUnit(ApiDbContext dbContext)
            : base(dbContext)
        {
            _path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        protected override IEnumerable<PhotoGroup> Data => 
            new List<PhotoGroup>
            {
                new PhotoGroup
                {
                    SortOrder = 0,
                    Name = "2016 team photos",
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            Title = "Photo 1",
                            SortOrder = 0,
                            Data = GetPhotoData("Files/2016_group1.jpg")
                        },
                        new Photo
                        {
                            Title = "Photo 2",
                            SortOrder = 1,
                            Data = GetPhotoData("Files/2016_group2.jpg")
                        },
                        new Photo
                        {
                            Title = "Photo 3",
                            SortOrder = 2,
                            Data = GetPhotoData("Files/2016_group3.jpg")
                        }
                    }
                },
                new PhotoGroup
                {
                    SortOrder = 1,
                    Name = "2015 team photos",
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            Title = "Photo 1",
                            SortOrder = 0,
                            Data = GetPhotoData("Files/2015_group.jpg")
                        }
                    }
                },
                new PhotoGroup
                {
                    SortOrder = 2,
                    Name = "2014 team photos",
                    Photos = new List<Photo>
                    {
                        new Photo
                        {
                            Title = "Photo 1",
                            SortOrder = 0,
                            Data = GetPhotoData("Files/2014_group.jpg")
                        }
                    }
                }
            };

        private byte[] GetPhotoData(string path)
        {
            path = Path.Combine(_path, path);

            if(!File.Exists(path))
            {
                throw new InvalidOperationException($"File with path {path} does not exist");
            }

            return File.ReadAllBytes(path);
        }

    }
}
