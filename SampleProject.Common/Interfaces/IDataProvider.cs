﻿using SampleProject.Common.Data;

namespace SampleProject.Common.Interfaces
{
    public interface IDataProvider
    {
        JsonData GetRateCollection();
    }
}