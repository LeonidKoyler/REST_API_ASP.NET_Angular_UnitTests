﻿using Calculation.Domain.Model;
using static Calculation.Common.Common;

namespace Calculation.Domain.Service
{
    public interface IVehicleService
    {
        Task<Vehicle> CalculateFees(decimal basePrice, VehicleType type, CancellationToken token = default);
    }
}