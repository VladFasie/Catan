using Infrastructure.Extensions;
using System;

namespace MapGeneratorApi
{
    public class CatanParty
    {
        public string Id { get; }

        public CatanParty()
        {
            Id = (new Random()).GenerateId(4);
        }
    }
}
