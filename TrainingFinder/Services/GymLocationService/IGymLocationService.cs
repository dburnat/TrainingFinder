using System;

namespace TrainingFinder.Services.GymLocationService
{
    public interface IGymLocationService
    {
        double GetLatitude(string address);
        double GetLongitude(string address);
    }
}